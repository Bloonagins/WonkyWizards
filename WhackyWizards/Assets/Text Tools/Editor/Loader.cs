// Copyright (C) 2019 Alexander Klochkov - All Rights Reserved
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms


using UnityEngine;	

using System;
using System.IO; 
using System.Collections.Generic;


namespace texttools
{
	internal struct FontPoint
	{
		internal float x;
		internal float y;
		internal uint  flags;
	};


	internal class FontHeader
	{
		//internal UInt16 majorVersion;
		//internal UInt16 minorVersion;
		//internal UInt32 fontRevision;
		//internal UInt32 checkSumAdjustment;
		//internal UInt32 magicNumber;
		//internal UInt16 flags;
		internal UInt16 unitsPerEm;
		//internal Int64  created;
		//internal Int64  modified;
		//internal Int16  xMin;
		//internal Int16  yMin;
		//internal Int16  xMax;
		//internal Int16  yMax;
		//internal UInt16 macStyle;
		//internal UInt16 lowestRecPPEM;
		//internal Int16  fontDirectionHint;
		internal Int16  indexToLocFormat;
		internal Int16  glyphDataFormat;
	};	
	
	internal class FontMetricsHeader
	{
		//internal UInt32 version;
		internal Int16  ascent;
		internal Int16  descent;
		internal Int16  lineGap;
		internal UInt16 advanceMax;
		//internal Int16  minLeftTopSideBearing;
		//internal Int16  minRightBottomSideBearing;
		//internal Int16  xMaxExtent;
		//internal Int16  caretSlopeRise;
		//internal Int16  caretSlopeRun;
		//internal Int16  caretOffset;
		//internal Int16  reserved0; 
		//internal Int16  reserved1;
		//internal Int16  reserved2;
		//internal Int16  reserved3;
		//internal Int16  metricDataFormat;
		internal UInt16 numOfLongMetrics;
	}; 
	
	internal class  FontMaximumProfile
	{
		//internal UInt32 version;
		internal UInt16 numGlyphs;
		internal UInt16 maxPoints;
		//internal UInt16 maxContours;
		//internal UInt16 maxCompositePoints;
		//internal UInt16 maxCompositeContours;
		//internal UInt16 maxZones;
		//internal UInt16 maxTwilightPoints;
		//internal UInt16 maxStorage;
		//internal UInt16 maxFunctionDefs;
		//internal UInt16 maxInstructionDefs;
		//internal UInt16 maxStackElements;
		//internal UInt16 maxSizeOfInstructions;
		//internal UInt16 maxComponentElements;
		//internal UInt16 maxComponentDepth;
	};

	internal struct CFF2Dict
	{
		internal uint begin;
		internal uint end;
	};
	

	internal static class FontReader
	{
		internal static UInt16 ReadUInt16(byte[] data, ref uint i)
		{
			i += 2;
			return (UInt16)((UInt16)(data[i-2]<<8)|(UInt16)(data[i-1]));
		}

		internal static UInt16 ReadUInt16(byte[] data, uint i)
		{
			return (UInt16)((UInt16)(data[i]<<8)|(UInt16)(data[i+1]));
		}

		internal static Int16 ReadInt16(byte[] data, ref uint i)
		{
			i += 2;
			return (Int16)((data[i-2]<<8)|(data[i-1]));
		}

		internal static Int16 ReadInt16(byte[] data, uint i)
		{
			return (Int16)((data[i]<<8)|(data[i+1]));
		}

		internal static UInt32 ReadUInt32(byte[] data, ref uint i)
		{
			i += 4;
			return ((UInt32)(data[i-4])<<24)|((UInt32)(data[i-3])<<16)|((UInt32)(data[i-2])<<8)|((UInt32)data[i-1]);
		}

		internal static UInt32 ReadUInt32(byte[] data, uint i)
		{
			return ((UInt32)(data[i])<<24)|((UInt32)(data[i+1])<<16)|((UInt32)(data[i+2])<<8)|((UInt32)data[i+3]);
		}

		internal static Int32 ReadInt32(byte[] data, ref uint i)
		{
			i += 4;
			return (data[i-4]<<24)|(data[i-3]<<16)|(data[i-2]<<8)|(data[i-1]);
		}

		internal static Int32 ReadCFF2Operand(byte[] data, ref uint i)
		{
			int b0 = data[i++];

			if(b0 > 31 && b0 < 247)
				return b0 - 139;

			if(b0 > 246 && b0 < 251)
				return (b0 - 247)*256 + data[i++] + 108;

			if(b0 > 250 && b0 < 255)
				return -(b0 - 251)*256 - data[i++] - 108;

			if(28 == b0)
				return (Int32)((data[i++]<<8)|(data[i++]));

			if(29 == b0)
				return (Int32)((data[i++]<<24)|(data[i++]<<16)|(data[i++]<<8)|(data[i++]));

			return 0;
		}

		internal static UInt32 ReadCFF2Offset(byte[] data, uint size, ref uint i)
		{
			if(1 == size)
				return (UInt32)data[i++];

			if(2 == size)
				return (UInt32)((data[i++]<<8)|(data[i++]));

			if(3 == size)
				return (UInt32)((data[i++]<<16)|(data[i++]<<8)|(data[i++]));

			if(4 == size)
				return (UInt32)((data[i++]<<24)|(data[i++]<<16)|(data[i++]<<8)|(data[i++]));

			return 0;
		}

		internal static void ReadCFF2DictData(byte[] data, CFF2Dict dict, int key, ref uint out1, ref uint out2)
		{
			uint offset, i = dict.begin;
			int  op, v = 0;

			while(i < dict.end)
			{
				offset = i;

				while(i < dict.end && data[i] > 27)
				{
					if(30 == data[i])
					{
						++i;
						while(i < dict.end)
						{
							v = data[i++];
							if((v & 0xf) == 0xf || (v>>4) == 0xf)
								break;
						}
					}
					else
					{
						v = ReadCFF2Operand(data, ref i);
					}
				}

				if(i >= dict.end)
					break;

				op = data[i++];

				if(op == 12)
					op = data[i++] + 100;
				if(op == key)
				{
					out1 = (uint)ReadCFF2Operand(data, ref offset);
					out2 = (uint)v;
				}
			}
		}

		internal static void ReadCFF2Dict(byte[] data, uint index, uint n, ref CFF2Dict dict)
		{
			uint i     = index;
			uint count = (uint)((data[i++]<<8)|(data[i++]));
			uint size  = data[i++];

			dict.begin = dict.end = 0;

			if(n >= count)
				return;

			i += n * size;
			dict.begin = index + 2 + (count + 1)*size + ReadCFF2Offset(data, size, ref i);
			dict.end   = index + 2 + (count + 1)*size + ReadCFF2Offset(data, size, ref i);
		}

		internal static void ReadCFF2Subr(byte[] data, uint index, uint n, ref CFF2Dict dict)
		{
			uint i     = index;
			uint count = (uint)((data[i++]<<8)|(data[i++]));
			uint size  = data[i++];

			dict.begin = dict.end = 0;

			if(count < 1240)
				n += 107;
			else if(count < 33900)
				n += 1131;
			else
				n += 32768;

			if(n >= count)
				return;

			i += n * size;
			dict.begin = index + 2 + (count + 1)*size + ReadCFF2Offset(data, size, ref i);
			dict.end   = index + 2 + (count + 1)*size + ReadCFF2Offset(data, size, ref i);
		}

		internal static void ReadCFF2Index(byte[] data, ref uint i)
		{
			uint count = (uint)((data[i++]<<8)|(data[i++]));

			if(count > 0)
			{
				uint n, size = data[i++];
				i += count * size;
				n = ReadCFF2Offset(data, size, ref i);
				i = i + n - 1;
			}
		}

		internal static uint ReadCFF2Subrs(byte[] data, CFF2Dict dict, uint cff)
		{
			CFF2Dict priv   = default(CFF2Dict);
			uint     size   = 0;
			uint     offset = 0;
			uint     subrs  = 0;
			uint     temp   = 0;

			ReadCFF2DictData(data, dict, 18, ref size, ref offset);

			if(0 == size || 0 == offset)
				return 0;

			priv.begin = cff + offset;
			priv.end   = priv.begin + size;

			ReadCFF2DictData(data, priv, 19, ref subrs, ref temp);

			return cff + offset + subrs;
        }

        internal static uint ReadCFF2CIDSubrs(byte[] data, uint cff, uint gi, uint fdarray, uint fdselect)
        {
            CFF2Dict dict   = default(CFF2Dict);
            int      n      = -1;
            uint     i      = fdselect;
            uint     format = data[i++];

            if(0 == format)
            {
                n = data[i+gi];
            }
            else if(3 == format)
            {
                uint ranges = (uint)((data[i++]<<8)|(data[i++]));
                uint first  = (uint)((data[i++]<<8)|(data[i++]));
                uint last   = 0;
                uint value  = 0;

                for(uint j=0; j<ranges; ++j)
                {
                    value = data[i++];
                    last  = (uint)((data[i++]<<8)|(data[i++]));

                    if(gi >= first && gi < last)
                    {
                        n = (int)value;
                        break;
                    }

                    first = last;
                }
            }

            if(-1 == n)
                return 0;

            FontReader.ReadCFF2Dict(data, fdarray, (uint)n, ref dict);

            return ReadCFF2Subrs(data, dict, cff);
        }

        internal static FontHeader ReadFontHeader(byte[] data, uint i)
		{
			FontHeader head = new FontHeader();

			i += 18;
			head.unitsPerEm = ReadUInt16(data, ref i);

			i += 30;
			head.indexToLocFormat = ReadInt16(data, ref i);
			head.glyphDataFormat  = ReadInt16(data, ref i);
			
			return head;
		}

		internal static FontMetricsHeader ReadFontMetricsHeader(byte[] data, uint i)
		{
			FontMetricsHeader head = new FontMetricsHeader();

			i += 4;
			head.ascent     = ReadInt16(data, ref i);  
			head.descent    = ReadInt16(data, ref i);
			head.lineGap    = ReadInt16(data, ref i);	  
			head.advanceMax = ReadUInt16(data, ref i);

			i += 22;
			head.numOfLongMetrics = ReadUInt16(data, ref i);

			return head;
		}	  
		
		internal static FontMaximumProfile ReadFontMaximumProfile(byte[] data, uint i)
		{
			FontMaximumProfile maxp = new FontMaximumProfile();

			i += 4;
			maxp.numGlyphs = ReadUInt16(data, ref i);
			maxp.maxPoints = ReadUInt16(data, ref i);

			return maxp;
		}
	}


	public static class TextToolsLoader 
	{
		static byte[]              data;
		static FontPoint[]         points;
		
		static List<uint>          indices    = new List<uint>(); 
		static List<char>          characters = new List<char>();

		static FontHeader          head;	
		static FontMetricsHeader   hhea;  
		static FontMetricsHeader   vhea;	  
		static FontMaximumProfile  maxp;
		static uint                cmap;	
		static uint                loca;
		static uint                glyf;
		static uint                kern;
		static uint                name;
		static uint                hmtx;
		static uint                vmtx;
		static uint                cff;

		static uint                charStrings;
		static uint                fontDictArray;
		static uint                fontDictSelect;
		static uint                globalSubrs;
		static uint                subrs;
		
		static uint                characterMapOffset;	
		static uint                numKerningTables;


		private static void Clear()
		{
			indices.Clear();
			characters.Clear();

			points = null;
			data   = null;

			head   = null;
			hhea   = null;	
			vhea   = null;
			maxp   = null;
			cmap   = 0;	
			loca   = 0;
			glyf   = 0;
			kern   = 0;
			name   = 0;
			hmtx   = 0;
			vmtx   = 0;
			cff    = 0;

            charStrings    = 0;
            fontDictArray  = 0;
            fontDictSelect = 0;
            globalSubrs    = 0;
            subrs          = 0;

            characterMapOffset = 0;
			numKerningTables   = 0;
		} 	
		
		private static void LoadContour(uint gdata, int i, ref Path path)
		{
			float x;
			float y;
			int   next;
			int   curr  = (0 == i) ? 0 : FontReader.ReadUInt16(data, gdata + (uint)(i-1)*2) + 1;
			int   last  = FontReader.ReadUInt16(data, gdata + (uint)(i)*2);
			int   first = curr;
			
			if(0 != (points[curr].flags & 0x01))
			{
				path.MoveTo(points[curr].x, points[curr].y);
				++curr;
			}
			else if(0 != (points[last].flags & 0x01))
			{
				path.MoveTo(points[last].x, points[last].y);
			}
			else
			{
				x = (points[curr].x + points[last].x) * 0.5f;
				y = (points[curr].y + points[last].y) * 0.5f;
				path.MoveTo(x, y);
			}

			while(curr <= last)
			{
				if(0 != (points[curr].flags & 0x01))
				{
					path.LineTo(points[curr].x, points[curr].y);
				}
				else
				{
					next = (curr == last) ? first : curr + 1;

					if(0 != (points[next].flags & 0x01))
					{
						path.QuadraticCurveTo(points[next].x, points[next].y, points[curr].x, points[curr].y);
						++curr;
					}
					else
					{
						x = (points[curr].x + points[next].x) * 0.5f;
						y = (points[curr].y + points[next].y) * 0.5f;
						path.QuadraticCurveTo(x, y, points[curr].x, points[curr].y);
					}
				}

				++curr;
			}

			path.Finalize(true);
		}

		private static bool LoadTTFGlyph(uint ind, char c, float[] transform, ref Path path)  
		{
			uint i1;
			uint i2;
			uint gdata;

			if(1 == head.indexToLocFormat) 
			{
				i1 = FontReader.ReadUInt32(data, loca + ind*4);
				i2 = FontReader.ReadUInt32(data, loca + ind*4 + 4);
				gdata = i1 != i2 ? glyf + i1 : 0;
			}
			else
			{
				i1 = FontReader.ReadUInt16(data, loca + ind*2);
				i2 = FontReader.ReadUInt16(data, loca + ind*2 + 2);
				gdata = i1 != i2 ? glyf + i1*2 : 0;
			}

			if(0 == gdata)
				return false;

			int contours = FontReader.ReadInt16(data, gdata);
			gdata += 10;
			
			if(0 == contours)
				return false;

			if(contours < 0)
			{
				uint    flags;
				float[] t1  = new float[6] { 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f };
				float[] t2  = new float[6] { 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f };
				float   inv = 1.0f/16384.0f;
				uint    i   = gdata;

				do
				{
					flags = FontReader.ReadUInt16(data, ref i);
					ind   = FontReader.ReadUInt16(data, ref i);
					
					if(0 != (flags & 0x0001))
					{
						t1[4] = (0 != (flags & 0x0002)) ? (float)FontReader.ReadInt16(data, ref i) : FontReader.ReadUInt16(data, ref i);
						t1[5] = (0 != (flags & 0x0002)) ? (float)FontReader.ReadInt16(data, ref i) : FontReader.ReadUInt16(data, ref i);
					}
					else
					{
						t1[4] = (0 != (flags & 0x0002)) ? (float)(sbyte)data[i] : data[i];
						++i;
						t1[5] = (0 != (flags & 0x0002)) ? (float)(sbyte)data[i] : data[i];
						++i;
					}

					if(0 != (flags & 0x0008))
					{
						t1[0] = FontReader.ReadInt16(data, ref i) * inv;
						t1[1] = t1[0];
					}
					else if(0 != (flags & 0x0040))
					{
						t1[0] = FontReader.ReadInt16(data, ref i) * inv;
						t1[1] = FontReader.ReadInt16(data, ref i) * inv;
					}
					else if(0 != (flags & 0x0080))
					{
						t1[0] = FontReader.ReadInt16(data, ref i) * inv;
						t1[2] = FontReader.ReadInt16(data, ref i) * inv;
						t1[3] = FontReader.ReadInt16(data, ref i) * inv;
						t1[1] = FontReader.ReadInt16(data, ref i) * inv;
					}

					if(null != transform)
					{
						t2[0] = transform[0] * t1[0] + transform[3] * t1[2];
						t2[1] = transform[1] * t1[1] + transform[2] * t1[3];
						t2[2] = transform[1] * t1[2] + transform[2] * t1[0];
						t2[3] = transform[0] * t1[3] + transform[3] * t1[1];
						t2[4] = transform[0] * t1[4] + transform[3] * t1[5] + transform[4];
						t2[5] = transform[1] * t1[5] + transform[2] * t1[4] + transform[5];

						LoadTTFGlyph(ind, c, t2, ref path);
					}
					else
					{
						LoadTTFGlyph(ind, c, t1, ref path);
					}		
				}
				while(0 != (flags & 0x0020));
			}
			else
			{
				uint count = FontReader.ReadUInt16(data, gdata + (uint)(contours-1)*2) + 1U;   
				uint ptr   = gdata + (uint)(contours+1)*2 + FontReader.ReadUInt16(data, gdata + (uint)(contours)*2);	  
				
				for(uint i=0, n=0, f=0; i<count; ++i)
				{
					if(0 == n)
					{
						f = data[ptr++];

						if(0 != (f & 0x08))
							n = data[ptr++];
					}
					else
					{
						--n;
					}

					points[i].flags = f;
				} 
				
				for(int i=0, x=0; i<count; ++i)
				{
					if(0 != (points[i].flags & 0x02))
						x += (0 != (points[i].flags & 0x10)) ? data[ptr++] : -data[ptr++];
					else if(0 == (points[i].flags & 0x10))
						x += FontReader.ReadInt16(data, ref ptr);

					points[i].x = x;
				}	 
				
				for(int i=0, y=0; i<count; ++i)
				{
					if(0 != (points[i].flags & 0x04))
						y += (0 != (points[i].flags & 0x20)) ? data[ptr++] : -data[ptr++];
					else if(0 == (points[i].flags & 0x20))
						y += FontReader.ReadInt16(data, ref ptr);

					points[i].y = y;
				}

				if(null != transform)
				{
					float x, y;

					for(int i=0; i<count; ++i)
					{
						x = points[i].x * transform[0] + points[i].y * transform[3] + transform[4];
						y = points[i].y * transform[1] + points[i].x * transform[2] + transform[5];

						points[i].x = x;
						points[i].y = y;
					}
				}

				for(int i=0; i<contours; ++i)
					LoadContour(gdata, i, ref path);	
			}

			if(null == transform)
			{
				if(!indices.Contains(ind))
					indices.Add(ind);
				if(!characters.Contains(c))
					characters.Add(c);
			}

			return true;
		}

		private static bool LoadCFF2Glyph(uint ind, char c, ref Path path)
		{
			CFF2Dict str = default(CFF2Dict);

			FontReader.ReadCFF2Dict(data, charStrings, ind, ref str);

			float[] stack  = new float[48];
			uint[]  nest   = new uint[40];
			uint    i      = str.begin;
			uint    j      = 0;
			uint    bits   = 0;
			uint    count  = 0;
			uint    depth  = 0;
            uint    cid    = 0;
            int     b0     = 0;
			int     b1     = 0;
			float   f      = 0.0f;
			bool    header = true;
			bool    clear;

			while(i < str.end)
			{
				b0 = data[i++];
				clear = true;

				if(28 == b0 || 255 == b0 || (b0 > 31 && b0 < 255))
				{
					if(count > 47)
						return false;

					if(255 == b0)
					{
						stack[count++] = (float)(int)((data[i++]<<24)|(data[i++]<<16)|(data[i++]<<8)|(data[i++])) / 0x10000;
					}
					else
					{
						--i;
						stack[count++] = (float)(Int16)FontReader.ReadCFF2Operand(data, ref i);
					}

					continue;
				}

				if(12 == b0)
				{
					b1 = data[i++];
					count = 0;

					switch(b1)
					{
						default:
							continue;
					}
				}

				switch(b0)
				{
					case 0x01:
					case 0x03:
					case 0x12:
					case 0x17:
						bits += (count / 2);
						break;
					case 0x04:
						header = false;
						if(count < 1)
							return false;
						path.MoveTo(path.lastPos.x, path.lastPos.y + stack[count-1]);
						break;
					case 0x05:
						if(count < 2)
							return false;
						for(j=1; j<count; j+=2)
							path.LineTo(path.lastPos.x + stack[j-1], path.lastPos.y + stack[j]);
						break;
					case 0x06:
						if(count < 1)
							return false;
						for(j=0; ; )
						{
							if(j >= count)
								break;
							path.LineTo(path.lastPos.x + stack[j++], path.lastPos.y);
							if(j >= count)
								break;
							path.LineTo(path.lastPos.x, path.lastPos.y + stack[j++]);
						}
						break;
					case 0x07:
						if(count < 1)
							return false;
						for(j=0; ; )
						{
							if(j >= count)
								break;
							path.LineTo(path.lastPos.x, path.lastPos.y + stack[j++]);
							if(j >= count)
								break;
							path.LineTo(path.lastPos.x + stack[j++], path.lastPos.y);
						}
						break;
					case 0x08:
						if(count < 6)
							return false;
						for(j=5; j<count; j+=6)
							path.CubicCurveTo(path.lastPos.x + stack[j-5] + stack[j-3] + stack[j-1], path.lastPos.y + stack[j-4] + stack[j-2] + stack[j], path.lastPos.x + stack[j-5], path.lastPos.y + stack[j-4], path.lastPos.x + stack[j-5] + stack[j-3], path.lastPos.y + stack[j-4] + stack[j-2]);
						break;
					case 0x0a:
						if(count < 1 || depth > 19)
							return false;
                        if(fontDictSelect > 0 && 0 == cid)
                        {
                            subrs = FontReader.ReadCFF2CIDSubrs(data, cff, ind, cff+fontDictArray, cff+fontDictSelect);
                            cid   = 1;
                        }
                        nest[depth++] = i;
						nest[19+depth] = str.end;
						clear         = false;
						FontReader.ReadCFF2Subr(data, subrs, (uint)stack[--count], ref str);
						i = str.begin;
						break;
					case 0x0b:
						if(depth < 1)
							return false;
						str.end = nest[19+depth];
						i       = nest[--depth];
						clear   = false;
						break;
					case 0x0e:
						path.Finalize(true);   
						if(!indices.Contains(ind))
							indices.Add(ind);
						if(!characters.Contains(c))
							characters.Add(c);
						return true;
					case 0x13:
					case 0x14:
						if(header)
							bits += (count / 2);
						header = false;
						i += (bits + 7) / 8;
						break;
					case 0x15:
						header = false;
						if(count < 2)
							return false;
						path.MoveTo(path.lastPos.x + stack[count-2], path.lastPos.y + stack[count-1]);
						break;
					case 0x16:
						header = false;
						if(count < 1)
							return false;
						path.MoveTo(path.lastPos.x + stack[count-1], path.lastPos.y);
						break;
					case 0x18:
						if(count < 8)
							return false;
						for(j=0; j+5<count-2; j+=6)
							path.CubicCurveTo(path.lastPos.x + stack[j] + stack[j+2] + stack[j+4], path.lastPos.y + stack[j+1] + stack[j+3] + stack[j+5], path.lastPos.x + stack[j], path.lastPos.y + stack[j+1], path.lastPos.x + stack[j] + stack[j+2], path.lastPos.y + stack[j+1] + stack[j+3]);
						if(j+1 >= count)
							return false;
						path.LineTo(path.lastPos.x + stack[j], path.lastPos.y + stack[j+1]);
						break;
					case 0x19:
						if(count < 8)
							return false;
						for(j=0; j+1<count-6; j+=2)
							path.LineTo(path.lastPos.x + stack[j], path.lastPos.y + stack[j+1]);
						if(j+5 >= count)
							return false;
						path.CubicCurveTo(path.lastPos.x + stack[j] + stack[j+2] + stack[j+4], path.lastPos.y + stack[j+1] + stack[j+3] + stack[j+5], path.lastPos.x + stack[j], path.lastPos.y + stack[j+1], path.lastPos.x + stack[j] + stack[j+2], path.lastPos.y + stack[j+1] + stack[j+3]);
						break;
					case 0x1a:
						if(count < 4)
							return false;
						f = 0.0f;
						j = 0;
						if(0 != (count & 1))
							f = stack[j++];
						for(; j+3<count; j+=4)
						{
							path.CubicCurveTo(path.lastPos.x + f + stack[j+1], path.lastPos.y + stack[j] + stack[j+2] + stack[j+3], path.lastPos.x + f, path.lastPos.y + stack[j], path.lastPos.x + f + stack[j+1], path.lastPos.y + stack[j] + stack[j+2]);
							f = 0.0f;
						}
						break;
					case 0x1b:
						if(count < 4)
							return false;
						f = 0.0f;
						j = 0;
						if(0 != (count & 1))
							f = stack[j++];
						for(; j+3<count; j+=4)
						{
							path.CubicCurveTo(path.lastPos.x + stack[j] + stack[j+1] + stack[j+3], path.lastPos.y + f + stack[j+2], path.lastPos.x + stack[j], path.lastPos.y + f, path.lastPos.x + stack[j] + stack[j+1], path.lastPos.y + f + stack[j+2]);
							f = 0.0f;
						}
						break;
					case 0x1d:
						if(count < 1 || depth > 19)
							return false;
						nest[depth++] = i;
						nest[19+depth] = str.end;
						clear         = false;
						FontReader.ReadCFF2Subr(data, globalSubrs, (uint)stack[--count], ref str);
						i = str.begin;
						break;
					case 0x1e:
						if(count < 4)
							return false;
						for(j=0; ; )
						{
							if((j+3) >= count)
								break;
							path.CubicCurveTo(path.lastPos.x + stack[j+1] + stack[j+3], path.lastPos.y + stack[j] + stack[j+2] + ((count-j == 5) ? stack[j+4] : 0.0f), path.lastPos.x, path.lastPos.y + stack[j], path.lastPos.x + stack[j+1], path.lastPos.y + stack[j] + stack[j+2]);
							j += 4;
							if((j+3) >= count)
								break;
							path.CubicCurveTo(path.lastPos.x + stack[j] + stack[j+1] + ((count-j == 5) ? stack[j+4] : 0.0f), path.lastPos.y + stack[j+2] + stack[j+3], path.lastPos.x + stack[j], path.lastPos.y, path.lastPos.x + stack[j] + stack[j+1], path.lastPos.y + stack[j+2]);
							j += 4;
						}
						break;
					case 0x1f:
						if(count < 4)
							return false;
						for(j=0; ; )
						{
							if((j+3) >= count)
								break;
							path.CubicCurveTo(path.lastPos.x + stack[j] + stack[j+1] + ((count-j == 5) ? stack[j+4] : 0.0f), path.lastPos.y + stack[j+2] + stack[j+3], path.lastPos.x + stack[j], path.lastPos.y, path.lastPos.x + stack[j] + stack[j+1], path.lastPos.y + stack[j+2]);
							j += 4;
							if((j+3) >= count)
								break;
							path.CubicCurveTo(path.lastPos.x + stack[j+1] + stack[j+3], path.lastPos.y + stack[j] + stack[j+2] + ((count-j == 5) ? stack[j+4] : 0.0f), path.lastPos.x, path.lastPos.y + stack[j], path.lastPos.x + stack[j+1], path.lastPos.y + stack[j] + stack[j+2]);
							j += 4;
						}
						break;
					default:
						return false;
				}

				if(clear)
					count = 0;
			}

			return false;
		}

		public static bool LoadFont(string file, ref int units, ref Vector2 linespace, ref Vector2 wordspace, ref string fontname)
		{
			int    platform = -1;
			int    encoding = -1;
			int    temp1    = -1;
			uint   temp2    = 0; 
			uint   temp3    = 0;  
			uint   temp4    = 0;
			uint   i        = 0;
			uint   version  = 0;
			uint   count    = 0;
			uint   tag      = 0;
			uint   offset   = 0;
			uint   length   = 0;  
	
			Clear();

			if(File.Exists(file))
				data = File.ReadAllBytes(file);
			if(null == data)
				return false;

			version = FontReader.ReadUInt32(data, ref i);
			count   = FontReader.ReadUInt16(data, ref i);
			i      += 6;

			if(version != 0x00010000 && version != 0x74727565 && version != 0x4F54544F && version != 0x43464620)
				return false;

			for(uint n=0; n<count; ++n) 
			{
				tag    = FontReader.ReadUInt32(data, ref i);
				i     += 4;
				offset = FontReader.ReadUInt32(data, ref i);
				length = FontReader.ReadUInt32(data, ref i);

				switch(tag)
				{
					case 0x68656164:
						head = FontReader.ReadFontHeader(data, offset);
						break;
					case 0x68686561:
						hhea = FontReader.ReadFontMetricsHeader(data, offset); 
						break;
					case 0x76686561:
						vhea = FontReader.ReadFontMetricsHeader(data, offset);
						break;
					case 0x6D617870:
						maxp = FontReader.ReadFontMaximumProfile(data, offset);
						break;	
					case 0x636D6170:
						cmap = offset;
						break;
					case 0x6B65726E:
						kern = offset;
						break;
					case 0x676C7966:
						glyf = offset;
						break;
					case 0x6C6F6361:
						loca = offset;
						break;
					case 0x6E616D65:
						name = offset;
						break;
					case 0x686D7478:
						hmtx = offset;
						break;
					case 0x766D7478:
						vmtx = offset;
						break;
					case 0x43464620:
						cff = offset;
						break;
					default:
						break;
				}
			}

			if(null == head || null == maxp || 0 == cmap)
				return false;
			
			if(0 == cff && (0 == glyf || 0 == loca))
				return false;
			
			i = cmap + 2;
			count = FontReader.ReadUInt16(data, ref i);

			for(uint n=0; n<count; ++n)
			{
				platform = FontReader.ReadUInt16(data, ref i);
				encoding = FontReader.ReadUInt16(data, ref i);

				if(3 == platform && (1 == encoding || 10 == encoding))
				{
					characterMapOffset = FontReader.ReadUInt32(data, ref i);
					break;
				}
				else 
				{
					i += 4;
				}
			}

			if(0 == characterMapOffset)
			{
				i = cmap + 4;
				encoding = -1;

				for(uint n=0; n<count; ++n)
				{
					platform = FontReader.ReadUInt16(data, ref i);
					temp1    = FontReader.ReadUInt16(data, ref i);
					offset   = FontReader.ReadUInt32(data, ref i);

					if(0 == platform && encoding < temp1)
					{
						encoding = temp1;
						characterMapOffset = offset;
					}
				}
			}
			
			if(0 == characterMapOffset)
				return false;
			
			if(0 == glyf || 0 == loca)
			{
				CFF2Dict top  = default(CFF2Dict);
				uint     type = 2;
				uint     temp = 0;

				i = cff + data[cff+2];

				FontReader.ReadCFF2Index(data, ref i);
				FontReader.ReadCFF2Dict(data, i, 0, ref top);

				FontReader.ReadCFF2DictData(data, top, 106, ref type, ref temp);
				FontReader.ReadCFF2DictData(data, top, 17, ref charStrings, ref temp);
				FontReader.ReadCFF2DictData(data, top, 136, ref fontDictArray, ref temp);
				FontReader.ReadCFF2DictData(data, top, 137, ref fontDictSelect, ref temp);

				FontReader.ReadCFF2Index(data, ref i);
				FontReader.ReadCFF2Index(data, ref i);

				globalSubrs = i;
				subrs = FontReader.ReadCFF2Subrs(data, top, cff);
				charStrings += cff;

				if(2 != type)
					return false;
				if(cff == charStrings)
					return false;
                if(fontDictArray > 0 && 0 == fontDictSelect)
                    return false;
            }

			if(0 != kern)	
			{
				i = kern + 2;
				numKerningTables = FontReader.ReadUInt16(data, ref i);
			}

			units  = head.unitsPerEm;
			points = new FontPoint[maxp.maxPoints];
			
			Path p = new Path();
			LoadGlyph(' ', ref p, ref wordspace);
			
			if(null != hhea)
			{
				linespace.x = hhea.ascent - hhea.descent + hhea.lineGap;
				linespace.y = hhea.advanceMax + wordspace.x;
			}

			if(null != vhea)
			{
				linespace.y = vhea.ascent - vhea.descent + vhea.lineGap;
				if(null == hhea)
					linespace.x = vhea.advanceMax + wordspace.y;
			}	
			
			if(0 != name)
			{
				i = name + 2;
				count  = FontReader.ReadUInt16(data, ref i);
				offset = FontReader.ReadUInt16(data, ref i);

				for(uint n=0; n<count; ++n)
				{
					temp1  = FontReader.ReadUInt16(data, ref i);	  
					temp2  = FontReader.ReadUInt16(data, ref i);
					i += 2;
					temp3  = FontReader.ReadUInt16(data, ref i);	  
					length = FontReader.ReadUInt16(data, ref i);
					temp4  = FontReader.ReadUInt16(data, ref i);

					if(platform == temp1 && encoding == temp2 && 4 == temp3)
					{
						fontname = "";
						length += name + offset + temp4;

						for(uint j=name+offset+temp4; j<length; ++j)
						{
							if(0 != data[j])
								fontname += (char)data[j];
						}

						break;
					}
				}
			}

			return true;
		}
	
		public static bool LoadGlyph(char c, ref Path path, ref Vector2 advance)
		{ 
			uint i      = cmap + characterMapOffset;  
			uint g      = 0;
			uint n      = 0;  
			uint format = FontReader.ReadUInt16(data, ref i);

			if(0 == format && c < 0x00000100)
			{
				g = data[i+4+c];
			} 
			else if(4 == format)
			{
				i += 4;
				uint segmets  = FontReader.ReadUInt16(data, ref i);  
				uint range    = FontReader.ReadUInt16(data, ref i);  
				uint selector = FontReader.ReadUInt16(data, ref i);
				uint shift    = FontReader.ReadUInt16(data, ref i);

				shift /= 2;
				range /= 4;

				uint end    = i;
				uint start  = i + 2 + segmets;	 
				uint delta  = i + 2 + segmets * 2;
				uint offset = i + 2 + segmets * 3; 

				if(c > FontReader.ReadUInt16(data, end + shift*2))
					n += shift;

				for(uint j=selector; j>0; range/=2, --j)
				{  
					if(c > FontReader.ReadUInt16(data, end + (n + range - 1)*2))
						n += range;
				}

				if(c >= FontReader.ReadUInt16(data, start + n*2))
				{
					if(0 == FontReader.ReadUInt16(data, offset + n*2))
					{
						g = (uint)(c + FontReader.ReadInt16(data, delta + n*2)) & 0xffff;
					}
					else 
					{
						i = FontReader.ReadUInt16(data, offset + n*2);
						i = (uint)(i/2 + c - FontReader.ReadUInt16(data, start + n*2) + n);
						g = FontReader.ReadUInt16(data, offset + i*2);	 
					}
				}
			} 
			else if(6 == format)
			{
				i += 4;
				uint first = FontReader.ReadUInt16(data, ref i);
				uint count = FontReader.ReadUInt16(data, ref i);
				
				g = ((c >= first) || (c - first) < count) ? FontReader.ReadUInt16(data, i+(c-first)*2) : 0U;
			}
			else if(10 == format)
			{
				i += 10;
				uint start = FontReader.ReadUInt32(data, ref i);
				uint count = FontReader.ReadUInt32(data, ref i);

				g = ((c >= start) || (c - start) < count) ? FontReader.ReadUInt32(data, i+(c-start)*4) : 0U;
			}	
			else if(12 == format)
			{
				i += 10;
				uint count = FontReader.ReadUInt32(data, ref i);
				uint start = 0;
				uint end   = 0;
				uint min   = 0;
				uint mid   = 0;
				uint max   = count;

				while(min < max)	 
				{
					mid   = (min + max) >> 1;
					start = FontReader.ReadUInt32(data, i+mid*12);
					end   = FontReader.ReadUInt32(data, i+mid*12+4);

					if(c < start)
					{
						max = mid;
					}  
					else if(c > end)
					{
						min = mid + 1;
					}
					else
					{
						g = c - start + FontReader.ReadUInt32(data, i+mid*12+8);
						break;
					}
				}
			}
			else if(13 == format)
			{
				i += 10;
				uint count = FontReader.ReadUInt32(data, ref i);
				uint start = 0;
				uint end   = 0;
				uint min   = 0;
				uint mid   = 0;
				uint max   = count;

				while(min < max)
				{
					mid   = (min + max) >> 1;
					start = FontReader.ReadUInt32(data, i+mid*12);
					end   = FontReader.ReadUInt32(data, i+mid*12+4);

					if(c < start)
					{
						max = mid;
					}
					else if(c > end)
					{
						min = mid + 1;
					}
					else
					{
						g = FontReader.ReadUInt32(data, i+mid*12+8);
						break;
					}
				}
			}
			
			if(0 == g)
				return false;

			if(0 != hmtx && null != hhea && hhea.numOfLongMetrics > 0)
			{
				n         = hhea.numOfLongMetrics;
				advance.x = g < n ? FontReader.ReadUInt16(data, hmtx + g*4) : FontReader.ReadUInt16(data, hmtx + n*4 - 4);
				advance.y =	hhea.ascent - hhea.descent;
			}

			if(0 != vmtx && null != vhea && vhea.numOfLongMetrics > 0)
			{
				n         = vhea.numOfLongMetrics;
				advance.y = g < n ? FontReader.ReadUInt16(data, vmtx + g*4) : FontReader.ReadUInt16(data, vmtx + n*4 - 4);
				if(0 == hmtx)
					advance.x =	vhea.ascent - vhea.descent;
			}

			path.Clear();

			if(' ' == c)
			{
				if(!indices.Contains(g))
					indices.Add(g);
				if(!characters.Contains(c))
					characters.Add(c);

				return false;
			}

			if(0 == loca || 0 == glyf)
				return LoadCFF2Glyph(g, c, ref path);

			return LoadTTFGlyph(g, c, null, ref path);
		}

		public static void LoadKerningPairs(Dictionary<uint, Vector2> map, List<TextToolsKerningPair> list)
		{
			if(0 == kern || 0 == numKerningTables || indices.Count < 2 || characters.Count < 2)
				return;

			Vector2 value    = Vector2.zero;
			uint    it       = 0;
			uint    key      = 0;
			uint    table    = 0;
			uint    length   = 0;
			uint    count    = 0;
			uint    left     = 0;
			uint    right    = 0;
			int     v        = 0;
			int     vertical = 0;
			byte    format   = 0;
			byte    coverage = 0;

			for(int i=0, n=indices.Count; i<n; ++i)
			{
				for(int j=0; j<n; ++j)
				{  
					table = kern + 4;
					value = Vector2.zero;

					for(uint t=0; t<numKerningTables; ++t)
					{
						length   = FontReader.ReadUInt16(data, table+2);
						format   = data[table + 4];
						coverage = data[table + 5];
						vertical = (coverage & 0x0001);

						if(0 == format)
						{
							it    = table + 6;
							count = FontReader.ReadUInt16(data, ref it);
							it   += 6;

							for(uint p=0; p<count; ++p) 
							{
								left  = FontReader.ReadUInt16(data, ref it);
								right = FontReader.ReadUInt16(data, ref it);
								v     = FontReader.ReadInt16(data, ref it);
								
								if(indices[i] == left && indices[j] == right)	 
								{
									value[vertical] = v;
									break;
								} 
							} 
						}

						table += length; 
					}

					if(Vector2.zero != value)
					{
						key = (uint)(characters[j]<<16)|(uint)characters[i];

						map.Add(key, value);
						list.Add(new TextToolsKerningPair(characters[i], characters[j], value));
					}
				}
			}
		}
	}
}
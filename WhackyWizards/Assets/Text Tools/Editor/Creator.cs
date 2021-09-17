// Copyright (C) 2019 Alexander Klochkov - All Rights Reserved
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms


using UnityEditor;
using UnityEngine;

using System.Collections.Generic;


namespace texttools
{
	public static class TextToolsCreator
	{
		static List<Material> materialList    = new List<Material>();
		static List<Vector3>  positionList    = new List<Vector3>();
		static List<Vector3>  normalList      = new List<Vector3>();
		static List<Vector4>  tangentList     = new List<Vector4>();
		static List<Vector2>  uv0List         = new List<Vector2>();
		static List<Vector2>  uv2List         = new List<Vector2>();
		static List<Vector2>  uv3List         = new List<Vector2>();
		static List<Color32>  colorList       = new List<Color32>();

		static List<Vector3>  mulPositionList = new List<Vector3>();
		static List<Vector3>  mulNormalList   = new List<Vector3>();

		static List<int>[]    indexList       = new List<int>[3] { new List<int>(), new List<int>(), new List<int>() };

		static int             numSegments;
		static float           outlineWidth;
		static float           contourLength;


		static int GetMaterialIndex(Material material) 
		{
			for(int i=0; i<materialList.Count; ++i)		
			{
				if(material == materialList[i])
					return i;
			}

			materialList.Add(material);

			return materialList.Count-1;
		}

		static void Clear()
		{
			materialList.Clear();
			positionList.Clear();
			normalList.Clear();
			tangentList.Clear();   
			uv0List.Clear();	
			uv2List.Clear();
			uv3List.Clear();
			colorList.Clear();
			mulPositionList.Clear();
			mulNormalList.Clear();
			indexList[0].Clear();	
			indexList[1].Clear();
			indexList[2].Clear();
		}

		static void SetColors(Path path, Color32 tl, Color32 tr, Color32 bl, Color32 br) 
		{
			if(tl.r == tr.r && tl.g == tr.g && tl.b == tr.b && tl.a == tr.a && tl.r == bl.r && tl.g == bl.g && tl.b == bl.b && tl.a == bl.a && tl.r == br.r && tl.g == br.g && tl.b == br.b && tl.a == br.a)
				return;

			Color32 tc, bc;
			float   width  = 0.0f;
			float   height = 0.0f;
			float   x      = 0.0f;
			float   y      = 0.0f;
			float   left   = float.MaxValue;
			float   top    = float.MinValue;
			float   right  = float.MinValue;
			float   bottom = float.MaxValue;

			foreach(SubPath sp in path.subPathList) 
			{
				for(int i=0; i<sp.points.Length; ++i)
				{
					left   = (sp.points[i].x < left)   ? sp.points[i].x : left;
					top    = (sp.points[i].y > top)    ? sp.points[i].y : top;
					right  = (sp.points[i].x > right)  ? sp.points[i].x : right;
					bottom = (sp.points[i].y < bottom) ? sp.points[i].y : bottom;
				}
			}

			width  = right - left;
			height = top - bottom;

			for(int i=0; i<colorList.Count; ++i)
			{
				x =	(positionList[i].x - left)/width;
				y =	(positionList[i].y - bottom)/height;

				tc = Color32.Lerp(tl, tr, x);
				bc = Color32.Lerp(bl, br, x);

				colorList[i] = Color32.Lerp(bc, tc, y);
			}
		} 
		
		static void Face(Material material, Path path, Color32 color, ref Vector2 size, float depth)
		{	
			int   offset = positionList.Count;
			int   index  = GetMaterialIndex(material);
			float left   = float.MaxValue;
			float top    = float.MinValue;
			float right  = float.MinValue;
			float bottom = float.MaxValue;

			Triangulator.Triangulate(path);
  
			foreach(int i in Triangulator.indexList)
				indexList[index].Add(i + offset);
			
			foreach(Point pt in Triangulator.pointList)
			{
				left   = (pt.x < left)   ? pt.x : left;
				top    = (pt.y > top)    ? pt.y : top;
				right  = (pt.x > right)  ? pt.x : right;
				bottom = (pt.y < bottom) ? pt.y : bottom;

				positionList.Add(new Vector3(pt.x, pt.y, 0.0f));
				normalList.Add(Vector3.back);
				tangentList.Add(new Vector4(-1.0f, 0.0f, 0.0f, -1.0f));
				uv2List.Add(Vector2.one); 
				uv3List.Add(Vector2.one); 
				colorList.Add(color);
			}

			size.Set(right - left, top - bottom); 

			if(depth > 0.000001f)
			{

				for(int i=0; i<Triangulator.indexList.Count; i+=3)
				{
					indexList[index].Add(Triangulator.indexList[i+0] + positionList.Count);
					indexList[index].Add(Triangulator.indexList[i+2] + positionList.Count);
					indexList[index].Add(Triangulator.indexList[i+1] + positionList.Count);
				}

				foreach(Point pt in Triangulator.pointList)
				{
					positionList.Add(new Vector3(pt.x, pt.y, depth));
					normalList.Add(Vector3.forward);
					tangentList.Add(new Vector4(-1.0f, 0.0f, 0.0f, -1.0f));
					uv2List.Add(Vector2.one);
					uv3List.Add(Vector2.one);
					colorList.Add(color);
				}
			}

			for(int i=offset; i<positionList.Count; ++i)	 
				uv0List.Add(new Vector2((positionList[i].x - left)/size.x, (positionList[i].y - bottom)/size.y));
		}

		static void Side(Material material, Path path, Color32 color, float size, float depth) 
		{
			if(size < 0.000001f)
				return;

			Vector3 n0     = Vector3.zero;
			Vector3 n1     = Vector3.zero;
			Vector2 v      = Vector2.zero;
			float   length = 0.0f;
			int     offset = 0;
			int     index  = GetMaterialIndex(material);
			int     curr   = positionList.Count;
			int     next;
			int     prev;
			int     begin;
			int     count; 

			foreach(SubPath sp in path.subPathList) 
			{ 	 
				begin  = positionList.Count;
				count  = sp.points.Length;
				length = 0.0f;

				if(!sp.points[0].join)
				{
					positionList.Add(new Vector3(sp.points[0].x, sp.points[0].y, depth));
					normalList.Add(Vector3.zero);
					tangentList.Add(Vector4.zero);
					uv0List.Add(Vector2.zero);
					uv2List.Add(Vector2.one);
					uv3List.Add(Vector2.one);
					colorList.Add(color);
				}

				for(int i=0; i<count; ++i)
				{
					next = (i + 1) % count;
					prev = (i - 1 + count) % count;

					positionList.Add(new Vector3(sp.points[i].x, sp.points[i].y, depth));
					uv0List.Add(new Vector2(0.0f, length));
					uv2List.Add(Vector2.one);
					uv3List.Add(Vector2.one);
					colorList.Add(color);  
					
					if(sp.points[i].join)
					{	
						positionList.Add(new Vector3(sp.points[i].x, sp.points[i].y, depth)); 
				
						n0.Set(sp.points[i].y - sp.points[prev].y, sp.points[prev].x - sp.points[i].x, 0.0f);
						n1.Set(sp.points[next].y - sp.points[i].y, sp.points[i].x - sp.points[next].x, 0.0f);
						n0.Normalize();
						n1.Normalize();

						normalList.Add(n0);
						normalList.Add(n1);
						tangentList.Add(new Vector4(0.0f, 0.0f, -1.0f, -1.0f));
						tangentList.Add(new Vector4(0.0f, 0.0f, -1.0f, -1.0f));
						uv0List.Add(new Vector2(0.0f, length));
						uv2List.Add(Vector2.one);
						uv3List.Add(Vector2.one);
						colorList.Add(color);
					}
					else 
					{
						n0.Set(sp.points[next].y - sp.points[prev].y, sp.points[prev].x - sp.points[next].x, 0.0f);
						n0.Normalize();

						n1.Set(-n0.z, 0.0f, n0.x);
						n1.Normalize();

						normalList.Add(n0);
						tangentList.Add(new Vector4(0.0f, 0.0f, -1.0f, -1.0f));
					} 
			
					length += Mathf.Sqrt((sp.points[next].x - sp.points[i].x)*(sp.points[next].x - sp.points[i].x) + (sp.points[next].y - sp.points[i].y)*(sp.points[next].y - sp.points[i].y));
				}

				if(!sp.points[0].join)
				{
					normalList[begin]  = normalList[begin+1];
					tangentList[begin] = tangentList[begin+1];
				}

				for(int i=begin; i<uv0List.Count; ++i)
				{
					v = uv0List[i];
					v.y /= length;
					uv0List[i] = v;
				}

				v = uv0List[begin];
				v.y = 1.0f;
				uv0List[begin] = v;
			}

			depth += size;
			offset = positionList.Count - curr;  
			
			foreach(SubPath sp in path.subPathList)
			{
				begin = curr;
				count = sp.points.Length;

				for(int i=0; i<count; ++i)
				{
					positionList.Add(new Vector3(sp.points[i].x, sp.points[i].y, depth));
					normalList.Add(normalList[curr]);
					tangentList.Add(tangentList[curr]);
					uv0List.Add(new Vector2(1.0f, uv0List[curr].y));
					uv2List.Add(Vector2.one);
					uv3List.Add(Vector2.one);
					colorList.Add(color);
					
					if(sp.points[i].join || 0 == i)
					{
						++curr;
						positionList.Add(new Vector3(sp.points[i].x, sp.points[i].y, depth));
						normalList.Add(normalList[curr]);
						tangentList.Add(tangentList[curr]);
						uv0List.Add(new Vector2(1.0f, uv0List[curr].y));
						uv2List.Add(Vector2.one);
						uv3List.Add(Vector2.one);
						colorList.Add(color);
					}

					next = ((count-1) == i) ? begin : curr + 1;
					
					indexList[index].Add(curr);
					indexList[index].Add(next + offset);
					indexList[index].Add(curr + offset);

					indexList[index].Add(next + offset);
					indexList[index].Add(curr);
					indexList[index].Add(next);

					++curr;
				}
			}
		}

		static void AddVertex(int i, Color32 color, Point c, Vector2 n1, Vector2 n2, int count, float length)
		{
			Vector3 v = Vector3.zero;
			Vector2	t = Vector2.zero;
			float   l = 0.0f;

			v.Set(c.x + mulPositionList[i].x*n1.x, c.y + mulPositionList[i].y*n1.y, mulPositionList[i].z);
			positionList.Add(v);

			v.Set(mulNormalList[i].x*n2.x, mulNormalList[i].y*n2.y, mulNormalList[i].z);
			v.Normalize();
			normalList.Add(v);

			v.Set(n2.x*v.z, n2.y*v.z, -n2.y*v.y - n2.x*v.x);
			v.Normalize();
			tangentList.Add(new Vector4(v.x, v.y, v.z, -1.0f));
	
			if(count > 1)
			{
				l = Vector3.Distance(positionList[positionList.Count-1], positionList[positionList.Count-1-numSegments-1]);
				
				t = uv3List[uv3List.Count-1-numSegments];
				t.y = length;
				uv3List[uv3List.Count-1-numSegments] = t;  
				
				t = uv0List[uv0List.Count-1-numSegments];
				t.y = contourLength;
				uv0List[uv0List.Count-1-numSegments] = t;
			}

			if(l > 0.0f) 
			{
				t = uv2List[uv2List.Count-1-numSegments];
				t.y = l;
				uv2List[uv2List.Count-1-numSegments] = t; 
			}
	
			uv0List.Add(new Vector2((float)i/numSegments, contourLength));
			uv2List.Add(new Vector2(l, (0.0f == l) ? 1.0f : l));
			uv3List.Add(new Vector2(contourLength, length));
			colorList.Add(color);
		}

		static void Bevel(Path path, List<int> indices, Color32 color, Point c, Point n, Vector2 n1, Vector2 n2, int begin) 
		{ 	 
			Vector2 n3     = (n1 + n2)*0.5f;	 
			float   length = 0.0f; 
			int     offset;
			
			path.AddPoint(c.x + n1.x, c.y + n1.y, true); 
			length = path.Length; 
	
			for(int i=0; i<=numSegments; ++i)
				AddVertex(i, color, c, n1, n1, path.pointList.Count, length);
	
			offset = positionList.Count;  
			contourLength += length;
	
			for(int i=0; i<=numSegments; ++i)
				AddVertex(i, color, c, n1, n3, path.pointList.Count, length);
		
			path.AddPoint(c.x + n2.x, c.y + n2.y, true); 
			length = Vector2.Distance(n1, n2);
	
			for(int i=0; i<=numSegments; ++i)
				AddVertex(i, color, c, n2, n3, path.pointList.Count, length);
		  	
			for(int i=0; i<numSegments; ++i)
			{
				indices.Add(offset);   
				indices.Add(offset + numSegments + 1);
				indices.Add(offset + numSegments + 2);
		
				indices.Add(offset + numSegments + 2);
				indices.Add(offset + 1);   
				indices.Add(offset++);
			}	
	
			offset = positionList.Count; 
			contourLength += length;
	
			for(int i=0; i<=numSegments; ++i)
				AddVertex(i, color, c, n2, n2, path.pointList.Count, length);

			for(int i=0; i<numSegments; ++i)
			{
				indices.Add(offset);
				indices.Add(-1 == begin ? offset + numSegments + 1 : begin + i);
				indices.Add(-1 == begin ? offset + numSegments + 2 : begin + i + 1);

				indices.Add(-1 == begin ? offset + numSegments + 2 : begin + i + 1);
				indices.Add(offset + 1);
				indices.Add(offset++);
			}  	
		}

		static void Miter(Path path, List<int> indices, Color32 color, Point c, Point p, Point n, Vector2 n1, Vector2 n2, float limit, int begin, bool join)
		{
			Point p1     = default(Point);
			Point p2     = default(Point);
			Point p3     = default(Point);
			Point p4     = default(Point);
			float length = 0.0f;
			int   offset; 
			
			p1.x = p.x + n1.x;
			p1.y = p.y + n1.y;
			p2.x = c.x + n1.x;
			p2.y = c.y + n1.y;
			p3.x = n.x + n2.x;
			p3.y = n.y + n2.y;
			p4.x = c.x + n2.x;
			p4.y = c.y + n2.y; 
	
			float   den = (p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y);
			float   num = (p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x);	
			float   mul = num / den;
			Vector2 pi  = new Vector2(p1.x + mul * (p2.x - p1.x), p1.y + mul * (p2.y - p1.y));
			Vector2 n3  = new Vector2(pi.x - c.x, pi.y - c.y);   
			Vector2 n4  = join ? n2 : n3;

			if(n3.magnitude < (outlineWidth*limit))
			{
				path.AddPoint(pi.x, pi.y, join);	
				length = path.Length;
						
				for(int i=0; i<=numSegments; ++i)
					AddVertex(i, color, c, n3, n1, path.pointList.Count, length);
		
				offset = positionList.Count; 
				contourLength += length;
		
				for(int i=0; i<=numSegments; ++i)
					AddVertex(i, color, c, n3, n4, path.pointList.Count, length);
		
				for(int i=0; i<numSegments; ++i)
				{
					indices.Add(offset);
					indices.Add(-1 == begin ? offset + numSegments + 1 : begin + i);
					indices.Add(-1 == begin ? offset + numSegments + 2 : begin + i + 1);
			
					indices.Add(-1 == begin ? offset + numSegments + 2 : begin + i + 1);
					indices.Add(offset + 1);
					indices.Add(offset++);
				} 
			}
			else
			{
				Bevel(path, indices, color, c, n, n1, n2, begin);
			}
		}

		static void Round(Path path, List<int> indices, Color32 color, Point c, Point n, Vector2 n1, Vector2 n2, int begin)
		{	
			Vector2 n3; 
			int     offset;	
			float   length = 0.0f;
			float   a1     = Mathf.Atan2(n1.y, n1.x);
			float   a2     = Mathf.Atan2(n2.y, n2.x);
			float   da     = (a1 > a2) ? a2 - a1 + 6.2831852f : a2 - a1;
			int     segs   = Mathf.Clamp(Mathf.RoundToInt(da * outlineWidth / Path.approximationScale), 1, 16);
			float   m      = da / segs;
	
			path.AddPoint(c.x + n1.x, c.y + n1.y, true);  
			length = path.Length;
	
			for(int i=0; i<=numSegments; ++i)
				AddVertex(i, color, c, n1, n1, path.pointList.Count, length);
	
			offset = positionList.Count; 
			contourLength += length;
	
			for(int i=0; i<=numSegments; ++i)
				AddVertex(i, color, c, n1, n1, path.pointList.Count, length);
	
			for(int i=0; i<segs; ++i)
			{
				a1 += m;
				n3  = ((segs-1) == i) ? n2 : new Vector2(Mathf.Cos(a1) * outlineWidth, Mathf.Sin(a1) * outlineWidth);

				path.AddPoint(c.x + n3.x, c.y + n3.y, false);
				length = path.Length;
		
				for(int j=0; j<=numSegments; ++j)
					AddVertex(j, color, c, n3, n3, path.pointList.Count, length);
		
				for(int j=0; j<numSegments; ++j)
				{
					indices.Add(offset);
					indices.Add(offset + numSegments + 1);
					indices.Add(offset + numSegments + 2);

					indices.Add(offset + numSegments + 2);
					indices.Add(offset + 1);
					indices.Add(offset++);
				}

				offset = positionList.Count;  
				contourLength += length;
		
				for(int j=0; j<=numSegments; ++j)
					AddVertex(j, color, c, n3, n3, path.pointList.Count, length);
			}

			Point p = path.pointList[path.pointList.Count-1];
			p.join = true;
			path.pointList[path.pointList.Count-1] = p; 
	
			for(int i=0; i<numSegments; ++i)
			{
				indices.Add(offset);
				indices.Add(-1 == begin ? offset + numSegments + 1 : begin + i);
				indices.Add(-1 == begin ? offset + numSegments + 2 : begin + i + 1);

				indices.Add(-1 == begin ? offset + numSegments + 2 : begin + i + 1);
				indices.Add(offset + 1);
				indices.Add(offset++);
			} 
		}

		static void Join(Path path, List<int> indices, Color32 color, Point c, Point p, Point n, TextToolsJoin join, float limit, int begin)
		{  
			Vector2 n1    = new Vector2(c.y - p.y, p.x - c.x);
			Vector2 n2    = new Vector2(n.y - c.y, c.x - n.x);
			float   cross = (n.x - c.x)*(c.y - p.y) - (n.y - c.y)*(c.x - p.x);
			float   inner = ((n1.magnitude < n2.magnitude) ? n1.magnitude : n2.magnitude) / outlineWidth;
			
			if(inner < 1.01f)
				inner = 1.01f; 
	
			n1.Normalize();
			n2.Normalize();
			n1 *= outlineWidth;
			n2 *= outlineWidth;	 
		
			if(cross > 0.00001f)
				Miter(path, indices, color, c, p, n, n1, n2, inner, begin, c.join);
			else if(c.join && TextToolsJoin.Bevel == join)
				Bevel(path, indices, color, c, n, n1, n2, begin);
			else if(c.join && TextToolsJoin.Round == join)
				Round(path, indices, color, c, n, n1, n2, begin);	
			else if(cross < -0.00001f)
				Miter(path, indices, color, c, p, n, n1, n2, limit, begin, c.join);
		}
		
		static void Outline(Material material, Path path, Path result, Color32 color, TextToolsJoin join, int segments, float depth, float width, float limit)
		{
			if(width < 0.000001f)
				return;

			Vector2 v = Vector2.zero;
			Vector4 t = Vector4.zero;
			int     count;
			int     prev;
			int     next;
			int     begin;
			int     offset = positionList.Count;
			int     index  = GetMaterialIndex(material);
			float   length = 0.0f;
			bool    bevel  = segments > 0;
			
			if(segments < 1)
				segments = 1;

			float a = 1.5707963f / segments;

			numSegments  = segments;
			outlineWidth = width;	 

			if(numSegments > 1)
			{
				for(int i=0; i<=numSegments; ++i)
				{
					mulPositionList.Add(new Vector3(Mathf.Sin(a*i), Mathf.Sin(a*i), (1.0f - Mathf.Cos(a*i))*outlineWidth));
					mulNormalList.Add(new Vector3(Mathf.Sin(a*i), Mathf.Sin(a*i), -Mathf.Cos(a*i)*outlineWidth));  
				}
			}
			else if(!bevel)
			{
				mulPositionList.Add(Vector3.zero);	 
				mulPositionList.Add(new Vector3(1.0f, 1.0f, 0.0f));

				mulNormalList.Add(Vector3.back);
				mulNormalList.Add(Vector3.back); 	 
			}
			else
			{
				mulPositionList.Add(Vector3.zero);
				mulPositionList.Add(new Vector3(1.0f, 1.0f, outlineWidth));

				mulNormalList.Add(new Vector3(0.70710678f, 0.70710678f, -0.70710678f*outlineWidth));
				mulNormalList.Add(new Vector3(0.70710678f, 0.70710678f, -0.70710678f*outlineWidth)); 
			} 
			
			foreach(SubPath sp in path.subPathList)
			{
				count = sp.points.Length;
				begin = positionList.Count;  
				contourLength = 0.0f;
		
				for(int i=0; i<count; ++i)
				{
					prev = (i - 1 + count) % count;
					next = (i + 1) % count;

					Join(result, indexList[index], color, sp.points[i], sp.points[prev], sp.points[next], join, limit, (0 == next ? begin : -1));
				}  

				length = Vector3.Distance(positionList[begin+numSegments], positionList[positionList.Count-1]);
				contourLength += length;	  
						
				for(int i=0; i<=numSegments; ++i)
				{
					v = uv0List[begin+i];
					v.y = contourLength;
					uv0List[begin+i] = v;

					v.x = Vector3.Distance(positionList[positionList.Count-1-numSegments+i], positionList[begin+i]);
					v.y = v.x;
					uv2List[begin+i] = v;

					v.y = length;
					v.x = uv3List[uv3List.Count-1-numSegments+i].x;
					uv3List[begin+i] = v;

					v = uv2List[uv2List.Count-1-numSegments+i];
					v.y = uv2List[begin+i].y;
					uv2List[uv2List.Count-1-numSegments+i] = v;

					v = uv3List[uv3List.Count-1-numSegments+i];
					v.y = length;
					uv3List[uv3List.Count-1-numSegments+i] = v;
				} 
		
				for(int i=begin; i<uv0List.Count; ++i)
				{
					v = uv0List[i];
					v.y /= contourLength;
					
					uv0List[i]  = v;
					uv3List[i] /= contourLength;
				}

				result.Finalize(false);
			}

			count = indexList[index].Count;

			for(int i=0; i<count; i+=3)
			{
				indexList[index].Add(indexList[index][i+0]+positionList.Count);
				indexList[index].Add(indexList[index][i+2]+positionList.Count);
				indexList[index].Add(indexList[index][i+1]+positionList.Count);
			}

			count = positionList.Count;

			for(int i=0; i<count; ++i)
			{
				t.Set(positionList[i].x, positionList[i].y, depth - positionList[i].z, 0.0f);
				positionList.Add(t);

				t.Set(normalList[i].x, normalList[i].y, -normalList[i].z, 0.0f);
				normalList.Add(t);

				t.Set(-tangentList[i].x, -tangentList[i].y, tangentList[i].z, tangentList[i].w);
				tangentList.Add(t);

				v.Set(1.0f - uv0List[i].x, uv0List[i].y);
				uv0List.Add(v);

				uv2List.Add(uv2List[i]);
				uv3List.Add(uv3List[i]);
				colorList.Add(colorList[i]);
			}
		}

		public static void CreateGlyph(TextToolsFont asset, char c, Material face, Material side, Material outline, Color32 tl, Color32 tr, Color32 bl, Color32 br, TextToolsJoin join, float limit, float width, float extrude, float quality, int segments, bool tangents, bool colors, bool correction)
		{
			TextToolsGlyph    glyph = new TextToolsGlyph();
			Path[]            path  = new Path[3] { new Path(), new Path(), new Path() };
			Path.approximationScale = quality * quality;
			Vector2           size  = Vector2.zero;
			bool              bevel = width > 0.000001f && segments > 0;
			int               ind   = width > 0.000001f ? 2 : 1;
			float             depth = bevel ? extrude + width + width : extrude;

			if(!TextToolsLoader.LoadGlyph(c, ref path[0], ref glyph.advance))
				return;

			Clipper.Clip(ClipOp.UNION, path[0], null, path[1]);

			Clear();
			Outline(outline, path[1], path[2], tl, join, segments, depth, width, limit);
			Face(face, path[1], tl, ref size, depth);
			Side(side, path[ind], tl, extrude, bevel ? width : 0.0f);
			SetColors(path[ind], tl, tr, bl, br);

			glyph.id   = c;
			glyph.mesh = new Mesh();

			glyph.mesh.name         = (int)c + "_" + c + "_" + asset.name.Trim();
			glyph.mesh.vertices     = positionList.ToArray();
			glyph.mesh.normals      = normalList.ToArray();
			glyph.mesh.uv           = uv0List.ToArray();
			glyph.mesh.uv2          = correction ? uv2List.ToArray()     : null;
			glyph.mesh.uv3          = correction ? uv3List.ToArray()     : null;
			glyph.mesh.colors32     = colors     ? colorList.ToArray()   : null;
			glyph.mesh.tangents     = tangents   ? tangentList.ToArray() : null;
			glyph.mesh.subMeshCount = materialList.Count;

			for(int i=0; i<materialList.Count; ++i)
				glyph.mesh.SetTriangles(indexList[i].ToArray(), i);

			glyph.mesh.RecalculateBounds();

			if(width > 0.000001f)
			{
				glyph.advance.x = glyph.advance.x - size.x + glyph.mesh.bounds.size.x;
				glyph.advance.y = glyph.advance.y - size.y + glyph.mesh.bounds.size.y;
			}

			asset.AddGlyph(glyph);
		}
	}
}
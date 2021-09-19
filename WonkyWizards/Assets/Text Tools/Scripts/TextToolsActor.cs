// Copyright (C) 2019 Alexander Klochkov - All Rights Reserved
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms


using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

using System.Collections.Generic;


namespace texttools
{
	[System.Serializable]
	public enum TextToolsAlignment
	{
		LeftTop     = 0,
		Center      = 1,
		RightBottom = 2,
	}


	[System.Serializable]
	public enum TextToolsCollider
	{
		None = 0,
		Box  = 1,
		Mesh = 2
	}


	[ExecuteInEditMode]
	public class TextToolsActor : MonoBehaviour
	{
		[FormerlySerializedAs("textPath")]
		[SerializeField]
		List<Vector2> textPath = new List<Vector2>();

		[SerializeField]
		List<Vector3> positionList = new List<Vector3>();
		
		[SerializeField]
		List<Vector2> pointList = new List<Vector2>();
		
		[SerializeField]
		List<float> lengthList = new List<float>();
		
		[FormerlySerializedAs("sourceFont")]
        [SerializeField]
		TextToolsFont sourceFont = null;

		[FormerlySerializedAs("inputText")]
		[SerializeField]
		[TextArea(10, 10)]
		string inputText = "";

		[FormerlySerializedAs("fontSize")]
		[SerializeField]
		int fontSize = 72;

		[FormerlySerializedAs("characterSpace")]
		[SerializeField]
		float characterSpace = 0.0f;

		[FormerlySerializedAs("wordSpace")]
		[SerializeField]
		float wordSpace = 0.0f;

		[FormerlySerializedAs("lineSpace")]
		[SerializeField]
		float lineSpace = 0.0f;

		[FormerlySerializedAs("pathOffset")]
		[SerializeField]
		float pathOffset = 0.0f;

		[FormerlySerializedAs("textAlignment")]
		[SerializeField]
		TextToolsAlignment textAlignment = TextToolsAlignment.LeftTop;
		
		[FormerlySerializedAs("colliderType")]
		[SerializeField]
		TextToolsCollider colliderType = TextToolsCollider.None;

		[FormerlySerializedAs("lightProbes")]
		[SerializeField]
		LightProbeUsage lightProbes = LightProbeUsage.BlendProbes;

		[FormerlySerializedAs("castShadow")]
		[SerializeField]
		ShadowCastingMode castShadows = ShadowCastingMode.On;

		[FormerlySerializedAs("reflectText")]
		[SerializeField]
		bool reflectText = false;

		[FormerlySerializedAs("verticalText")]
		[SerializeField]
		bool verticalText = false;

		[FormerlySerializedAs("verticalPath")]
		[SerializeField]
		bool verticalPath = false;

		[FormerlySerializedAs("receiveShadows")]
		[SerializeField]
		bool receiveShadows = true;

		[FormerlySerializedAs("simulatePhysics")]
		[SerializeField]
		bool simulatePhysics = false;			 


#if UNITY_EDITOR

		public TextToolsFont SourceFont
		{
			get { return sourceFont; }
		}
		
		private void QuadraticCurve(float x1, float y1, float x2, float y2, float x3, float y3, int level)
		{
			if(level > 16)
				return;

			float x12  = (x1 + x2) * 0.5f;
			float y12  = (y1 + y2) * 0.5f;
			float x23  = (x2 + x3) * 0.5f;
			float y23  = (y2 + y3) * 0.5f;
			float x123 = (x12 + x23) * 0.5f;
			float y123 = (y12 + y23) * 0.5f;

			float dx = x3 - x1;
			float dy = y3 - y1;
			float d  = (x2 - x3)*dy - (y2 - y3)*dx;

			if((d * d) <= (dx*dx + dy*dy))
			{
				pointList.Add(new Vector2(x123, y123));
				return;
			}

			QuadraticCurve(x1, y1, x12, y12, x123, y123, level+1);
			QuadraticCurve(x123, y123, x23, y23, x3, y3, level+1);
		}

		private void PathLength()
		{
			float l = 0.0f;

			if(textPath.Count < 3)
				return;

			lengthList.Clear();
			pointList.Clear();
			pointList.Add(textPath[0]);

			for(int i=0, n=textPath.Count-2; i<n; i+=2)
			{
				QuadraticCurve(textPath[i].x, textPath[i].y, textPath[i+1].x, textPath[i+1].y, textPath[i+2].x, textPath[i+2].y, 0);
				pointList.Add(textPath[i+2]);
			}

			for(int i=0, n=pointList.Count-1; i<n; ++i)
			{
				l += (pointList[i+1] - pointList[i]).magnitude;
				lengthList.Add(l);
			} 
		}

		public void Apply(bool generate, bool clear, bool place, bool length, bool lighting, bool physics)
		{
			if(generate)
				GenerateText(clear);

			if(length)
				PathLength();

			if(place)
				PlaceText();

			if(lighting)
			{
				MeshRenderer mr = null;

				for(int i=0; i<transform.childCount; ++i)
				{
					mr = transform.GetChild(i).gameObject.GetComponent<MeshRenderer>();

					mr.lightProbeUsage   = lightProbes;
					mr.shadowCastingMode = castShadows;
					mr.receiveShadows    = receiveShadows;
				}
			}

			if(physics)
			{
				GameObject   go = null;
				MeshFilter   mf = null;
				MeshCollider mc = null;
				BoxCollider  bc = null;
				Rigidbody	 rb = null;

				for(int i=0; i<transform.childCount; ++i)
				{
					go = transform.GetChild(i).gameObject;
					mf = go.GetComponent<MeshFilter>();
					mc = go.GetComponent<MeshCollider>();
					bc = go.GetComponent<BoxCollider>();
					rb = go.GetComponent<Rigidbody>();

					if(TextToolsCollider.Box != colliderType && null != bc)
					{
						bc.enabled = false;
						DestroyImmediate(bc);
						bc = null;
					}

					if(TextToolsCollider.Mesh != colliderType && null != mc)
					{
						mc.enabled = false;
						DestroyImmediate(mc);
						mc = null;
					}

					if(!simulatePhysics && null != rb)
					{
						DestroyImmediate(rb);
						rb = null;
					}
					
					if(TextToolsCollider.Box == colliderType && null == bc)
					{
						bc = go.AddComponent<BoxCollider>();
						bc.center = mf.sharedMesh.bounds.center;
						bc.size   = mf.sharedMesh.bounds.size;
					}
					else if(TextToolsCollider.Mesh == colliderType && null == mc)
					{
						mc = go.AddComponent<MeshCollider>();
						mc.convex     = true;
						mc.sharedMesh = mf.sharedMesh;
					}

					if(simulatePhysics && null == rb)
					{
						rb = go.AddComponent<Rigidbody>();
						rb.detectCollisions	= (TextToolsCollider.None != colliderType);
					}
				}
			}
		}

#endif

		private void PlaceText()
		{	
			if(0 == transform.childCount || 0 == positionList.Count || 0 == lengthList.Count || pointList.Count < 2 || 0.0f == lengthList[lengthList.Count-1])
				return;

			GameObject go   = null;
			Vector3    pos  = Vector3.zero;
			float      prev = 0.0f;
			float      end  = lengthList[lengthList.Count-1]; 
			float      v;	
			float      a; 
			int        j;

			for(int i=0; i<transform.childCount; ++i)
			{
				prev = 0.0f;  
				go   = transform.GetChild(i).gameObject; 
				pos  = go.transform.localPosition;
				v    = positionList[i].x + pathOffset;

				while(v > end)
					v -= end;	
				while(v < 0.0f)
					v += end;	  
		
				for(j=0; j<lengthList.Count; ++j)
				{
					if(v < lengthList[j])
						break;
					prev = lengthList[j];
				}

				v = (v - prev)/(lengthList[j] - prev);	
				a = Mathf.Atan2((pointList[j+1].y - pointList[j].y), (pointList[j+1].x - pointList[j].x));
				
				pos.x = pointList[j].x * (1.0f - v) + pointList[j+1].x * v;
		
				if(verticalPath)
				{
					pos.y = pointList[j].y * (1.0f - v) + pointList[j+1].y * v;
					go.transform.localRotation = Quaternion.AngleAxis(a*Mathf.Rad2Deg, Vector3.forward);
				}
				else
				{	
					pos.z = pointList[j].y * (1.0f - v) + pointList[j+1].y * v;
					go.transform.localRotation = Quaternion.AngleAxis(-a*Mathf.Rad2Deg, Vector3.up); 
				}  
				
				go.transform.localPosition = pos;
			}
		}

		private void GenerateText(bool clear)
		{
			GameObject   go = null;
			MeshFilter   mf = null;
			MeshRenderer mr = null;
			MeshCollider mc = null;
			BoxCollider  bc = null;
			Rigidbody	 rb = null;

			if(clear)
			{
				for(int i=transform.childCount-1; i>=0; --i)
				{
					go = transform.GetChild(i).gameObject;
					mf = go.GetComponent<MeshFilter>();
					mr = go.GetComponent<MeshRenderer>();
					mc = go.GetComponent<MeshCollider>();
					bc = go.GetComponent<BoxCollider>();
					rb = go.GetComponent<Rigidbody>();

					mf.sharedMesh = null;
					mr.enabled    = false;

					if(Application.isPlaying)
					{
						if(null != bc)
						{
							bc.enabled = false;
							Destroy(bc);
						}

						if(null != mc)
						{
							mc.enabled = false;
							Destroy(mc);
						}

						if(null != rb)
							Destroy(rb);

						Destroy(mr);
						Destroy(mf);
						Destroy(go);
					}
					else
					{
						if(null != bc)
						{
							bc.enabled = false;
							DestroyImmediate(bc);
						}

						if(null != mc)
						{
							mc.enabled = false;
							DestroyImmediate(mc);
						}

						if(null != rb)
							DestroyImmediate(rb);
						
						DestroyImmediate(mr);
						DestroyImmediate(mf);
						DestroyImmediate(go);
					}
				}

				go = null;
				mf = null;
				mr = null;
				mc = null;
				bc = null;
				rb = null;
				Resources.UnloadUnusedAssets();
			}

			positionList.Clear();

			if(null == sourceFont || string.IsNullOrEmpty(inputText))
				return;

			TextToolsGlyph glyph     = null;
			int            ind       = verticalText ? 1 : 0;
			int            rit       = 0;
			int            it        = 0;
			int            begin     = 0;
			int            count     = 0;
			float          s         = (float)fontSize/(float)sourceFont.UnitsPerEm;
			float          ws        = verticalText ? -sourceFont.WordSpace[ind] * s - wordSpace : sourceFont.WordSpace[ind] * s + wordSpace;
			float          ls        = verticalText ? -sourceFont.LineSpace[ind] * s - lineSpace : sourceFont.LineSpace[ind] * s + lineSpace;
			float          cs        = 0.0f;
			Vector3        pos       = Vector3.zero;
			Vector3        offset1   = Vector3.zero;
			Vector3        offset2   = new Vector3(float.MaxValue, float.MaxValue, 0.0f);
			Vector3        scale     = new Vector3(s, s, s);
			Bounds         bounds1;
			Bounds         bounds2;
			char           curr;
			char           next;

			while(it < inputText.Length)
			{  
				pos[ind] = 0.0f;
				offset1  = Vector3.zero;
				begin    = count;

				if(reflectText)
				{
					rit = it;
					while(rit < inputText.Length && '\n' != inputText[rit])
						++rit;
					--rit;
				}

				while(it < inputText.Length && '\n' != inputText[it])
				{
					curr = inputText[it++];
					next = (inputText.Length != it) ? inputText[it] : '\0';
					
					if(reflectText)
					{
						curr = inputText[rit];
						next = (rit > 0) ? inputText[--rit] : '\0';
					}

					if(' ' == curr)
					{
						pos[ind] += ws;
						continue;
					}

					glyph = sourceFont.GetGlyph(curr);

					if(null == glyph)
						continue;

					if(clear)
					{
						go = new GameObject("C_" + curr + "_" + (count++));
						go.transform.parent = transform;

						mf = go.AddComponent<MeshFilter>();
						mr = go.AddComponent<MeshRenderer>();

						if(TextToolsCollider.Box == colliderType)
						{
							bc = go.AddComponent<BoxCollider>();
							bc.center = glyph.mesh.bounds.center;
							bc.size   = glyph.mesh.bounds.size;
						}
						else if(TextToolsCollider.Mesh == colliderType)
						{
							mc = go.AddComponent<MeshCollider>();
							mc.convex     = true;
							mc.sharedMesh = glyph.mesh;
						}

						if(simulatePhysics)
						{
							rb = go.AddComponent<Rigidbody>();
							rb.detectCollisions	= (TextToolsCollider.None != colliderType);
						}

						mf.sharedMesh = glyph.mesh;

						mr.sharedMaterials   = sourceFont.Materials;
						mr.lightProbeUsage   = lightProbes;
						mr.shadowCastingMode = castShadows;
						mr.receiveShadows    = receiveShadows;
					}
					else
					{
						go = transform.GetChild(count++).gameObject;
					}
					
					go.layer                   = gameObject.layer;
					go.isStatic                = gameObject.isStatic;
					go.transform.localRotation = Quaternion.identity;
					go.transform.localPosition = pos;
					go.transform.localScale    = scale;

					cs       = characterSpace + (glyph.advance[ind] + sourceFont.GetKerning(curr, next, verticalText)) * s;
					pos[ind] = verticalText ? pos[ind] - cs : pos[ind] + cs;
				}

				if(it < inputText.Length)
					++it;

				if(count > 0 && begin != count) 
				{
					bounds1 = transform.GetChild(begin).gameObject.GetComponent<MeshRenderer>().bounds;
					bounds2 = transform.GetChild(count-1).gameObject.GetComponent<MeshRenderer>().bounds;
 
					if(TextToolsAlignment.Center == textAlignment)
						offset1[ind] = (bounds1.min[ind] - bounds2.max[ind]) * 0.5f;
					else if(TextToolsAlignment.RightBottom == textAlignment)
						offset1[ind] = -pos[ind];

					for(int i=begin; i<count; ++i)
						transform.GetChild(i).gameObject.transform.localPosition += offset1;

					bounds1 = transform.GetChild(begin).gameObject.GetComponent<MeshRenderer>().bounds;
					bounds2 = transform.GetChild(count-1).gameObject.GetComponent<MeshRenderer>().bounds;

					if((bounds1.min.x - transform.position.x) < offset2.x)
						offset2.x = bounds1.min.x - transform.position.x;
					if((bounds2.min.y - transform.position.y) < offset2.y)
						offset2.y = bounds2.min.y - transform.position.y;  
				}

				pos[verticalText ? 0 : 1] -= ls;
			}

			for(int i=0; i<count; ++i)
			{
				go = transform.GetChild(i).gameObject;
				go.transform.localPosition -= offset2;
				positionList.Add(go.transform.localPosition);
			}

			PlaceText();
		}

		public void SetText(string text, bool update)
		{
			inputText = text;

			if(update)
				GenerateText(true);
		}

		public void SetFont(TextToolsFont font, bool update)
		{
			sourceFont = font;

			if(update)
				GenerateText(true);
		}

		public void SetTextAlignment(TextToolsAlignment alignment, bool update)
		{
			textAlignment = alignment;

			if(update)
				GenerateText(false);
		}
	}
}
// Copyright (C) 2019 Alexander Klochkov - All Rights Reserved
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms


using UnityEngine;
using UnityEngine.Serialization;

using System.Collections.Generic; 

#if UNITY_EDITOR  

using UnityEditor;

#endif


namespace texttools
{ 
#if UNITY_EDITOR

	[System.Serializable]
	public enum TextToolsJoin
	{
		Bevel = 0,
		Round = 1,
		Miter = 2
	}


	[System.Serializable]
	public enum TextToolsCharacterRangeType
	{
		ControlCharacter,
		BasicLatin,
		Latin1Supplement,
		LatinExtendedA,
		LatinExtendedB,
		IPAExtensions,
		SpacingModifierLetters,
		CombiningDiacriticalMarks,
		GreekAndCoptic,
		Cyrillic,
		CyrillicSupplementary,
		Armenian,
		Hebrew,
		Arabic,
		Syriac,
		ArabicSupplement,
		Thaana,
		NKo,
		Samaritan,
		Mandaic,
		ArabicExtendedA,
		Devanagari,
		Bengali,
		Gurmukhi,
		Gujarati,
		Oriya,
		Tamil,
		Telugu,
		Kannada,
		Malayalam,
		Sinhala,
		Thai,
		Lao,
		Tibetan,
		Myanmar,
		Georgian,
		HangulJamo,
		Ethiopic,
		EthiopicSupplement,
		Cherokee,
		UnifiedCanadianAboriginalSyllabics,
		Ogham,
		Runic,
		Tagalog,
		Hanunoo,
		Buhid,
		Tagbanwa,
		Khmer,
		Mongolian,
		UnifiedCanadianAboriginalSyllabicsExtended,
		Limbu,
		TaiLe,
		NewTaiLue,
		KhmerSymbols,
		Buginese,
		TaiTham,
		CombiningDiacriticalMarksExtended,
		Balinese,
		Sundanese,
		Batak,
		Lepcha,
		OlChiki,
		CyrillicExtendedC,
		SundaneseSupplement,
		VedicExtensions,
		PhoneticExtensions,
		PhoneticExtensionsSupplement,
		CombiningDiacriticalMarksSupplement,
		LatinExtendedAdditional,
		GreekExtended,
		GeneralPunctuation,
		SuperscriptsAndSubscripts,
		CurrencySymbols,
		CombiningDiacriticalMarksForSymbols,
		LetterlikeSymbols,
		NumberForms,
		Arrows,
		MathematicalOperators,
		MiscellaneousTechnical,
		ControlPictures,
		OpticalCharacterRecognition,
		EnclosedAlphanumerics,
		BoxDrawing,
		BlockElements,
		GeometricShapes,
		MiscellaneousSymbols,
		Dingbats,
		MiscellaneousMathematicalSymbolsA,
		SupplementalArrowsA,
		BraillePatterns,
		SupplementalArrowsB,
		MiscellaneousMathematicalSymbolsB,
		SupplementalMathematicalOperators,
		MiscellaneousSymbolsAndArrows,
		Glagolitic,
		LatinExtendedC,
		Coptic,
		GeorgianSupplement,
		Tifinagh,
		EthiopicExtended,
		CyrillicExtendedA,
		SupplementalPunctuation,
		CJKRadicalsSupplement,
		KangxiRadicals,
		IdeographicDescriptionCharacters,
		CJKSymbolsAndPunctuation,
		Hiragana,
		Katakana,
		Bopomofo,
		HangulCompatibilityJamo,
		Kanbun,
		BopomofoExtended,
		CJKStrokes,
		KatakanaPhoneticExtensions,
		EnclosedCJKLettersAndMonths,
		CJKCompatibility,
		CJKUnifiedIdeographsExtensionA,
		YijingHexagramSymbols,
		CJKUnifiedIdeographs,
		YiSyllables,
		YiRadicals,
		Lisu,
		Vai,
		CyrillicExtendedB,
		Bamum,
		ModifierToneLetters,
		LatinExtendedD,
		SylotiNagri,
		CommonIndicNumberForms,
		PhagsPa,
		Saurashtra,
		DevanagariExtended,
		KayahLi,
		Rejang,
		HangulJamoExtendedA,
		Javanese,
		MyanmarExtendedB,
		Cham,
		MyanmarExtendedA,
		TaiViet,
		MeeteiMayekExtensions,
		EthiopicExtendedA,
		LatinExtendedE,
		CherokeeSupplement,
		MeeteiMayek,
		HangulSyllables,
		HangulJamoExtendedB,
		HighSurrogates,
		HighPrivateUseSurrogates,
		LowSurrogates,
		PrivateUseArea,
		CJKCompatibilityIdeographs,
		AlphabeticPresentationForms,
		ArabicPresentationFormsA,
		VariationSelectors,
		VerticalForms,
		CombiningHalfMarks,
		CJKCompatibilityForms,
		SmallFormVariants,
		ArabicPresentationFormsB,
		HalfwidthAndFullwidthForms,
		Specials,
		LinearBSyllabary,
		LinearBIdeograms,
		AegeanNumbers,
		AncientGreekNumbers,
		AncientSymbols,
		PhaistosDisc,
		Lycian,
		Carian,
		CopticEpactNumbers,
		OldItalic,
		Gothic,
		OldPermic,
		Ugaritic,
		OldPersian,
		Deseret,
		Shavian,
		Osmanya,
		Osage,
		Elbasan,
		CaucasianAlbanian,
		LinearA,
		CypriotSyllabary,
		ImperialAramaic,
		Palmyrene,
		Nabataean,
		Hatran,
		Phoenician,
		Lydian,
		MeroiticHieroglyphs,
		MeroiticCursive,
		Kharoshthi,
		OldSouthArabian,
		OldNorthArabian,
		Manichaean,
		Avestan,
		InscriptionalParthian,
		InscriptionalPahlavi,
		PsalterPahlavi,
		OldTurkic,
		OldHungarian,
		RumiNumeralSymbols,
		Brahmi,
		Kaithi,
		SoraSompeng,
		Chakma,
		Mahajani,
		Sharada,
		SinhalaArchaicNumbers,
		Khojki,
		Multani,
		Khudawadi,
		Grantha,
		Newa,
		Tirhuta,
		Siddham,
		Modi,
		MongolianSupplement,
		Takri,
		Ahom,
		WarangCiti,
		PauCinHau,
		Bhaiksuki,
		Marchen,
		Cuneiform,
		CuneiformNumbersAndPunctuation,
		EarlyDynasticCuneiform,
		EgyptianHieroglyphs,
		AnatolianHieroglyphs,
		BamumSupplement,
		Mro,
		BassaVah,
		PahawhHmong,
		Miao,
		IdeographicSymbolsAndPunctuation,
		Tangut,
		TangutComponents,
		KanaSupplement,
		Duployan,
		ShorthandFormatControls,
		ByzantineMusicalSymbols,
		MusicalSymbols,
		AncientGreekMusicalNotation,
		TaiXuanJingSymbols,
		CountingRodNumerals,
		MathematicalAlphanumericSymbols,
		SuttonSignWriting,
		GlagoliticSupplement,
		MendeKikakui,
		Adlam,
		ArabicMathematicalAlphabeticSymbols,
		MahjongTiles,
		DominoTiles,
		PlayingCards,
		EnclosedAlphanumericSupplement,
		EnclosedIdeographicSupplement,
		MiscellaneousSymbolsAndPictographs,
		EmoticonsEmoji,
		OrnamentalDingbats,
		TransportAndMapSymbols,
		AlchemicalSymbols,
		GeometricShapesExtended,
		SupplementalArrowsC,
		SupplementalSymbolsAndPictographs,
		CJKUnifiedIdeographsExtensionB,
		CJKUnifiedIdeographsExtensionC,
		CJKUnifiedIdeographsExtensionD,
		CJKUnifiedIdeographsExtensionE,
		CJKCompatibilityIdeographsSupplement,
		Tags,
		VariationSelectorsSupplement,
		Custom
	}


	[System.Serializable]
	public struct TextToolsCharacterRange
	{
		public string                      custom;
		public char                        begin;
		public char                        end;
		public TextToolsCharacterRangeType type;

		public TextToolsCharacterRange(int b, int e, TextToolsCharacterRangeType t) { custom = ""; begin = (char)b; end = (char)e; type = t; }
	}

#endif

	
	[System.Serializable]
	public struct TextToolsKerningPair
	{
		public Vector2 value;
		public char    left;
		public char    right;

		public TextToolsKerningPair(char l, char r, Vector2 v) { value = v; left = l; right = r; }
	}


	[System.Serializable]
	public class TextToolsGlyph
	{
		public Mesh    mesh    = null;
		public Vector2 advance = Vector2.zero;
		public int     id      = 0;
	}


	[ExecuteInEditMode]
	public class TextToolsFont : ScriptableObject
	{
		[FormerlySerializedAs("glyphList")]
		[SerializeField]
		List<TextToolsGlyph> glyphList = null;

		[FormerlySerializedAs("kerningPairList")]
		[SerializeField]
		List<TextToolsKerningPair> kerningPairList = null;

		[FormerlySerializedAs("faceMaterial")]
		[SerializeField]
		Material faceMaterial = null;

		[FormerlySerializedAs("sideMaterial")]
		[SerializeField]
		Material sideMaterial = null;

		[FormerlySerializedAs("outlineMaterial")]
		[SerializeField]
		Material outlineMaterial = null;

		[FormerlySerializedAs("wordSpace")]
		[SerializeField]
		Vector2 wordSpace = Vector2.zero;

		[FormerlySerializedAs("lineSpace")]
		[SerializeField]
		Vector2 lineSpace = Vector2.zero;

		[FormerlySerializedAs("extrude")]
		[SerializeField]
		float extrude = 0.0f;

		[FormerlySerializedAs("outlineWidth")]
		[SerializeField]
		float outlineWidth = 0.0f;

		[FormerlySerializedAs("unitsPerEm")]
		[SerializeField]
		int unitsPerEm = 1;

		[FormerlySerializedAs("missingGlyph")]
		[SerializeField]
		int missingGlyph = 127;

#if UNITY_EDITOR

		[FormerlySerializedAs("characterRangeList")]
		[SerializeField]
		List<TextToolsCharacterRange> characterRangeList = new List<TextToolsCharacterRange>();

		[FormerlySerializedAs("meshCompression")]
		[SerializeField]
		ModelImporterMeshCompression meshCompression = ModelImporterMeshCompression.Off;

		[FormerlySerializedAs("outlineJoin")]
		[SerializeField]
		TextToolsJoin outlineJoin = TextToolsJoin.Bevel;

		[FormerlySerializedAs("topLeftColor")]
		[SerializeField]
		Color32 topLeftColor = Color.black;

		[FormerlySerializedAs("topRightColor")]
		[SerializeField]
		Color32 topRightColor = Color.black;

		[FormerlySerializedAs("bottomLeftColor")]
		[SerializeField]
		Color32 bottomLeftColor = Color.black;

		[FormerlySerializedAs("bottomRightColor")]
		[SerializeField]
		Color32 bottomRightColor = Color.black;

		[FormerlySerializedAs("assetPath")]
        [SerializeField]
        string assetPath = "";  	
		
		[FormerlySerializedAs("fontName")]
        [SerializeField]
		string fontName = "";

		[FormerlySerializedAs("curveQuality")]
        [SerializeField]
        float curveQuality = 1.0f;	 	 
		
		[FormerlySerializedAs("miterLimit")]
        [SerializeField]
		float miterLimit = 4.0f;
		
		[FormerlySerializedAs("bevelSegments")]
        [SerializeField]
        int bevelSegments = 0; 

		[FormerlySerializedAs("optimizeMesh")]
		[SerializeField]
		bool optimizeMesh = false;

		[FormerlySerializedAs("useTangents")]
		[SerializeField]
		bool useTangents = false;

		[FormerlySerializedAs("useColors")]
		[SerializeField]
		bool useColors = false;

		[FormerlySerializedAs("perspectiveCorrection")]
		[SerializeField]
		bool uvCorrection = false;

		TextToolsGlyph[]              previewGlyphs         = null;
		Vector3[]                     previewPositions      = null;
		string                        infoString            = "";

        List<TextToolsCharacterRange> oldCharacterRangeList = new List<TextToolsCharacterRange>();
        ModelImporterMeshCompression  oldMeshCompression;
        TextToolsJoin                 oldOutlineJoin;
        Material                      oldFaceMaterial;
        Material                      oldSideMaterial;
        Material                      oldOutlineMaterial;
        Color32                       oldTopLeftColor;
        Color32                       oldTopRightColor;
        Color32                       oldBottomLeftColor;
        Color32                       oldBottomRightColor;
        string                        oldAssetPath;
        string                        oldFontName;
        float                         oldCurveQuality;
        float                         oldMiterLimit;
        float                         oldExtrude;
        float                         oldOutlineWidth;
        int                           oldBevelSegments;
        int                           oldMissingGlyph;
        bool                          oldOptimizeMesh;
        bool                          oldUseTangents;
        bool                          oldUseColors;
        bool                          olduvCorrection;
        bool                          snapShot = false;

#endif

        Dictionary<int, TextToolsGlyph> glyphMap       = null;
		Dictionary<uint, Vector2>       kerningPairMap = null;
		Material[]                      materials      = null;
		


		public Material[] Materials
		{
			get 
			{
				if(null == materials)
				{
					int count = 1;

					if(outlineWidth > 0.00001f && outlineMaterial != faceMaterial)
						++count;

					if(extrude > 0.000001f && sideMaterial != faceMaterial)
					{
						if(outlineWidth > 0.00001f)
						{
							if(sideMaterial != outlineMaterial)
								++count;
						}
						else 
						{
							++count;
						}
					}

					materials = new Material[count];
					count     = 0;

					if(outlineWidth > 0.000001f && outlineMaterial != faceMaterial)
						materials[count++] = outlineMaterial;

					materials[count] = faceMaterial;

					if(extrude > 0.000001f && sideMaterial != faceMaterial)
					{
						if(outlineWidth > 0.00001f)
						{
							if(sideMaterial != outlineMaterial)
								materials[++count] = sideMaterial;
						}
						else
						{
							materials[++count] = sideMaterial;
						}
					}
				}

				return materials; 
			}
		}

		public int UnitsPerEm
		{
			get { return unitsPerEm; }
		}

		public Vector2 WordSpace
		{
			get { return wordSpace; }
		}

		public Vector2 LineSpace
		{
			get { return lineSpace; }
		}


#if UNITY_EDITOR 
		
		public delegate bool LoadFontDelegate         (string file, ref int units, ref Vector2 linespace, ref Vector2 wordspace, ref string fontname);
		public delegate void LoadKerningPairsDelegate (Dictionary<uint, Vector2> map, List<TextToolsKerningPair> list);
		public delegate void CreateGlyphDelegate      (TextToolsFont asset, char c, Material face, Material side, Material outline, Color32 tl, Color32 tr, Color32 bl, Color32 br, TextToolsJoin join, float limit, float width, float extrude, float quality, int segments, bool tangents, bool colors, bool correction);

		public TextToolsGlyph[] PreviewGlyphs
		{
			get
			{
				if(null == previewGlyphs && !string.IsNullOrEmpty(fontName))
				{
					Vector3 pos    = Vector3.zero;
					Vector3 min    = Vector3.zero;
					Vector3 max    = Vector3.zero;
					Vector3 offset = Vector3.zero;
					float   left   = float.MaxValue;
					float   top    = float.MinValue;
					float   right  = float.MinValue;
					float   bottom = float.MaxValue;
					int     count  = 0;
					int     begin  = 0;
					char    curr;
					char    next;
   
					previewGlyphs    = new TextToolsGlyph[fontName.Length];
					previewPositions = new Vector3[fontName.Length];

					for(int i=0; i<fontName.Length; ++i)
					{
						previewGlyphs[i]    = null;
						previewPositions[i] = Vector3.zero;
					}

					for(int i=0; i<fontName.Length; ++i)
					{
						pos.x = 0.0f;
						begin = count;

						while(i<fontName.Length && ' ' != fontName[i] && '\n' != fontName[i])
						{
							curr = fontName[i++];
							next = (fontName.Length != i) ? fontName[i] : '\0';
							
							previewGlyphs[count]    = GetGlyph(curr);
							previewPositions[count] = pos;

							if(null != previewGlyphs[count])
							{
								top    = ((previewGlyphs[count].mesh.bounds.max.y + pos.y) > top)    ? previewGlyphs[count].mesh.bounds.max.y + pos.y : top;
								bottom = ((previewGlyphs[count].mesh.bounds.min.y + pos.y) < bottom) ? previewGlyphs[count].mesh.bounds.min.y + pos.y : bottom;
								pos.x += previewGlyphs[count++].advance.x + GetKerning(curr, next, false);
							}
						}

						if(count > 0 && begin != count) 
						{
							min      = previewGlyphs[begin].mesh.bounds.min;
							max      = previewGlyphs[count-1].mesh.bounds.max;
							offset.x = (max.x + previewPositions[count-1].x - min.x - previewPositions[begin].x) * 0.5f;

							for(int j=begin; j<count; ++j)
								previewPositions[j].x -= offset.x;

							left  = ((min.x + previewPositions[begin].x)   < left)  ? min.x + previewPositions[begin].x   : left;
							right = ((max.x + previewPositions[count-1].x) > right) ? max.x + previewPositions[count-1].x : right;
						}

						pos.y -= lineSpace.x;
					}

					offset.Set(0.0f, (top + bottom) * 0.5f, (((right - left) * 0.5f)/0.26794919243112270647255365849413f) * 1.15f + extrude);

					for(int i=0; i<count; ++i)
						previewPositions[i] -= offset;
				}
 
				return previewGlyphs; 
			}
		}

		public Vector3[] PreviewPositions
		{
			get { return previewPositions; }
		}

		public string InfoString
		{
			get 
			{
				if(null != glyphList && glyphList.Count > 0 && string.IsNullOrEmpty(infoString))
				{
					int vertices   = 0;
					int primitives = 0;

					foreach(TextToolsGlyph g in glyphList)
					{
						vertices   += g.mesh.vertexCount;
						primitives += g.mesh.triangles.Length;
					}

					infoString = fontName + " " + vertices + " verts, " + (primitives/3) + " tris";

					if(glyphList[0].mesh.subMeshCount > 1)
						infoString += ", " + glyphList[0].mesh.subMeshCount + " submeshes";

					infoString += " uv";

					if(uvCorrection)
						infoString += ",uv2,uv3";
					
					if(useColors)
						infoString += ",colors";
				}

				return infoString; 
			}
		}

        public void CreateSnapshot()
        {
            if(snapShot)
                return;

            oldCharacterRangeList.Clear();
            oldCharacterRangeList.AddRange(characterRangeList);

            oldMeshCompression  = meshCompression;
            oldOutlineJoin      = outlineJoin;
            oldFaceMaterial     = faceMaterial;
            oldSideMaterial     = sideMaterial;
            oldOutlineMaterial  = outlineMaterial;
            oldTopLeftColor     = topLeftColor;
            oldTopRightColor    = topRightColor;
            oldBottomLeftColor  = bottomLeftColor;
            oldBottomRightColor = bottomRightColor;
            oldAssetPath        = assetPath;
            oldFontName         = fontName;
            oldCurveQuality     = curveQuality;
            oldMiterLimit       = miterLimit;
            oldExtrude          = extrude;
            oldOutlineWidth     = outlineWidth;
            oldBevelSegments    = bevelSegments;
            oldMissingGlyph     = missingGlyph;
            oldOptimizeMesh     = optimizeMesh;
            oldUseTangents      = useTangents;
            oldUseColors        = useColors;
            olduvCorrection     = uvCorrection;
            snapShot            = true;
        }

        public void Revert()
        {
            characterRangeList.Clear();
            characterRangeList.AddRange(oldCharacterRangeList);

            meshCompression  = oldMeshCompression;
            outlineJoin      = oldOutlineJoin;
            faceMaterial     = oldFaceMaterial;
            sideMaterial     = oldSideMaterial;
            outlineMaterial  = oldOutlineMaterial;
            topLeftColor     = oldTopLeftColor;
            topRightColor    = oldTopRightColor;
            bottomLeftColor  = oldBottomLeftColor;
            bottomRightColor = oldBottomRightColor;
            assetPath        = oldAssetPath;
            fontName         = oldFontName;
            curveQuality     = oldCurveQuality;
            miterLimit       = oldMiterLimit;
            extrude          = oldExtrude;
            outlineWidth     = oldOutlineWidth;
            bevelSegments    = oldBevelSegments;
            missingGlyph     = oldMissingGlyph;
            optimizeMesh     = oldOptimizeMesh;
            useTangents      = oldUseTangents;
            useColors        = oldUseColors;
            uvCorrection     = olduvCorrection;
        }

        public void Apply(LoadFontDelegate load, LoadKerningPairsDelegate kerning, CreateGlyphDelegate create)
		{
			Object[]             assets;
			TextToolsActor[]     actors = Object.FindObjectsOfType<TextToolsActor>();
			List<TextToolsActor> list   = new List<TextToolsActor>();
			bool                 found  = true;

			previewPositions = null;
			previewGlyphs    = null;
			materials        = null;
			fontName         = "";
			infoString		 = "";
            snapShot         = false;

            if(null == glyphMap)
				glyphMap = new Dictionary<int, TextToolsGlyph>();

			if(null == kerningPairMap)
				kerningPairMap = new Dictionary<uint, Vector2>();
			
			if(null == glyphList)
				glyphList = new List<TextToolsGlyph>();
			
			if(null == kerningPairList)
				kerningPairList = new List<TextToolsKerningPair>();

			glyphMap.Clear();
			kerningPairMap.Clear();
			glyphList.Clear();
			kerningPairList.Clear();

			foreach(TextToolsActor a in actors)
			{
				if(a.SourceFont == this)
				{
					a.SetFont(null, true);
					list.Add(a);
				}
			}

			while(found)
			{
				assets = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this));
				found  = false;

				foreach(Object a in assets)
				{
					if(a is Mesh)
					{
						DestroyImmediate(a, true);
						found = true;
					}
				}
			}

			if(!load(assetPath, ref unitsPerEm, ref lineSpace, ref wordSpace, ref fontName))
				return;

			foreach(TextToolsCharacterRange r in characterRangeList) 
			{
				if(string.IsNullOrEmpty(r.custom))
				{
					for(char i=r.begin; i<=r.end; ++i)
					{
						if(!glyphMap.ContainsKey(i))
							create(this, i, faceMaterial, sideMaterial, outlineMaterial, topLeftColor, topRightColor, bottomLeftColor, bottomRightColor, outlineJoin, miterLimit, outlineWidth, extrude, curveQuality, bevelSegments, useTangents, useColors, uvCorrection);
					}
				}
				else
				{
					for(int i=0; i<r.custom.Length; ++i)
					{
						if(!glyphMap.ContainsKey(r.custom[i]))
							create(this, r.custom[i], faceMaterial, sideMaterial, outlineMaterial, topLeftColor, topRightColor, bottomLeftColor, bottomRightColor, outlineJoin, miterLimit, outlineWidth, extrude, curveQuality, bevelSegments, useTangents, useColors, uvCorrection);
					}
				}
			}

			if(!glyphMap.ContainsKey((char)missingGlyph))
				create(this, (char)missingGlyph, faceMaterial, sideMaterial, outlineMaterial, topLeftColor, topRightColor, bottomLeftColor, bottomRightColor, outlineJoin, miterLimit, outlineWidth, extrude, curveQuality, bevelSegments, useTangents, useColors, uvCorrection);

			kerning(kerningPairMap, kerningPairList);
			
			foreach(TextToolsActor a in list)
				a.SetFont(this, true);

            CreateSnapshot();
            list.Clear();
			EditorUtility.SetDirty(this);
		}

		public void AddGlyph(TextToolsGlyph glyph)
		{
			glyphMap.Add(glyph.id, glyph);
			glyphList.Add(glyph);

			if(optimizeMesh)
				MeshUtility.Optimize(glyph.mesh);

			MeshUtility.SetMeshCompression(glyph.mesh, meshCompression);
			AssetDatabase.AddObjectToAsset(glyph.mesh, this);
		}
  
#endif	

		private void LoadFontData() 
		{
			uint key;

			glyphMap       = new Dictionary<int, TextToolsGlyph>();
			kerningPairMap = new Dictionary<uint, Vector2>();

			foreach(TextToolsGlyph g in glyphList) 
			{
				if(!glyphMap.ContainsKey(g.id))
					glyphMap.Add(g.id, g);
			}

			foreach(TextToolsKerningPair p in kerningPairList)
			{
				key = (uint)(p.right<<16)|(uint)p.left;
				
				if(!kerningPairMap.ContainsKey(key)) 
					kerningPairMap.Add(key, p.value);
			}
		} 
		
		public TextToolsGlyph GetGlyph(char c)
		{
			if(null == glyphMap)
				LoadFontData();

			if(glyphMap.ContainsKey(c)) 
				return glyphMap[c];
			else if(glyphMap.ContainsKey(missingGlyph))
				return glyphMap[missingGlyph];

			return null;
		}

		public float GetKerning(char left, char right, bool vertical)
		{
			uint key = (uint)(right<<16)|(uint)left;

			if(null == kerningPairMap)
				LoadFontData();

			if(kerningPairMap.ContainsKey(key))
				return vertical ? kerningPairMap[key].y : kerningPairMap[key].x;

			return 0.0f;
		}
	}
}
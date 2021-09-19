// Copyright (C) 2019 Alexander Klochkov - All Rights Reserved
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms


using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using UnityEditorInternal;

using System.IO;


namespace texttools
{
	[CanEditMultipleObjects]
    [CustomEditor(typeof(TextToolsFont))]
    public class AssetInspector : Editor
	{
		static private TextToolsCharacterRange[] ranges = new TextToolsCharacterRange[] 
		{ 
			new TextToolsCharacterRange(0x0000, 0x001f, TextToolsCharacterRangeType.ControlCharacter),	 
			new TextToolsCharacterRange(0x0020, 0x007f, TextToolsCharacterRangeType.BasicLatin), 
			new TextToolsCharacterRange(0x0080, 0x00ff, TextToolsCharacterRangeType.Latin1Supplement),	
			new TextToolsCharacterRange(0x0100, 0x017f, TextToolsCharacterRangeType.LatinExtendedA),
			new TextToolsCharacterRange(0x0180, 0x024f, TextToolsCharacterRangeType.LatinExtendedB),
			new TextToolsCharacterRange(0x0250, 0x02af, TextToolsCharacterRangeType.IPAExtensions),
			new TextToolsCharacterRange(0x02b0, 0x02ff, TextToolsCharacterRangeType.SpacingModifierLetters),
			new TextToolsCharacterRange(0x0300, 0x036f, TextToolsCharacterRangeType.CombiningDiacriticalMarks),
			new TextToolsCharacterRange(0x0370, 0x03ff, TextToolsCharacterRangeType.GreekAndCoptic),
			new TextToolsCharacterRange(0x0400, 0x04ff, TextToolsCharacterRangeType.Cyrillic),
			new TextToolsCharacterRange(0x0500, 0x052f, TextToolsCharacterRangeType.CyrillicSupplementary),
			new TextToolsCharacterRange(0x0530, 0x058f, TextToolsCharacterRangeType.Armenian),
			new TextToolsCharacterRange(0x0590, 0x05ff, TextToolsCharacterRangeType.Hebrew),
			new TextToolsCharacterRange(0x0600, 0x06ff, TextToolsCharacterRangeType.Arabic),
			new TextToolsCharacterRange(0x0700, 0x074f, TextToolsCharacterRangeType.Syriac),
			new TextToolsCharacterRange(0x0750, 0x077f, TextToolsCharacterRangeType.ArabicSupplement),
			new TextToolsCharacterRange(0x0780, 0x07bf, TextToolsCharacterRangeType.Thaana),
			new TextToolsCharacterRange(0x07c0, 0x07ff, TextToolsCharacterRangeType.NKo),
			new TextToolsCharacterRange(0x0800, 0x083f, TextToolsCharacterRangeType.Samaritan),
			new TextToolsCharacterRange(0x0840, 0x085f, TextToolsCharacterRangeType.Mandaic),
			new TextToolsCharacterRange(0x08a0, 0x08ff, TextToolsCharacterRangeType.ArabicExtendedA),
			new TextToolsCharacterRange(0x0900, 0x097f, TextToolsCharacterRangeType.Devanagari),
			new TextToolsCharacterRange(0x0980, 0x09ff, TextToolsCharacterRangeType.Bengali),
			new TextToolsCharacterRange(0x0a00, 0x0a7f, TextToolsCharacterRangeType.Gurmukhi),
			new TextToolsCharacterRange(0x0a80, 0x0aff, TextToolsCharacterRangeType.Gujarati),
			new TextToolsCharacterRange(0x0b00, 0x0b7f, TextToolsCharacterRangeType.Oriya),
			new TextToolsCharacterRange(0x0b80, 0x0bff, TextToolsCharacterRangeType.Tamil),
			new TextToolsCharacterRange(0x0c00, 0x0c7f, TextToolsCharacterRangeType.Telugu),
			new TextToolsCharacterRange(0x0c80, 0x0cff, TextToolsCharacterRangeType.Kannada),
			new TextToolsCharacterRange(0x0d00, 0x0d7f, TextToolsCharacterRangeType.Malayalam),
			new TextToolsCharacterRange(0x0d80, 0x0dff, TextToolsCharacterRangeType.Sinhala),
			new TextToolsCharacterRange(0x0e00, 0x0e7f, TextToolsCharacterRangeType.Thai),
			new TextToolsCharacterRange(0x0e80, 0x0eff, TextToolsCharacterRangeType.Lao),
			new TextToolsCharacterRange(0x0f00, 0x0fff, TextToolsCharacterRangeType.Tibetan),
			new TextToolsCharacterRange(0x1000, 0x109f, TextToolsCharacterRangeType.Myanmar),
			new TextToolsCharacterRange(0x10a0, 0x10ff, TextToolsCharacterRangeType.Georgian),
			new TextToolsCharacterRange(0x1100, 0x11ff, TextToolsCharacterRangeType.HangulJamo),
			new TextToolsCharacterRange(0x1200, 0x137f, TextToolsCharacterRangeType.Ethiopic),
			new TextToolsCharacterRange(0x1380, 0x139f, TextToolsCharacterRangeType.EthiopicSupplement),
			new TextToolsCharacterRange(0x13a0, 0x13ff, TextToolsCharacterRangeType.Cherokee),
			new TextToolsCharacterRange(0x1400, 0x167f, TextToolsCharacterRangeType.UnifiedCanadianAboriginalSyllabics),
			new TextToolsCharacterRange(0x1680, 0x169f, TextToolsCharacterRangeType.Ogham),
			new TextToolsCharacterRange(0x16a0, 0x16ff, TextToolsCharacterRangeType.Runic),
			new TextToolsCharacterRange(0x1700, 0x171f, TextToolsCharacterRangeType.Tagalog),
			new TextToolsCharacterRange(0x1720, 0x173f, TextToolsCharacterRangeType.Hanunoo),
			new TextToolsCharacterRange(0x1740, 0x175f, TextToolsCharacterRangeType.Buhid),
			new TextToolsCharacterRange(0x1760, 0x177f, TextToolsCharacterRangeType.Tagbanwa),
			new TextToolsCharacterRange(0x1780, 0x17ff, TextToolsCharacterRangeType.Khmer),
			new TextToolsCharacterRange(0x1800, 0x18af, TextToolsCharacterRangeType.Mongolian),
			new TextToolsCharacterRange(0x18b0, 0x18ff, TextToolsCharacterRangeType.UnifiedCanadianAboriginalSyllabicsExtended),
			new TextToolsCharacterRange(0x1900, 0x194f, TextToolsCharacterRangeType.Limbu),
			new TextToolsCharacterRange(0x1950, 0x197f, TextToolsCharacterRangeType.TaiLe),
			new TextToolsCharacterRange(0x1980, 0x19Df, TextToolsCharacterRangeType.NewTaiLue),
			new TextToolsCharacterRange(0x19e0, 0x19ff, TextToolsCharacterRangeType.KhmerSymbols),
			new TextToolsCharacterRange(0x1a00, 0x1a1f, TextToolsCharacterRangeType.Buginese),
			new TextToolsCharacterRange(0x1a20, 0x1aaf, TextToolsCharacterRangeType.TaiTham),
			new TextToolsCharacterRange(0x1ab0, 0x1aff, TextToolsCharacterRangeType.CombiningDiacriticalMarksExtended),
			new TextToolsCharacterRange(0x1b00, 0x1b7f, TextToolsCharacterRangeType.Balinese),
			new TextToolsCharacterRange(0x1b80, 0x1bbf, TextToolsCharacterRangeType.Sundanese),
			new TextToolsCharacterRange(0x1bc0, 0x1bff, TextToolsCharacterRangeType.Batak),
			new TextToolsCharacterRange(0x1c00, 0x1c4f, TextToolsCharacterRangeType.Lepcha),
			new TextToolsCharacterRange(0x1c50, 0x1c7f, TextToolsCharacterRangeType.OlChiki),
			new TextToolsCharacterRange(0x1c80, 0x1c87, TextToolsCharacterRangeType.CyrillicExtendedC),
			new TextToolsCharacterRange(0x1cc0, 0x1ccf, TextToolsCharacterRangeType.SundaneseSupplement),
			new TextToolsCharacterRange(0x1cd0, 0x1cff, TextToolsCharacterRangeType.VedicExtensions),
			new TextToolsCharacterRange(0x1d00, 0x1d7f, TextToolsCharacterRangeType.PhoneticExtensions),
			new TextToolsCharacterRange(0x1d80, 0x1dbf, TextToolsCharacterRangeType.PhoneticExtensionsSupplement),
			new TextToolsCharacterRange(0x1dc0, 0x1dff, TextToolsCharacterRangeType.CombiningDiacriticalMarksSupplement),
			new TextToolsCharacterRange(0x1e00, 0x1eff, TextToolsCharacterRangeType.LatinExtendedAdditional),
			new TextToolsCharacterRange(0x1f00, 0x1fff, TextToolsCharacterRangeType.GreekExtended),
			new TextToolsCharacterRange(0x2000, 0x206f, TextToolsCharacterRangeType.GeneralPunctuation),
			new TextToolsCharacterRange(0x2070, 0x209f, TextToolsCharacterRangeType.SuperscriptsAndSubscripts),
			new TextToolsCharacterRange(0x20a0, 0x20cf, TextToolsCharacterRangeType.CurrencySymbols),
			new TextToolsCharacterRange(0x20d0, 0x20ff, TextToolsCharacterRangeType.CombiningDiacriticalMarksForSymbols),
			new TextToolsCharacterRange(0x2100, 0x214f, TextToolsCharacterRangeType.LetterlikeSymbols),
			new TextToolsCharacterRange(0x2150, 0x218f, TextToolsCharacterRangeType.NumberForms),
			new TextToolsCharacterRange(0x2190, 0x21ff, TextToolsCharacterRangeType.Arrows),
			new TextToolsCharacterRange(0x2200, 0x22ff, TextToolsCharacterRangeType.MathematicalOperators),
			new TextToolsCharacterRange(0x2300, 0x23ff, TextToolsCharacterRangeType.MiscellaneousTechnical),
			new TextToolsCharacterRange(0x2400, 0x243f, TextToolsCharacterRangeType.ControlPictures),
			new TextToolsCharacterRange(0x2440, 0x245f, TextToolsCharacterRangeType.OpticalCharacterRecognition),
			new TextToolsCharacterRange(0x2460, 0x24ff, TextToolsCharacterRangeType.EnclosedAlphanumerics),
			new TextToolsCharacterRange(0x2500, 0x257f, TextToolsCharacterRangeType.BoxDrawing),
			new TextToolsCharacterRange(0x2580, 0x259f, TextToolsCharacterRangeType.BlockElements),
			new TextToolsCharacterRange(0x25a0, 0x25ff, TextToolsCharacterRangeType.GeometricShapes),
			new TextToolsCharacterRange(0x2600, 0x26ff, TextToolsCharacterRangeType.MiscellaneousSymbols),
			new TextToolsCharacterRange(0x2700, 0x27bf, TextToolsCharacterRangeType.Dingbats),
			new TextToolsCharacterRange(0x27c0, 0x27ef, TextToolsCharacterRangeType.MiscellaneousMathematicalSymbolsA),
			new TextToolsCharacterRange(0x27f0, 0x27ff, TextToolsCharacterRangeType.SupplementalArrowsA),
			new TextToolsCharacterRange(0x2800, 0x28ff, TextToolsCharacterRangeType.BraillePatterns),
			new TextToolsCharacterRange(0x2900, 0x297f, TextToolsCharacterRangeType.SupplementalArrowsB),
			new TextToolsCharacterRange(0x2980, 0x29ff, TextToolsCharacterRangeType.MiscellaneousMathematicalSymbolsB),
			new TextToolsCharacterRange(0x2a00, 0x2aff, TextToolsCharacterRangeType.SupplementalMathematicalOperators),
			new TextToolsCharacterRange(0x2b00, 0x2bff, TextToolsCharacterRangeType.MiscellaneousSymbolsAndArrows),
			new TextToolsCharacterRange(0x2c00, 0x2c5f, TextToolsCharacterRangeType.Glagolitic),
			new TextToolsCharacterRange(0x2c60, 0x2c7f, TextToolsCharacterRangeType.LatinExtendedC),
			new TextToolsCharacterRange(0x2c80, 0x2cff, TextToolsCharacterRangeType.Coptic),
			new TextToolsCharacterRange(0x2d00, 0x2d2f, TextToolsCharacterRangeType.GeorgianSupplement),
			new TextToolsCharacterRange(0x2d30, 0x2d7f, TextToolsCharacterRangeType.Tifinagh),
			new TextToolsCharacterRange(0x2d80, 0x2ddf, TextToolsCharacterRangeType.EthiopicExtended),
			new TextToolsCharacterRange(0x2de0, 0x2dff, TextToolsCharacterRangeType.CyrillicExtendedA),
			new TextToolsCharacterRange(0x2e00, 0x2e7f, TextToolsCharacterRangeType.SupplementalPunctuation),
			new TextToolsCharacterRange(0x2e80, 0x2eff, TextToolsCharacterRangeType.CJKRadicalsSupplement),
			new TextToolsCharacterRange(0x2f00, 0x2fdf, TextToolsCharacterRangeType.KangxiRadicals),
			new TextToolsCharacterRange(0x2ff0, 0x2fff, TextToolsCharacterRangeType.IdeographicDescriptionCharacters),
			new TextToolsCharacterRange(0x3000, 0x303f, TextToolsCharacterRangeType.CJKSymbolsAndPunctuation),
			new TextToolsCharacterRange(0x3040, 0x309f, TextToolsCharacterRangeType.Hiragana),
			new TextToolsCharacterRange(0x30a0, 0x30ff, TextToolsCharacterRangeType.Katakana),
			new TextToolsCharacterRange(0x3100, 0x312f, TextToolsCharacterRangeType.Bopomofo),
			new TextToolsCharacterRange(0x3130, 0x318f, TextToolsCharacterRangeType.HangulCompatibilityJamo),
			new TextToolsCharacterRange(0x3190, 0x319f, TextToolsCharacterRangeType.Kanbun),
			new TextToolsCharacterRange(0x31a0, 0x31bf, TextToolsCharacterRangeType.BopomofoExtended),
			new TextToolsCharacterRange(0x31c0, 0x31ef, TextToolsCharacterRangeType.CJKStrokes),
			new TextToolsCharacterRange(0x31f0, 0x31ff, TextToolsCharacterRangeType.KatakanaPhoneticExtensions),
			new TextToolsCharacterRange(0x3200, 0x32ff, TextToolsCharacterRangeType.EnclosedCJKLettersAndMonths),
			new TextToolsCharacterRange(0x3300, 0x33ff, TextToolsCharacterRangeType.CJKCompatibility),
			new TextToolsCharacterRange(0x3400, 0x4dbf, TextToolsCharacterRangeType.CJKUnifiedIdeographsExtensionA),
			new TextToolsCharacterRange(0x4dc0, 0x4dff, TextToolsCharacterRangeType.YijingHexagramSymbols),
			new TextToolsCharacterRange(0x4e00, 0x9fff, TextToolsCharacterRangeType.CJKUnifiedIdeographs),
			new TextToolsCharacterRange(0xa000, 0xa48f, TextToolsCharacterRangeType.YiSyllables),
			new TextToolsCharacterRange(0xa490, 0xa4cf, TextToolsCharacterRangeType.YiRadicals),
			new TextToolsCharacterRange(0xa4d0, 0xa4ff, TextToolsCharacterRangeType.Lisu),
			new TextToolsCharacterRange(0xa500, 0xa63f, TextToolsCharacterRangeType.Vai),
			new TextToolsCharacterRange(0xa640, 0xa69f, TextToolsCharacterRangeType.CyrillicExtendedB),
			new TextToolsCharacterRange(0xa6a0, 0xa6ff, TextToolsCharacterRangeType.Bamum),
			new TextToolsCharacterRange(0xa700, 0xa71f, TextToolsCharacterRangeType.ModifierToneLetters),
			new TextToolsCharacterRange(0xa720, 0xa7ff, TextToolsCharacterRangeType.LatinExtendedD),
			new TextToolsCharacterRange(0xa800, 0xa82f, TextToolsCharacterRangeType.SylotiNagri),
			new TextToolsCharacterRange(0xa830, 0xa83f, TextToolsCharacterRangeType.CommonIndicNumberForms),
			new TextToolsCharacterRange(0xa840, 0xa87f, TextToolsCharacterRangeType.PhagsPa),
			new TextToolsCharacterRange(0xa880, 0xa8df, TextToolsCharacterRangeType.Saurashtra),
			new TextToolsCharacterRange(0xa8e0, 0xa8ff, TextToolsCharacterRangeType.DevanagariExtended),
			new TextToolsCharacterRange(0xa900, 0xa92f, TextToolsCharacterRangeType.KayahLi),
			new TextToolsCharacterRange(0xa930, 0xa95f, TextToolsCharacterRangeType.Rejang),
			new TextToolsCharacterRange(0xa960, 0xa97f, TextToolsCharacterRangeType.HangulJamoExtendedA),
			new TextToolsCharacterRange(0xa980, 0xa9df, TextToolsCharacterRangeType.Javanese),
			new TextToolsCharacterRange(0xa9e0, 0xa9ff, TextToolsCharacterRangeType.MyanmarExtendedB),
			new TextToolsCharacterRange(0xaa00, 0xaa5f, TextToolsCharacterRangeType.Cham),
			new TextToolsCharacterRange(0xaa60, 0xaa7f, TextToolsCharacterRangeType.MyanmarExtendedA),
			new TextToolsCharacterRange(0xaa80, 0xaadf, TextToolsCharacterRangeType.TaiViet),
			new TextToolsCharacterRange(0xaae0, 0xaaff, TextToolsCharacterRangeType.MeeteiMayekExtensions),
			new TextToolsCharacterRange(0xab00, 0xab2f, TextToolsCharacterRangeType.EthiopicExtendedA),
			new TextToolsCharacterRange(0xab30, 0xab6f, TextToolsCharacterRangeType.LatinExtendedE),
			new TextToolsCharacterRange(0xab70, 0xabbf, TextToolsCharacterRangeType.CherokeeSupplement),
			new TextToolsCharacterRange(0xabc0, 0xabff, TextToolsCharacterRangeType.MeeteiMayek),
			new TextToolsCharacterRange(0xac00, 0xd7af, TextToolsCharacterRangeType.HangulSyllables),
			new TextToolsCharacterRange(0xd7b0, 0xd7ff, TextToolsCharacterRangeType.HangulJamoExtendedB),
			new TextToolsCharacterRange(0xd800, 0xdb7f, TextToolsCharacterRangeType.HighSurrogates),
			new TextToolsCharacterRange(0xdb80, 0xdbff, TextToolsCharacterRangeType.HighPrivateUseSurrogates),
			new TextToolsCharacterRange(0xdc00, 0xdfff, TextToolsCharacterRangeType.LowSurrogates),
			new TextToolsCharacterRange(0xe000, 0xf8ff, TextToolsCharacterRangeType.PrivateUseArea),
			new TextToolsCharacterRange(0xf900, 0xfaff, TextToolsCharacterRangeType.CJKCompatibilityIdeographs),
			new TextToolsCharacterRange(0xfb00, 0xfb4f, TextToolsCharacterRangeType.AlphabeticPresentationForms),
			new TextToolsCharacterRange(0xfb50, 0xfdff, TextToolsCharacterRangeType.ArabicPresentationFormsA),
			new TextToolsCharacterRange(0xfe00, 0xfe0f, TextToolsCharacterRangeType.VariationSelectors),
			new TextToolsCharacterRange(0xfe10, 0xfe1f, TextToolsCharacterRangeType.VerticalForms),
			new TextToolsCharacterRange(0xfe20, 0xfe2f, TextToolsCharacterRangeType.CombiningHalfMarks),
			new TextToolsCharacterRange(0xfe30, 0xfe4f, TextToolsCharacterRangeType.CJKCompatibilityForms),
			new TextToolsCharacterRange(0xfe50, 0xfe6f, TextToolsCharacterRangeType.SmallFormVariants),
			new TextToolsCharacterRange(0xfe70, 0xfeff, TextToolsCharacterRangeType.ArabicPresentationFormsB),
			new TextToolsCharacterRange(0xff00, 0xffef, TextToolsCharacterRangeType.HalfwidthAndFullwidthForms),
			new TextToolsCharacterRange(0xfff0, 0xffff, TextToolsCharacterRangeType.Specials),
			new TextToolsCharacterRange(0x10000, 0x1007f, TextToolsCharacterRangeType.LinearBSyllabary),
			new TextToolsCharacterRange(0x10080, 0x100ff, TextToolsCharacterRangeType.LinearBIdeograms),
			new TextToolsCharacterRange(0x10100, 0x1013f, TextToolsCharacterRangeType.AegeanNumbers),
			new TextToolsCharacterRange(0x10140, 0x1018f, TextToolsCharacterRangeType.AncientGreekNumbers),
			new TextToolsCharacterRange(0x10190, 0x101cf, TextToolsCharacterRangeType.AncientSymbols),
			new TextToolsCharacterRange(0x101d0, 0x101ff, TextToolsCharacterRangeType.PhaistosDisc),
			new TextToolsCharacterRange(0x10280, 0x1029f, TextToolsCharacterRangeType.Lycian),
			new TextToolsCharacterRange(0x102a0, 0x102df, TextToolsCharacterRangeType.Carian),
			new TextToolsCharacterRange(0x102e0, 0x102ff, TextToolsCharacterRangeType.CopticEpactNumbers),
			new TextToolsCharacterRange(0x10300, 0x1032f, TextToolsCharacterRangeType.OldItalic),
			new TextToolsCharacterRange(0x10330, 0x1034f, TextToolsCharacterRangeType.Gothic),
			new TextToolsCharacterRange(0x10350, 0x1037f, TextToolsCharacterRangeType.OldPermic),
			new TextToolsCharacterRange(0x10380, 0x1039f, TextToolsCharacterRangeType.Ugaritic),
			new TextToolsCharacterRange(0x103a0, 0x103df, TextToolsCharacterRangeType.OldPersian),
			new TextToolsCharacterRange(0x10400, 0x1044f, TextToolsCharacterRangeType.Deseret),
			new TextToolsCharacterRange(0x10450, 0x1047f, TextToolsCharacterRangeType.Shavian),
			new TextToolsCharacterRange(0x10480, 0x104af, TextToolsCharacterRangeType.Osmanya),
			new TextToolsCharacterRange(0x104b0, 0x104ff, TextToolsCharacterRangeType.Osage),
			new TextToolsCharacterRange(0x10500, 0x1052f, TextToolsCharacterRangeType.Elbasan),
			new TextToolsCharacterRange(0x10530, 0x1056f, TextToolsCharacterRangeType.CaucasianAlbanian),
			new TextToolsCharacterRange(0x10600, 0x1077f, TextToolsCharacterRangeType.LinearA),
			new TextToolsCharacterRange(0x10800, 0x1083f, TextToolsCharacterRangeType.CypriotSyllabary),
			new TextToolsCharacterRange(0x10840, 0x1085f, TextToolsCharacterRangeType.ImperialAramaic),
			new TextToolsCharacterRange(0x10860, 0x1087f, TextToolsCharacterRangeType.Palmyrene),
			new TextToolsCharacterRange(0x10880, 0x108af, TextToolsCharacterRangeType.Nabataean),
			new TextToolsCharacterRange(0x108e0, 0x108ff, TextToolsCharacterRangeType.Hatran),
			new TextToolsCharacterRange(0x10900, 0x1091f, TextToolsCharacterRangeType.Phoenician),
			new TextToolsCharacterRange(0x10920, 0x1093f, TextToolsCharacterRangeType.Lydian),
			new TextToolsCharacterRange(0x10980, 0x1099f, TextToolsCharacterRangeType.MeroiticHieroglyphs),
			new TextToolsCharacterRange(0x109a0, 0x109ff, TextToolsCharacterRangeType.MeroiticCursive),
			new TextToolsCharacterRange(0x10a00, 0x10a5f, TextToolsCharacterRangeType.Kharoshthi),
			new TextToolsCharacterRange(0x10a60, 0x10a7f, TextToolsCharacterRangeType.OldSouthArabian),
			new TextToolsCharacterRange(0x10a80, 0x10a9f, TextToolsCharacterRangeType.OldNorthArabian),
			new TextToolsCharacterRange(0x10ac0, 0x10aff, TextToolsCharacterRangeType.Manichaean),
			new TextToolsCharacterRange(0x10b00, 0x10b3f, TextToolsCharacterRangeType.Avestan),
			new TextToolsCharacterRange(0x10b40, 0x10b5f, TextToolsCharacterRangeType.InscriptionalParthian),
			new TextToolsCharacterRange(0x10b60, 0x10b7f, TextToolsCharacterRangeType.InscriptionalPahlavi),
			new TextToolsCharacterRange(0x10b80, 0x10baf, TextToolsCharacterRangeType.PsalterPahlavi),
			new TextToolsCharacterRange(0x10c00, 0x10c4f, TextToolsCharacterRangeType.OldTurkic),
			new TextToolsCharacterRange(0x10c80, 0x10cff, TextToolsCharacterRangeType.OldHungarian),
			new TextToolsCharacterRange(0x10e60, 0x10e7f, TextToolsCharacterRangeType.RumiNumeralSymbols),
			new TextToolsCharacterRange(0x11000, 0x1107f, TextToolsCharacterRangeType.Brahmi),
			new TextToolsCharacterRange(0x11080, 0x110cf, TextToolsCharacterRangeType.Kaithi),
			new TextToolsCharacterRange(0x110d0, 0x110ff, TextToolsCharacterRangeType.SoraSompeng),
			new TextToolsCharacterRange(0x11100, 0x1114f, TextToolsCharacterRangeType.Chakma),
			new TextToolsCharacterRange(0x11150, 0x1117f, TextToolsCharacterRangeType.Mahajani),
			new TextToolsCharacterRange(0x11180, 0x111df, TextToolsCharacterRangeType.Sharada),
			new TextToolsCharacterRange(0x111e0, 0x111ff, TextToolsCharacterRangeType.SinhalaArchaicNumbers),
			new TextToolsCharacterRange(0x11200, 0x1124f, TextToolsCharacterRangeType.Khojki),
			new TextToolsCharacterRange(0x11280, 0x112af, TextToolsCharacterRangeType.Multani),
			new TextToolsCharacterRange(0x112b0, 0x112ff, TextToolsCharacterRangeType.Khudawadi),
			new TextToolsCharacterRange(0x11300, 0x1137f, TextToolsCharacterRangeType.Grantha),
			new TextToolsCharacterRange(0x11400, 0x1147f, TextToolsCharacterRangeType.Newa),
			new TextToolsCharacterRange(0x11480, 0x114df, TextToolsCharacterRangeType.Tirhuta),
			new TextToolsCharacterRange(0x11580, 0x115ff, TextToolsCharacterRangeType.Siddham),
			new TextToolsCharacterRange(0x11600, 0x1165f, TextToolsCharacterRangeType.Modi),
			new TextToolsCharacterRange(0x11660, 0x1167f, TextToolsCharacterRangeType.MongolianSupplement),
			new TextToolsCharacterRange(0x11680, 0x116cf, TextToolsCharacterRangeType.Takri),
			new TextToolsCharacterRange(0x11700, 0x1173f, TextToolsCharacterRangeType.Ahom),
			new TextToolsCharacterRange(0x118a0, 0x118ff, TextToolsCharacterRangeType.WarangCiti),
			new TextToolsCharacterRange(0x11ac0, 0x11aff, TextToolsCharacterRangeType.PauCinHau),
			new TextToolsCharacterRange(0x11c00, 0x11c6f, TextToolsCharacterRangeType.Bhaiksuki),
			new TextToolsCharacterRange(0x11c70, 0x11cbf, TextToolsCharacterRangeType.Marchen),
			new TextToolsCharacterRange(0x12000, 0x123ff, TextToolsCharacterRangeType.Cuneiform),
			new TextToolsCharacterRange(0x12400, 0x1247f, TextToolsCharacterRangeType.CuneiformNumbersAndPunctuation),
			new TextToolsCharacterRange(0x12480, 0x1254f, TextToolsCharacterRangeType.EarlyDynasticCuneiform),
			new TextToolsCharacterRange(0x13000, 0x1342f, TextToolsCharacterRangeType.EgyptianHieroglyphs),
			new TextToolsCharacterRange(0x14400, 0x1467f, TextToolsCharacterRangeType.AnatolianHieroglyphs),
			new TextToolsCharacterRange(0x16800, 0x16a3f, TextToolsCharacterRangeType.BamumSupplement),
			new TextToolsCharacterRange(0x16a40, 0x16a6f, TextToolsCharacterRangeType.Mro),
			new TextToolsCharacterRange(0x16ad0, 0x16aff, TextToolsCharacterRangeType.BassaVah),
			new TextToolsCharacterRange(0x16b00, 0x16b8f, TextToolsCharacterRangeType.PahawhHmong),
			new TextToolsCharacterRange(0x16f00, 0x16f9f, TextToolsCharacterRangeType.Miao),
			new TextToolsCharacterRange(0x16fe0, 0x16fff, TextToolsCharacterRangeType.IdeographicSymbolsAndPunctuation),
			new TextToolsCharacterRange(0x17000, 0x187ff, TextToolsCharacterRangeType.Tangut),
			new TextToolsCharacterRange(0x18800, 0x18aff, TextToolsCharacterRangeType.TangutComponents),
			new TextToolsCharacterRange(0x1b000, 0x1b0ff, TextToolsCharacterRangeType.KanaSupplement),
			new TextToolsCharacterRange(0x1bc00, 0x1bc9f, TextToolsCharacterRangeType.Duployan),
			new TextToolsCharacterRange(0x1bca0, 0x1bcaf, TextToolsCharacterRangeType.ShorthandFormatControls),
			new TextToolsCharacterRange(0x1d000, 0x1d0ff, TextToolsCharacterRangeType.ByzantineMusicalSymbols),
			new TextToolsCharacterRange(0x1d100, 0x1d1ff, TextToolsCharacterRangeType.MusicalSymbols),
			new TextToolsCharacterRange(0x1d200, 0x1d24f, TextToolsCharacterRangeType.AncientGreekMusicalNotation),
			new TextToolsCharacterRange(0x1d300, 0x1d35f, TextToolsCharacterRangeType.TaiXuanJingSymbols),
			new TextToolsCharacterRange(0x1d360, 0x1d37f, TextToolsCharacterRangeType.CountingRodNumerals),
			new TextToolsCharacterRange(0x1d400, 0x1d7ff, TextToolsCharacterRangeType.MathematicalAlphanumericSymbols),
			new TextToolsCharacterRange(0x1d800, 0x1daaf, TextToolsCharacterRangeType.SuttonSignWriting),
			new TextToolsCharacterRange(0x1e000, 0x1e02f, TextToolsCharacterRangeType.GlagoliticSupplement),
			new TextToolsCharacterRange(0x1e800, 0x1e8df, TextToolsCharacterRangeType.MendeKikakui),
			new TextToolsCharacterRange(0x1e900, 0x1e95f, TextToolsCharacterRangeType.Adlam),
			new TextToolsCharacterRange(0x1ee00, 0x1eeff, TextToolsCharacterRangeType.ArabicMathematicalAlphabeticSymbols),
			new TextToolsCharacterRange(0x1f000, 0x1f02f, TextToolsCharacterRangeType.MahjongTiles),
			new TextToolsCharacterRange(0x1f030, 0x1f09f, TextToolsCharacterRangeType.DominoTiles),
			new TextToolsCharacterRange(0x1f0a0, 0x1f0ff, TextToolsCharacterRangeType.PlayingCards),
			new TextToolsCharacterRange(0x1f100, 0x1f1ff, TextToolsCharacterRangeType.EnclosedAlphanumericSupplement),
			new TextToolsCharacterRange(0x1f200, 0x1f2ff, TextToolsCharacterRangeType.EnclosedIdeographicSupplement),
			new TextToolsCharacterRange(0x1f300, 0x1f5ff, TextToolsCharacterRangeType.MiscellaneousSymbolsAndPictographs),
			new TextToolsCharacterRange(0x1f600, 0x1f64f, TextToolsCharacterRangeType.EmoticonsEmoji),
			new TextToolsCharacterRange(0x1f650, 0x1f67f, TextToolsCharacterRangeType.OrnamentalDingbats),
			new TextToolsCharacterRange(0x1f680, 0x1f6ff, TextToolsCharacterRangeType.TransportAndMapSymbols),
			new TextToolsCharacterRange(0x1f700, 0x1f77f, TextToolsCharacterRangeType.AlchemicalSymbols),
			new TextToolsCharacterRange(0x1f780, 0x1f7ff, TextToolsCharacterRangeType.GeometricShapesExtended),
			new TextToolsCharacterRange(0x1f800, 0x1f8ff, TextToolsCharacterRangeType.SupplementalArrowsC),
			new TextToolsCharacterRange(0x1f900, 0x1f9ff, TextToolsCharacterRangeType.SupplementalSymbolsAndPictographs),
			new TextToolsCharacterRange(0x20000, 0x2a6d6, TextToolsCharacterRangeType.CJKUnifiedIdeographsExtensionB),
			new TextToolsCharacterRange(0x2a700, 0x2b734, TextToolsCharacterRangeType.CJKUnifiedIdeographsExtensionC),
			new TextToolsCharacterRange(0x2b740, 0x2b81d, TextToolsCharacterRangeType.CJKUnifiedIdeographsExtensionD),
			new TextToolsCharacterRange(0x2b820, 0x2cea1, TextToolsCharacterRangeType.CJKUnifiedIdeographsExtensionE),
			new TextToolsCharacterRange(0x2f800, 0x2fa1f, TextToolsCharacterRangeType.CJKCompatibilityIdeographsSupplement),
			new TextToolsCharacterRange(0xe0000, 0xe007f, TextToolsCharacterRangeType.Tags),
			new TextToolsCharacterRange(0xe0100, 0xe01ef, TextToolsCharacterRangeType.VariationSelectorsSupplement)
		};

		static private int           hash               = "TextTools".GetHashCode();
		static private bool          fontShow           = true;
		static private bool          meshShow           = false;
		

		private ReorderableList      characterRangeList = null;
		private PreviewRenderUtility previewUtility     = null;
		private Vector2              previewDir         = Vector2.zero;
		private float                zoom               = 0.0f;
		
		private bool                 changeCheck;

		private SerializedProperty   characterRanges;
		private SerializedProperty   faceMaterial;
		private SerializedProperty   sideMaterial;
		private SerializedProperty   outlineMaterial;
		private SerializedProperty   meshCompression;
		private SerializedProperty   outlineJoin;
		private SerializedProperty   topLeftColor;
		private SerializedProperty   topRightColor;
		private SerializedProperty   bottomLeftColor;
		private SerializedProperty   bottomRightColor;
		private SerializedProperty   assetPath;
		private SerializedProperty   curveQuality;
		private SerializedProperty   extrude;
		private SerializedProperty   outlineWidth;
		private SerializedProperty   miterLimit;
		private SerializedProperty   bevelSegments;
		private SerializedProperty   unitsPerEm;
		private SerializedProperty   missingGlyph;
		private SerializedProperty   optimizeMesh;
		private SerializedProperty   useTangents;
		private SerializedProperty   useColors;
		private SerializedProperty   uvCorrection;


		void DrawHeader(Rect rect)
		{
			EditorGUI.LabelField(rect, "Character Ranges");
		}

		void DrawElement(Rect rect, int index, bool active, bool focused)
		{
			SerializedProperty element = characterRangeList.serializedProperty.GetArrayElementAtIndex(index);
			SerializedProperty custom  = element.FindPropertyRelative("custom");
			SerializedProperty begin   = element.FindPropertyRelative("begin");
			SerializedProperty end     = element.FindPropertyRelative("end");
			SerializedProperty type    = element.FindPropertyRelative("type");
			float              left    = rect.x;
			float              width   = rect.width;

			GUI.enabled = string.IsNullOrEmpty(custom.stringValue);
			EditorGUI.BeginChangeCheck();
			
			rect.width = width*0.55f;
			EditorGUI.PropertyField(rect, type, new GUIContent(), true);

			if(EditorGUI.EndChangeCheck() && (int)TextToolsCharacterRangeType.Custom != type.enumValueIndex)
			{
				begin.intValue = ranges[type.enumValueIndex].begin;
				end.intValue   = ranges[type.enumValueIndex].end;
				changeCheck    = true;
			}

			EditorGUI.BeginChangeCheck();
			
			rect.x += rect.width + 5.0f;
			rect.width = width*0.2f;
			EditorGUI.PropertyField(rect, begin, new GUIContent(), true);
			
			rect.x += rect.width + 5.0f;
			rect.width = width*0.2f;
			EditorGUI.PropertyField(rect, end, new GUIContent(), true);
			
			if(EditorGUI.EndChangeCheck())
			{
				type.enumValueIndex = (int)TextToolsCharacterRangeType.Custom;
				changeCheck = true;
			}

			GUI.enabled = true;
			EditorGUI.BeginChangeCheck();

			rect.x  = left;
			rect.y += EditorGUIUtility.singleLineHeight + 1.0f;
			rect.width = width*0.55f;
			EditorGUI.PropertyField(rect, custom, new GUIContent(), true);
			
			if(EditorGUI.EndChangeCheck())
				changeCheck = true;

			GUI.enabled = string.IsNullOrEmpty(custom.stringValue);
			EditorGUI.BeginChangeCheck();

			rect.height = EditorGUIUtility.singleLineHeight;
			rect.x += rect.width + 5.0f;
			rect.width = width*0.2f;
			begin.intValue = EditorGUI.IntField(rect, begin.intValue);

			rect.x += rect.width + 5.0f;
			rect.width = width*0.2f;
			end.intValue = EditorGUI.IntField(rect, end.intValue);
			
			if(EditorGUI.EndChangeCheck())
			{
				type.enumValueIndex = (int)TextToolsCharacterRangeType.Custom;
				changeCheck = true;
			}

			GUI.enabled = true;
		}
		
		public void OnEnable()
        {
            TextToolsFont asset;

            if(serializedObject.isEditingMultipleObjects)
            {
                foreach(Object t in targets)
                {
                    asset = t as TextToolsFont;
                    asset.CreateSnapshot();
                }
            }
            else
            {
                asset = target as TextToolsFont;
                asset.CreateSnapshot();
            }

            characterRanges  = serializedObject.FindProperty("characterRangeList");
			faceMaterial     = serializedObject.FindProperty("faceMaterial");
			sideMaterial     = serializedObject.FindProperty("sideMaterial");
			outlineMaterial  = serializedObject.FindProperty("outlineMaterial");
			meshCompression  = serializedObject.FindProperty("meshCompression");
			outlineJoin      = serializedObject.FindProperty("outlineJoin");
			topLeftColor     = serializedObject.FindProperty("topLeftColor");
			topRightColor    = serializedObject.FindProperty("topRightColor");
			bottomLeftColor  = serializedObject.FindProperty("bottomLeftColor");
			bottomRightColor = serializedObject.FindProperty("bottomRightColor");
			assetPath        = serializedObject.FindProperty("assetPath");
			curveQuality     = serializedObject.FindProperty("curveQuality");
			extrude          = serializedObject.FindProperty("extrude");
			outlineWidth     = serializedObject.FindProperty("outlineWidth");
			miterLimit       = serializedObject.FindProperty("miterLimit");
			bevelSegments    = serializedObject.FindProperty("bevelSegments");
			unitsPerEm       = serializedObject.FindProperty("unitsPerEm");
			missingGlyph     = serializedObject.FindProperty("missingGlyph");
			optimizeMesh     = serializedObject.FindProperty("optimizeMesh");
			useTangents      = serializedObject.FindProperty("useTangents");
			useColors        = serializedObject.FindProperty("useColors");
			uvCorrection     = serializedObject.FindProperty("uvCorrection");
			changeCheck      = false;
			
			if(null == characterRangeList)
			{
				characterRangeList = new ReorderableList(serializedObject, characterRanges, false, true, true, true);
				characterRangeList.elementHeight       = EditorGUIUtility.singleLineHeight * 2.0f + 5.0f;
				characterRangeList.drawHeaderCallback  = DrawHeader;
				characterRangeList.drawElementCallback = DrawElement;
			}

			if(null == previewUtility)
				previewUtility = new PreviewRenderUtility();

#if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui += this.OnSceneGUI;
#else
            SceneView.onSceneGUIDelegate += this.OnSceneGUI;
#endif
            EditorApplication.hierarchyWindowItemOnGUI += this.HierarchyWindowItemOnGUI;
		}

		void OnDisable()
		{
			if(changeCheck)
			{
				string asset = "";

				if(serializedObject.isEditingMultipleObjects)
					asset += targets.Length;
				else
					asset = AssetDatabase.GetAssetPath(target);

				if(EditorUtility.DisplayDialog("Unapplied import settings", "Unapplied import settings for '" + asset + "'", "Apply", "Revert"))
					ApplyChanges();
				else
                    RevertChanges();
			}

			previewUtility.Cleanup();

#if UNITY_2019_1_OR_NEWER
            SceneView.duringSceneGui -= this.OnSceneGUI;
#else
            SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
#endif
            EditorApplication.hierarchyWindowItemOnGUI -= this.HierarchyWindowItemOnGUI;
		}

		void OnDestroy()
		{
			characterRangeList = null;
			previewUtility     = null;
		}

		void OnSceneGUI(SceneView view)
		{
			EventType type = Event.current.type;

			if(EventType.DragUpdated == type)
			{
				DragAndDrop.visualMode = DragAndDropVisualMode.Link;
				Event.current.Use();
			}
			else if(EventType.DragPerform == type)
			{
				TextToolsActor actor;
				GameObject     go;
				Vector3        pos = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).GetPoint(5.0f);

				foreach(Object obj in DragAndDrop.objectReferences)
				{
					if(obj is TextToolsFont)
					{
						go = new GameObject(obj.name);
						
						go.transform.position = pos;

						actor = go.AddComponent<TextToolsActor>();
						actor.SetFont(obj as TextToolsFont, false);

						Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
						Selection.activeGameObject = go;
					}
				}

				Event.current.Use();
			}
		}

		void HierarchyWindowItemOnGUI(int id, Rect selectionRect)
		{
			EventType type = Event.current.type;

			if(EventType.DragUpdated != type && EventType.DragPerform != type && EventType.DragExited != type)
				return;

			TextToolsActor actor;
			GameObject     go;

			DragAndDrop.visualMode = DragAndDropVisualMode.Link;

			if(selectionRect.Contains(Event.current.mousePosition) && EventType.DragPerform == type)
			{
				GameObject parent = EditorUtility.InstanceIDToObject(id) as GameObject;

				foreach(Object obj in DragAndDrop.objectReferences)
				{
					if(obj is TextToolsFont)
					{
						go = new GameObject(obj.name);

						if(null != go)
						{
							go.transform.SetParent(parent.transform);
							go.transform.localPosition = Vector3.zero;
							go.transform.localRotation = Quaternion.identity;
							go.transform.localScale    = Vector3.one;

							actor = go.AddComponent<TextToolsActor>();
							actor.SetFont(obj as TextToolsFont, false);

							Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
							Selection.activeGameObject = go;
						}
					}
				}

				Event.current.Use();
			}
			else if(!selectionRect.Contains(Event.current.mousePosition) && EventType.DragExited == type)
			{
				foreach(Object obj in DragAndDrop.objectReferences)
				{
					if(obj is TextToolsFont)
					{
						go = new GameObject(obj.name);

						if(null != go)
						{
							actor = go.AddComponent<TextToolsActor>();
							actor.SetFont(obj as TextToolsFont, false);

							Selection.activeGameObject = go;
							Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
							Undo.PerformRedo();
						}
					}
				}

				Event.current.Use();
			}
		}

		public override string GetInfoString()
		{
			TextToolsFont asset = target as TextToolsFont;

			return (null == asset) ? "" : asset.InfoString;
        }

        void DoOpenFile()
        {
            GUIContent label    = new GUIContent("Font Name");
            Rect       position = EditorGUILayout.GetControlRect(true, 16.0f);
            int        id       = GUIUtility.GetControlID(hash, FocusType.Keyboard, position);
            Event      evt      = Event.current;
            EventType  type     = evt.type;

            position = EditorGUI.PrefixLabel(position, id, label);

            if(EventType.MouseDown == type && 0 == Event.current.button && position.Contains(Event.current.mousePosition))
            {
                EditorGUIUtility.editingTextField = false;

                string[] filters   = { "All files", "ttf,otf", "TrueType fonts", "ttf", "OpenType fonts", "otf" };
                string   directory = string.IsNullOrEmpty(assetPath.stringValue) ? Directory.GetCurrentDirectory().Replace('\\', '/') + "/" :  System.IO.Path.GetDirectoryName(assetPath.stringValue);
                string   path      = EditorUtility.OpenFilePanelWithFilters("Open File", directory, filters);

                if(!string.IsNullOrEmpty(path))
                {
                    assetPath.stringValue = path;
                    changeCheck = true;
                    serializedObject.ApplyModifiedProperties();
                }

                evt.Use();
                GUIUtility.ExitGUI();
            }
            else if(EventType.Repaint == type)
            {
                EditorStyles.objectField.Draw(position, new GUIContent(assetPath.stringValue.Substring(assetPath.stringValue.LastIndexOf("/") + 1)), id, false);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if(fontShow = EditorGUILayout.Foldout(fontShow, "Font"))
            {
                DoOpenFile();
                EditorGUI.BeginChangeCheck();
				
				EditorGUILayout.PropertyField(missingGlyph);
				EditorGUILayout.Space();

                characterRangeList.DoLayoutList();
                EditorGUILayout.Space();

                if(EditorGUI.EndChangeCheck())
					changeCheck = true;
			}

			if(meshShow = EditorGUILayout.Foldout(meshShow, "Mesh"))
			{
				EditorGUI.BeginChangeCheck();

				EditorGUILayout.PropertyField(curveQuality);
				EditorGUILayout.PropertyField(extrude);
				EditorGUILayout.PropertyField(outlineWidth);
				EditorGUILayout.PropertyField(bevelSegments);
				EditorGUILayout.PropertyField(miterLimit);
				EditorGUILayout.PropertyField(outlineJoin);
				EditorGUILayout.PropertyField(meshCompression);
				EditorGUILayout.PropertyField(optimizeMesh);
				EditorGUILayout.PropertyField(useTangents);
				EditorGUILayout.PropertyField(useColors);
				EditorGUILayout.PropertyField(uvCorrection);
				EditorGUILayout.PropertyField(faceMaterial);
				EditorGUILayout.PropertyField(sideMaterial);
				EditorGUILayout.PropertyField(outlineMaterial);
				EditorGUILayout.PropertyField(topLeftColor);
				EditorGUILayout.PropertyField(topRightColor);
				EditorGUILayout.PropertyField(bottomLeftColor);
				EditorGUILayout.PropertyField(bottomRightColor);
				EditorGUILayout.Space();
				
				if(EditorGUI.EndChangeCheck())
					changeCheck = true;
			}
			
			GUI.enabled = changeCheck && !EditorApplication.isPlayingOrWillChangePlaymode;
			EditorGUILayout.Space();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();

            serializedObject.ApplyModifiedProperties();

            if(GUILayout.Button("Revert", GUILayout.MinWidth(55.0f)))
                RevertChanges();
			
			if(GUILayout.Button("Apply", GUILayout.MinWidth(50.0f)))
				ApplyChanges();

			GUILayout.EndHorizontal();
		}

		public override bool HasPreviewGUI()
		{
			TextToolsFont asset = target as TextToolsFont;

			return (null != asset);
		} 
		
		public override void OnPreviewGUI(Rect rect, GUIStyle background) 
        {
			if(!ShaderUtil.hardwareSupportsRectRenderTexture)
			{
				if(Event.current.type == EventType.Repaint)
					EditorGUI.DropShadowLabel(new Rect(rect.x, rect.y, rect.width, 40), "TextTools preview requires\nrender texture support");

				return;
			}
			
			if(null == previewUtility)
				previewUtility = new PreviewRenderUtility();
			
			int   id = GUIUtility.GetControlID(hash, FocusType.Passive);
			Event ev = Event.current;

			switch(ev.GetTypeForControl(id))
			{
				case EventType.MouseDown:
					if(rect.Contains(ev.mousePosition) && rect.width > 50)
					{
						GUIUtility.hotControl = id;
						ev.Use();
						EditorGUIUtility.SetWantsMouseJumping(1);
					}
					break;
				case EventType.MouseDrag:
					if(GUIUtility.hotControl == id && 0 == ev.button)
					{
						previewDir  -= ev.delta * (ev.shift ? 3 : 1) / Mathf.Min(rect.width, rect.height) * 140.0f;
						previewDir.y = Mathf.Clamp(previewDir.y, -90, 90);
						ev.Use();
						GUI.changed = true;
					}	   
					else if(GUIUtility.hotControl == id && 1 == ev.button)
					{
						zoom -= ev.delta.y * (ev.shift ? 3 : 1) / Mathf.Min(rect.width, rect.height) * 140.0f;
						zoom += ev.delta.x * (ev.shift ? 3 : 1) / Mathf.Min(rect.width, rect.height) * 140.0f;
						ev.Use();
						GUI.changed = true;
					}
					break;
				case EventType.MouseUp:
					if(GUIUtility.hotControl == id)
						GUIUtility.hotControl = 0;
					EditorGUIUtility.SetWantsMouseJumping(0);
					break;
			}
			
			if(Event.current.type != EventType.Repaint)
				return;

			TextToolsFont asset  = target as TextToolsFont;
			Quaternion    rot    = Quaternion.Euler(previewDir.y, 0, 0) * Quaternion.Euler(0, previewDir.x, 0);
			Vector3       pos    = Vector3.zero;
			float         scale  = 72.0f/(float)unitsPerEm.intValue;   

#if UNITY_2017_1_OR_NEWER 	 
			
			Camera        camera = previewUtility.camera;
			Light[]       lights = previewUtility.lights;
#else  
			Camera        camera = previewUtility.m_Camera;
			Light[]       lights = previewUtility.m_Light;

#endif

			
			if(null == asset.PreviewGlyphs || null == asset.Materials)
				return;

			previewUtility.BeginPreview(rect, background);

			camera.fieldOfView   = 30.0f;
			camera.nearClipPlane = 0.1f;
			camera.farClipPlane  = 10000.0f;
			camera.clearFlags    = CameraClearFlags.Nothing;
			camera.transform.position = Vector3.back;
			camera.transform.rotation = Quaternion.identity;

			lights[0].intensity = 1.4f;
			lights[0].transform.rotation = Quaternion.Euler(40f, 40f, 0);
			lights[1].intensity = 1.4f;
			
			for(int i=0; i<asset.PreviewGlyphs.Length; ++i)
			{
				if(null == asset.PreviewGlyphs[i])
					continue;

				pos    = rot * new Vector3(asset.PreviewPositions[i].x * scale, asset.PreviewPositions[i].y * scale, extrude.floatValue * 0.5f * scale);
				pos.z -= asset.PreviewPositions[i].z * (rect.height/rect.width) * scale + zoom;
				
				for(int j=0; j<asset.Materials.Length; ++j)
					previewUtility.DrawMesh(asset.PreviewGlyphs[i].mesh, Matrix4x4.TRS(pos, rot, new Vector3(scale, scale, scale)), asset.Materials[j], j);
			}

			camera.Render();
			EditorGUI.DrawPreviewTexture(rect, previewUtility.EndPreview(), null, ScaleMode.StretchToFill);
		}	 
		
		public override Texture2D RenderStaticPreview(string path, UnityEngine.Object[] subassets, int width, int height)
		{
			if(!ShaderUtil.hardwareSupportsRectRenderTexture)
				return null;
			
			if(null == previewUtility)
				previewUtility = new PreviewRenderUtility();

			TextToolsFont asset  = target as TextToolsFont;
			Quaternion    rot    = Quaternion.Euler(previewDir.y, 0, 0) * Quaternion.Euler(0, previewDir.x, 0);
			Vector3       pos    = Vector3.zero;
			float         scale  = 72.0f/(float)unitsPerEm.intValue;  

#if UNITY_2017_1_OR_NEWER 	 
			
			Camera        camera = previewUtility.camera;
			Light[]       lights = previewUtility.lights;
#else  
			Camera        camera = previewUtility.m_Camera;
			Light[]       lights = previewUtility.m_Light;

#endif
			
			if(null == asset.PreviewGlyphs || null == asset.Materials)
				return null;

			previewUtility.BeginStaticPreview(new Rect(0.0f, 0.0f, width, height));

			camera.fieldOfView   = 30.0f;
			camera.nearClipPlane = 0.1f;
			camera.farClipPlane  = 10000.0f;
			camera.clearFlags    = CameraClearFlags.Nothing;
			camera.transform.position = Vector3.back;
			camera.transform.rotation = Quaternion.identity;

			lights[0].intensity = 1.4f;
			lights[0].transform.rotation = Quaternion.Euler(40f, 40f, 0);
			lights[1].intensity = 1.4f;

			for(int i=0; i<asset.PreviewGlyphs.Length; ++i)
			{
				if(null == asset.PreviewGlyphs[i])
					continue;

				pos    = rot * new Vector3(asset.PreviewPositions[i].x * scale, asset.PreviewPositions[i].y * scale, extrude.floatValue * 0.5f * scale);
				pos.z -= asset.PreviewPositions[i].z * scale;

				for(int j=0; j<asset.Materials.Length; ++j)
					previewUtility.DrawMesh(asset.PreviewGlyphs[i].mesh, Matrix4x4.TRS(pos, rot, new Vector3(scale, scale, scale)), asset.Materials[j], j);

			}

			camera.Render();

			return previewUtility.EndStaticPreview();
        }

        public void RevertChanges()
        {
            TextToolsFont asset = null;

            if(serializedObject.isEditingMultipleObjects)
            {
                foreach(Object t in targets)
                {
                    asset = t as TextToolsFont;
                    asset.Revert();
                }
            }
            else
            {
                asset = target as TextToolsFont;
                asset.Revert();
            }

            changeCheck = false;

            GUI.FocusControl(null);
            EditorGUI.FocusTextInControl(null);
        }

        public void ApplyChanges() 
        {
			TextToolsFont asset    = null;
			float         progress = 0.0f;

            if(serializedObject.isEditingMultipleObjects) 
            { 
				AssetDatabase.StartAssetEditing();

                foreach(Object t in targets) 
                {
					asset = t as TextToolsFont;
					asset.Apply(TextToolsLoader.LoadFont, TextToolsLoader.LoadKerningPairs, TextToolsCreator.CreateGlyph);

                    progress += (1.0f / targets.Length);
                    EditorUtility.DisplayProgressBar("Apply", "Apply Change for '" + AssetDatabase.GetAssetPath(t) + "'", progress);
                } 
				
				AssetDatabase.StopAssetEditing();
                EditorUtility.ClearProgressBar();
            }
            else 
            {
				asset = target as TextToolsFont;
				asset.Apply(TextToolsLoader.LoadFont, TextToolsLoader.LoadKerningPairs, TextToolsCreator.CreateGlyph);
            }

			changeCheck = false;

			GUI.FocusControl(null);	
			EditorGUI.FocusTextInControl(null);            
             
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

            Canvas.ForceUpdateCanvases();
			SceneView.RepaintAll();
        }

		[MenuItem("Assets/Create/TextTools Font", false, 100)]
        static void CreateTextToolsFont()
        { 
			string path = AssetDatabase.GetAssetPath(Selection.activeObject);

			if("" == path)
				path = "Assets";
			else if("" != System.IO.Path.GetExtension(path))
				path = path.Replace(System.IO.Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
						
			TextToolsFont asset = ScriptableObject.CreateInstance<TextToolsFont>();
            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(path + "/TextToolsFont.asset"));
			
			EditorUtility.SetDirty(asset);  
			AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(asset)); 
        }
	}
}
// Copyright (C) 2019 Alexander Klochkov - All Rights Reserved
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms


using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


namespace texttools
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(TextToolsActor))]
	public class ActorInspector : Editor
	{
		static private bool        contentShow  = false;
		static private bool        ligthingShow = false;
		static private bool        physicsShow  = false;
		
		private ReorderableList    textPathList = null;

		private SerializedProperty textPath;
		private SerializedProperty sourceFont;
		private SerializedProperty inputText;
		private SerializedProperty fontSize;
		private SerializedProperty characterSpace;
		private SerializedProperty wordSpace;
		private SerializedProperty lineSpace;
		private SerializedProperty pathOffset;
		private SerializedProperty textAlignment;
		private SerializedProperty colliderType;
		private SerializedProperty lightProbes;
		private SerializedProperty castShadows;
		private SerializedProperty reflectText;
		private SerializedProperty verticalText;
		private SerializedProperty verticalPath;
		private SerializedProperty receiveShadows;
		private SerializedProperty simulatePhysics;


		void DrawHeader(Rect rect)
		{
			EditorGUI.LabelField(rect, "Text path");
		}

		void DrawElement(Rect rect, int index, bool active, bool focused)
		{
			SerializedProperty element = textPathList.serializedProperty.GetArrayElementAtIndex(index);

			EditorGUI.PropertyField(rect, element, new GUIContent(), true);
		}
		
		public void OnEnable()
		{
			textPath        = serializedObject.FindProperty("textPath");
			sourceFont      = serializedObject.FindProperty("sourceFont");
			inputText       = serializedObject.FindProperty("inputText");
			fontSize        = serializedObject.FindProperty("fontSize");
			characterSpace  = serializedObject.FindProperty("characterSpace");
			wordSpace       = serializedObject.FindProperty("wordSpace");
			lineSpace       = serializedObject.FindProperty("lineSpace");
			pathOffset      = serializedObject.FindProperty("pathOffset");
			textAlignment   = serializedObject.FindProperty("textAlignment");
			colliderType    = serializedObject.FindProperty("colliderType");
			lightProbes     = serializedObject.FindProperty("lightProbes");
			castShadows     = serializedObject.FindProperty("castShadows");
			reflectText     = serializedObject.FindProperty("reflectText");
			verticalText    = serializedObject.FindProperty("verticalText");
			verticalPath    = serializedObject.FindProperty("verticalPath");
			receiveShadows  = serializedObject.FindProperty("receiveShadows");
			simulatePhysics = serializedObject.FindProperty("simulatePhysics");

			if(null == textPathList)
			{
				textPathList = new ReorderableList(serializedObject, textPath, false, true, true, true);
				textPathList.drawHeaderCallback  = DrawHeader;
				textPathList.drawElementCallback = DrawElement;
			}
		}

		public void OnDisable()
		{
		}

		void OnDestroy()
		{
			textPathList = null;
		}

		public override void OnInspectorGUI()
		{
			TextToolsActor actor    = null;
			bool           generate = false;  
			bool           clear    = false; 
			bool           place    = false; 
			bool           length   = false;
			bool           lighting	= false;
			bool           physics  = false;

			serializedObject.Update();

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.Space();
			EditorGUILayout.PropertyField(inputText, new GUIContent());
			EditorGUILayout.Space();

			if(EditorGUI.EndChangeCheck())
				generate = clear = true;

			if(contentShow = EditorGUILayout.Foldout(contentShow, "Content")) 
			{
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(sourceFont);
				EditorGUILayout.Space();
				
				if(EditorGUI.EndChangeCheck())
					generate = clear = true;

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(textAlignment);
				
				if(EditorGUI.EndChangeCheck())
					generate = true;

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(reflectText);
				
				if(EditorGUI.EndChangeCheck())
					generate = clear = true;

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(verticalText);
				EditorGUILayout.PropertyField(verticalPath);
				EditorGUILayout.PropertyField(fontSize);
				EditorGUILayout.PropertyField(characterSpace);
				EditorGUILayout.PropertyField(wordSpace);
				EditorGUILayout.PropertyField(lineSpace);
				
				if(EditorGUI.EndChangeCheck())
					generate = true;

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(pathOffset);
				
				if(EditorGUI.EndChangeCheck())
					place = true;

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.Space();
				textPathList.DoLayoutList();
				EditorGUILayout.Space();
				
				if(EditorGUI.EndChangeCheck())
					place = length = true;
			}

			if(ligthingShow = EditorGUILayout.Foldout(ligthingShow, "Ligthing"))
			{
				EditorGUI.BeginChangeCheck();

				EditorGUILayout.PropertyField(lightProbes);
				EditorGUILayout.PropertyField(castShadows);
				EditorGUILayout.PropertyField(receiveShadows);
				EditorGUILayout.Space();

				lighting = EditorGUI.EndChangeCheck();
			}

			if(physicsShow = EditorGUILayout.Foldout(physicsShow, "Physics"))
			{
				EditorGUI.BeginChangeCheck();

				EditorGUILayout.PropertyField(colliderType, new GUIContent("Collider"));
				EditorGUILayout.PropertyField(simulatePhysics);

				physics = EditorGUI.EndChangeCheck();
			}

			serializedObject.ApplyModifiedProperties();

			if(generate || clear || place || length || lighting || physics)
			{
				if(serializedObject.isEditingMultipleObjects)
				{
					foreach(Object t in targets)
					{
						actor = target as TextToolsActor;
						actor.Apply(generate, clear, place, length, lighting, physics);
					}
				}
				else
				{
					actor = target as TextToolsActor;
					actor.Apply(generate, clear, place, length, lighting, physics);
				}
			}
		}
	}
}
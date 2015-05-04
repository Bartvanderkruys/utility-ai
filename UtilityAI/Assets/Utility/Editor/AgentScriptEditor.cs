using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(Agent))]
public class AgentScriptEditor : Editor {
	
	private ReorderableList list;
	
	private void OnEnable() {
		list = new ReorderableList(serializedObject, 
		                           serializedObject.FindProperty("Actions"), 
		                           true, true, true, true);


		list.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Actions");
		};

		//lambda function for creating custom inspectors
		list.drawElementCallback =  
		(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
//			EditorGUI.TextField(
//				new Rect(rect.x, rect.y, EditorGUIUtility.fieldWidth, EditorGUIUtility.singleLineHeight),
//				"Action Name: ", element.FindPropertyRelative("actionName"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("time"), GUIContent.none);
		};
	}
	
	public override void OnInspectorGUI()
	{
		//Agent agent = (Agent)target;
		DrawDefaultInspector ();
		serializedObject.Update();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
	}
}

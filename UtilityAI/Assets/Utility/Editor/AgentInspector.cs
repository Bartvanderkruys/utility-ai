using UnityEngine;  
using UnityEditor;  
using UnityEditorInternal;

[CustomEditor(typeof(Agent))]
public class AgentInspector : Editor {  
	private ReorderableList considerationList;

	private void OnEnable() {
		considerationList = new ReorderableList(serializedObject, 
		                           serializedObject.FindProperty("agentConsiderations"), 
		                           true, true, true, true);

		considerationList.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Agent Considerations");
		};
		considerationList.elementHeight = EditorGUIUtility.singleLineHeight * 2 + 5;
		considerationList.drawElementCallback =  
		(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = considerationList.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;

			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, rect.width - 50.0f, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("considerationName"), GUIContent.none);
				EditorGUI.PropertyField(
				new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 2, rect.width * 0.5f, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("minimum_value"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.x + rect.width * 0.5f - 25.0f, rect.y + EditorGUIUtility.singleLineHeight + 2, rect.width * 0.5f - 25.0f, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("maximum_value"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.width, rect.y, EditorGUIUtility.singleLineHeight * 2, EditorGUIUtility.singleLineHeight * 2),
				element.FindPropertyRelative("utilityCurve"), GUIContent.none);
		};
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update();
		considerationList.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
		DrawDefaultInspector ();
	}
}
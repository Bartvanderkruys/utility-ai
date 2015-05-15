using UnityEngine;  
using UnityEditor;  
using UnityEditorInternal;

[CustomEditor(typeof(Agent))]
public class ActionInspector : Editor {  
	private ReorderableList considerationList;
	
	private void OnEnable() {

		considerationList = new ReorderableList(serializedObject, 
		                                        serializedObject.FindProperty("considerations"), 
		                                        true, true, true, true);
		
		considerationList.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Considerations");
		};
		considerationList.elementHeight = EditorGUIUtility.singleLineHeight * 2 + 5;
		considerationList.drawElementCallback =  
		(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = considerationList.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, rect.width - 50.0f, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("name"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight, rect.width - 50.0f, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("property"), GUIContent.none);
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
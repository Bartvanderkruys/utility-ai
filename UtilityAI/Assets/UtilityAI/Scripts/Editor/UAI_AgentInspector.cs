using UnityEngine;  
using UnityEditor;  
using UnityEditorInternal;

[CustomEditor(typeof(UAI_Agent))]
public class UAI_AgentInspector : Editor {  
	private ReorderableList actionList;
	
	private void OnEnable() {

		actionList = new ReorderableList(serializedObject, 
	                                        serializedObject.FindProperty("linkedActions"), 
	                                        true, true, true, true);
		
		actionList.drawHeaderCallback = (Rect rect) => {  
			EditorGUI.LabelField(rect, "Actions");
		};
		actionList.elementHeight = EditorGUIUtility.singleLineHeight + 5;
		actionList.drawElementCallback =  
		(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = actionList.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			
			EditorGUI.PropertyField(
				new Rect(rect.x + 20, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("action"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("enabled"), GUIContent.none);
		};
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update();
		actionList.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
		DrawDefaultInspector ();
	}
}
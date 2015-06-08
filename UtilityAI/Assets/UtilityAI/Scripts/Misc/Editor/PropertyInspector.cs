using UnityEngine;  
using UnityEditor;  
using UnityEditorInternal;

[CustomEditor(typeof(Property))]
public class PropertyInspector : Editor {  

	private void OnEnable() {		
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update();
		serializedObject.ApplyModifiedProperties();
		DrawDefaultInspector ();
	}
}
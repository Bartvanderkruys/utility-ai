using UnityEngine;  
using UnityEditor;  
using UnityEditorInternal;

[CustomEditor(typeof(UAI_Property))]
public class UAI_PropertyInspector : Editor {  

	private void OnEnable() {		
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update();
		serializedObject.ApplyModifiedProperties();
		DrawDefaultInspector ();
	}
}
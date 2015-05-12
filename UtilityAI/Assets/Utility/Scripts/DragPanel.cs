using UnityEngine;
using System.Collections;

public class DragPanel : MonoBehaviour {

	public GameObject panel;

	public void Drag(){
		panel.transform.position = Input.mousePosition + transform.localPosition;
	}
}

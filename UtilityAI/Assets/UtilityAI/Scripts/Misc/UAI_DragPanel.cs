using UnityEngine;
using System.Collections;

public class UAI_DragPanel : MonoBehaviour {

	public GameObject panel;

	public void Drag(){
		panel.transform.position = new Vector3 (Input.mousePosition.x - transform.localPosition.x + 8,
		                                       Input.mousePosition.y - transform.localPosition.y + 8,
		                                       0);
	}
}

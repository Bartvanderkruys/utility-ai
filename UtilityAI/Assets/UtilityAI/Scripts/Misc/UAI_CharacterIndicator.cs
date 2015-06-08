using UnityEngine;

public class UAI_CharacterIndicator : MonoBehaviour 
{ 
	private Camera cam;
	public GameObject indicator;

	void Start(){
		cam = Camera.main;
	}

	void Update() { 
		GetComponent<RectTransform>().rotation = cam.transform.rotation; 
	} 
}
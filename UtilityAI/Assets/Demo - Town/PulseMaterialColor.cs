using UnityEngine;
using System.Collections;

public class PulseMaterialColor : MonoBehaviour {

	public Material mat;
	private bool pulsing = false;
	private Color originalColor;
	private Collider characterCollider;

	void Start(){
		originalColor = mat.GetColor ("_Color");
		characterCollider = GameObject.Find ("Character").GetComponent<BoxCollider> ();
	}

	// Update is called once per frame
	void Update () {
		if (pulsing) {
			float sineColorValue = Mathf.Abs(Mathf.Sin(Time.time * 3.0f))/2;
			mat.SetColor("_Color", new Color(0, 0.5f + sineColorValue, 0));
		} 
	}

	void OnTriggerEnter(Collider col){
		if(col == characterCollider)
		pulsing = true;
	}

	void OnTriggerExit(Collider col){
		if (col == characterCollider) {
			pulsing = false;
			mat.SetColor ("_Color", originalColor);
		}
	}

	void OnApplicationQuit(){
		mat.SetColor ("_Color", originalColor);
	}
}

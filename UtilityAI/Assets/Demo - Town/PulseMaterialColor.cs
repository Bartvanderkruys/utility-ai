using UnityEngine;
using System.Collections;

public class PulseMaterialColor : MonoBehaviour {

	public Material mat;
	private bool pulsing = false;
	private Color originalColor;
	private Collider characterCollider;
	private OverlayUI ui;

	void Start(){
		ui = GameObject.Find ("OverlayUI").GetComponent<OverlayUI> ();
		originalColor = mat.GetColor ("_Color");
	}

	// Update is called once per frame
	void Update () {
		if (pulsing) {
			float sineColorValue = Mathf.Abs(Mathf.Sin(Time.time * 3.0f))/2;
			mat.SetColor("_Color", new Color(0, 0.5f + sineColorValue, 0));
		} 
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player")
		pulsing = true;
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.tag == "Player") {
			pulsing = false;
			mat.SetColor ("_Color", originalColor);
		}
	}

	void OnApplicationQuit(){
		mat.SetColor ("_Color", originalColor);
	}
}

using UnityEngine;
using System.Collections;

public class SlidingDoors : MonoBehaviour {

	public GameObject doorLeft;
	public GameObject doorRight;
	
	void OnTriggerEnter(Collider col){
		doorLeft.transform.localPosition = new Vector3 (-8.0f, 0.0f, 0.0f);
		doorRight.transform.localPosition = new Vector3 (3.0f, 0.0f, 0.0f);
	}

	void OnTriggerExit(Collider col){
		doorLeft.transform.localPosition = new Vector3 (-5.0f, 0.0f, 0.0f);
		doorRight.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
	}

	IEnumerator DoorOpen(){
		bool isOpen = false;
		while (!isOpen) {
			if (doorLeft.transform.position.x < -10.0f) {
				doorLeft.transform.position = new Vector3 (-10.0f, 0.0f, 0.0f);
				doorRight.transform.position = new Vector3 (5.0f, 0.0f, 0.0f);
				isOpen = true;
			} else {
				doorLeft.transform.Translate (new Vector3 (-1.0f, 0.0f, 0.0f));
				doorRight.transform.Translate (new Vector3 (1.0f, 0.0f, 0.0f));
			}
			yield return new WaitForSeconds (0.0f);
		}
	}

	IEnumerator DoorClosed(){
		bool isClosed = false;
		while (!isClosed) {
			if (doorLeft.transform.position.x > -5.0f) {
				doorLeft.transform.position = new Vector3 (-5.0f, 0.0f, 0.0f);
				doorRight.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
				isClosed = true;
			} else {
				doorLeft.transform.Translate (new Vector3 (1.0f , 0.0f, 0.0f));
				doorRight.transform.Translate (new Vector3 (-1.0f * Time.deltaTime, 0.0f, 0.0f));
			}
			yield return new WaitForSeconds (0.0f);
		}
	}
}

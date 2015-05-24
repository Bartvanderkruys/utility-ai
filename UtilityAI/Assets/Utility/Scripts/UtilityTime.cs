using UnityEngine;
using System.Collections;

public class UtilityTime : MonoBehaviour {

	public static float time;
	public static float speed = 1.0f;

	void Update () {
		time = Time.deltaTime * speed;
	}
}

using UnityEngine;
using System.Collections;

public class UtilityTime : MonoBehaviour {

	public static float time;
	public static float speed = 1.0f;
	public static bool paused = false;

	void Update () {
		if(!paused)
			time = Time.deltaTime * speed;
		else
			time = 0.0f;
	}
}

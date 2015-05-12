using UnityEngine;
using System.Collections;

public class PropertyBoundedFloat : Property {
	public float minValue = 0.0f;
	public float maxValue = 100.0f;
	public float startValue = 50.0f;
	public float decreasePerSec = 0.0f;
	public float currValue;


	public float current {
		get{ return currValue; }
		set{ currValue = value; }
	}

	// Use this for initialization
	void Start () {
		currValue = startValue;
	}
	
	// Update is called once per frame
	void Update () {
		currValue -= Time.deltaTime * decreasePerSec;
		if (currValue < minValue)
			currValue = minValue;
		if (currValue > maxValue)
			currValue = maxValue;

		normalized_value = (currValue - minValue) / (maxValue - minValue);
	}
}

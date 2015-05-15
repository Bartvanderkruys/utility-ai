using UnityEngine;
using System.Collections;

public class PropertyBoundedFloat : Property {
	public float minValue = 0.0f;
	public float maxValue = 100.0f;
	public float startValue = 50.0f;
	public float decreasePerSec = 0.0f;
	private float currValue;


	public float value {
		get{ return currValue; }
		set{ 
			currValue = value; 
			nValue = (currValue - minValue) / (maxValue - minValue);
			if (currValue < minValue)
				currValue = minValue;
			if (currValue > maxValue)
				currValue = maxValue;
		}
	}

	// Use this for initialization
	void Start () {
		currValue = startValue;
	}
	
	// Update is called once per frame
	void Update () {
		value -= Time.deltaTime * decreasePerSec;
	}
}

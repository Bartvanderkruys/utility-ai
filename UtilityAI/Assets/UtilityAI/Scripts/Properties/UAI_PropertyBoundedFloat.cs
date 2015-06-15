using UnityEngine;
using System.Collections;

[AddComponentMenu("UtilityAI/Float Property")]
public class UAI_PropertyBoundedFloat : UAI_Property {

	public float minValue = 0.0f;
	public float maxValue = 100.0f;
	public float startValue = 50.0f;
	public float ChangePerSec = 0.0f;
	private float currValue;

	void Start(){
		if (randomizeStartValue)
			currValue = Random.Range (minValue, maxValue) + minValue;
		else
			currValue = startValue;
	}

	void Update () {
		value += UtilityTime.time * ChangePerSec;
	}

	public float value {
		get{ return currValue; }
		set{ 
			currValue = value; 
			if (currValue < minValue)
				currValue = minValue;
			if (currValue > maxValue)
				currValue = maxValue;
			nValue = (currValue - minValue) / (maxValue - minValue);
		}
	}
}

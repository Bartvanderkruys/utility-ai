using UnityEngine;
using System.Collections;

[AddComponentMenu("UtilityAI/Double Property")]
public class UAI_PropertyBoundedDouble : UAI_Property {

	public double minValue = 0.0d;
	public double maxValue = 100.0d;
	public double startValue = 50.0d;
	public double ChangePerSec = 0.0d;
	private double currValue;
	
	void Start(){
		if (randomizeStartValue)
			currValue = Random.value * (maxValue - minValue) + minValue;
		else
			currValue = startValue;
	}
	
	void Update () {
		value += UtilityTime.time * ChangePerSec;
	}
	
	public double value {
		get{ return currValue; }
		set{ 
			currValue = value; 
			if (currValue < minValue)
				currValue = minValue;
			if (currValue > maxValue)
				currValue = maxValue;
			nValue = (float)((currValue - minValue) / (maxValue - minValue));
		}
	}
}

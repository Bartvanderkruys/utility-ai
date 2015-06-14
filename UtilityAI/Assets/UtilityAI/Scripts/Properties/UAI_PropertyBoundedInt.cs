using UnityEngine;
using System.Collections;

[AddComponentMenu("UtilityAI/Int Property")]
public class UAI_PropertyBoundedInt : UAI_Property {

	public int minValue = 0;
	public int maxValue = 100;
	public int startValue = 50;
	//public int ChangePerSec = 0;
	private int currValue;
	
	void Start(){
		if (randomizeStartValue)
			currValue = Mathf.FloorToInt(Random.Range(minValue+1, maxValue+1)) -1 + minValue;
		else
			currValue = startValue;
	}
	
	void Update () {
		//value += UtilityTime.time * ChangePerSec;
	}
	
	public int value {
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
}
using UnityEngine;
using System;

[Serializable]
public class Consideration{
	public string considerationName;
	public float minimum_value;
	public float maximum_value;
	public AnimationCurve utilityCurve;
	private float value;
	public Property property;

	public float GetValue()
	{
		return value;
	}
	public void SetValue(float p_value)
	{
		value = p_value;
	}

	public float GetUtilityScore()
	{
		//normalize
		float x = value / (maximum_value - minimum_value);
		//plot on utility graph
		float utilityScore = 1 - utilityCurve.Evaluate(x);
		//return score
		//Debug.Log (utilityScore);
		return utilityScore;
	}
}

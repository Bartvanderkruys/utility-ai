using UnityEngine;
using System;

[Serializable]
public class Consideration{
	public float minimum_value;
	public float maximum_value;
	public AnimationCurve utilityCurve;
	
	private float value;

	public float GetValue()
	{
		return value;
	}
	public void SetValue(ref float p_value)
	{
		value = p_value;
	}
}

using UnityEngine;
using System;

[Serializable]
public struct Consideration{
	public string considerationName;
	public float minimum_value;
	public float maximum_value;
	public AnimationCurve utilityCurve;
	public float value;

	public float GetValue()
	{
		return value;
	}
	public void SetValue(float p_value)
	{
		value = p_value;
	}
}

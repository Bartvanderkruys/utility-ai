using UnityEngine;
using System;

[Serializable]
public struct Consideration{
	public string name;
	public float minimum_value;
	public float maximum_value;
	public AnimationCurve utilityCurve;
}

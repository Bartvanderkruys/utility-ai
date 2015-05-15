using UnityEngine;
using System;

[Serializable]
public class Consideration {
	public string name;
	public AnimationCurve utilityCurve;
	public Property property;

	public float propertyScore{
		get { return property.normalizedValue; }
	}

	public float utilityScore {
		get { return 1 - utilityCurve.Evaluate (property.normalizedValue); }
	}
}

using UnityEngine;
using System;

[Serializable]
public class Consideration {
	public AnimationCurve utilityCurve;
	public Property property;
	public float weight = 1.0f;

	public float propertyScore{
		get { return property.normalizedValue; }
	}

	public float utilityScore {
		get { return 1 - utilityCurve.Evaluate (property.normalizedValue); }
	}
}

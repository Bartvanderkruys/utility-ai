using UnityEngine;
using System;

[Serializable]
public class UAI_Consideration {
	public AnimationCurve utilityCurve;
	public UAI_Property property;
	public float weight = 1.0f;
	public bool enabled = true;

	public float propertyScore{
		get { return property.normalizedValue; }
	}

	public float utilityScore {
		get { return utilityCurve.Evaluate (property.normalizedValue);}
	}
}

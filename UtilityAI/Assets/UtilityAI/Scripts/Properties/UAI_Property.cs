using UnityEngine;
using System;

[Serializable]
public class UAI_Property : MonoBehaviour{
	protected float nValue;

	public float normalizedValue {
		get{ return nValue; }
	}

	public virtual void SetFloatValue(float value){}
	public virtual float GetFloatMax(){return nValue;}

	public bool modifiable;
}

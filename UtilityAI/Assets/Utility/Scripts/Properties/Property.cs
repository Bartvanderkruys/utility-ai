using UnityEngine;
using System;

[Serializable]
public class Property : MonoBehaviour{
	protected float nValue;

	public float normalizedValue {
		get{ return nValue; }
	}

	void Start(){}

	void Update(){}
}

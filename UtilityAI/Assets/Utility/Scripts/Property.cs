using UnityEngine;
using System;

[Serializable]
public class Property : MonoBehaviour{
	protected float normalized_value;

	public float need {
		get{ return normalized_value; }
	}

	void Start(){}

	void Update(){}
}

using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Action{
	public string actionName;
	//time required to perform action;
	public float time;
	//function delegate
	public delegate void Del();
	public Del handle;

	//appropriate considerations
	[HideInInspector]
	public List<Consideration> considerations;

	//action score, for UI
	private float actionScore;
	public float GetActionScore()
	{
		return actionScore;
	}
	public void SetActionScore(float val)
	{
		actionScore = val;
	}
}

using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Action : MonoBehaviour{
	public string actionName;
	//time required to perform action;
	public float time;
	//function delegate
	public delegate void Del();
	public Del handle;
	
	//appropriate weighted considerations
	[HideInInspector]
	public List<Consideration> considerations = new List<Consideration>();

	private float actionScore;

	public void EvaluateAction(){
		actionScore = 0.0f;
		//evaluate appropriate considerations
		for (int j = 0; j < considerations.Count; j++){
			//normalize value
			actionScore += considerations[j].utilityScore;
		}
		//determine average
		actionScore = actionScore / considerations.Count;
	}

	public float GetActionScore()
	{
		return actionScore;
	}
	public void SetActionScore(float val)
	{
		actionScore = val;
	}
}

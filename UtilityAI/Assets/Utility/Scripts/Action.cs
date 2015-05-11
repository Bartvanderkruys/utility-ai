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
	
	//appropriate weighted considerations
	public List<LinkedConsideration>linkedConsideration;
	
	private float actionScore;

	public void EvaluateAction(){
		actionScore = 0.0f;
		//evaluate appropriate considerations
		for (int j = 0; j < linkedConsideration.Count; j++){
			//normalize value
			actionScore += linkedConsideration[j].GetConsideration().GetUtilityScore();
		}
		//determine average
		actionScore = actionScore / linkedConsideration.Count;
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

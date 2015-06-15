using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
[AddComponentMenu("UtilityAI/Action")]
public class UAI_Action : MonoBehaviour{
	//time required to perform action;
	public float time;
	//function delegate
	public delegate void Del();
	public Del handle;
	public int priorityLevel;
	public bool interruptible;

	//appropriate weighted considerations
	[HideInInspector]
	public List<UAI_Consideration> considerations = new List<UAI_Consideration>();

	private float actionScore;

	public void EnableConsideration(string propertyName){
		for (int i = 0; i < considerations.Count; i++) {
			if(considerations[i].property.name == propertyName)
				considerations[i].enabled = true;
		}
	}

	public void DisableConsideration(string propertyName){
		for (int i = 0; i < considerations.Count; i++) {
			if(considerations[i].property.name == propertyName)
				considerations[i].enabled = false;
		}
	}

	public void EvaluateAction(){
		actionScore = 0.0f;
		int enabledConsiderationsCount = 0;
		//evaluate appropriate considerations
		for (int i = 0; i < considerations.Count; i++){
			//calc utility score
			if(considerations[i].enabled) {
				actionScore += considerations[i].utilityScore * considerations[i].weight;
				enabledConsiderationsCount ++;
			}
		}
		//determine average
		actionScore = actionScore / enabledConsiderationsCount;
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

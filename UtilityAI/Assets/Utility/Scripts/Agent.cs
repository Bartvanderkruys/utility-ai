// This component is added to an actor. 
// It collects all actions and considerations and
// it evaluates which action has the highest utility score

using UnityEngine;
using System.Collections.Generic;

public class Agent : MonoBehaviour {
	
	public List<Action> actions = new List<Action>();
	private Action previousAction, topAction;
	[HideInInspector]
	public float actionTimer = 0.0f;
	private bool isTiming = false;
	[HideInInspector]
	public bool newAction;

	void Start(){
		Evaluate ();
	}

	public void UpdateAI(){
		if (actionTimer > 0.0f && isTiming) {
			actionTimer -= Time.deltaTime;
			GetTopAction ().handle ();
		} else if (actionTimer > 0.0f) {
			GetTopAction ().handle ();
		} else {
			StopTimer();
			Evaluate ();
			actionTimer = GetTopAction().time;
		}
	}

	public void SetVoidActionDelegate(string name, Action.Del del)
	{
		for (int i = 0; i < actions.Count; i++) {
			if (actions[i].actionName == name)
			{
				actions[i].handle = del;
				return;
			}
		}
		Debug.Log ("Setting Action Delegate failed. Action: " + name + " Does not exist.");
	}

	public void StartTimer(){
		isTiming = true;
	}

	public void StopTimer(){
		isTiming = false;
	}

	public void Evaluate(){

		previousAction = topAction;
		float topActionScore = 0.0f;

		for (int i = 0; i < actions.Count; i++) {
			actions[i].EvaluateAction();
			if(actions[i].GetActionScore() > topActionScore)
			{
				topAction = actions[i];
				topActionScore = actions[i].GetActionScore();
			}	
		}
		if(topAction != previousAction)
			newAction = true;
	}

	public Action GetTopAction()
	{
		return topAction;
	}
}
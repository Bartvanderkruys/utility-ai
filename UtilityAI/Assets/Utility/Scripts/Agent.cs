// This component is added to an actor. 
// It collects all actions and considerations and
// it evaluates which action has the highest utility score

using UnityEngine;
using System.Collections.Generic;

public class Agent : MonoBehaviour {

	[HideInInspector]
	public List<Action> actions = new List<Action>();
	private Action topAction;
	[HideInInspector]
	public float actionTimer = 0.0f;

	void Start(){
		Evaluate ();
	}

	public void UpdateAI(){
		if (actionTimer > 0.0f) {
			actionTimer -= Time.deltaTime;
			GetTopAction().handle();
		} else {
			//evaluate top action and store time and delegate
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

	public void Evaluate(){
		//topAction = GetActionByName("Drink Coffee");
		float topActionScore = 0.0f;
		//for each action
		for (int i = 0; i < actions.Count; i++) {
			actions[i].EvaluateAction();
			//if the score is the highest, set the action as the next action
			if(actions[i].GetActionScore() > topActionScore)
			{
				topAction = actions[i];
				topActionScore = actions[i].GetActionScore();
			}	
		}
	}

	public Action GetTopAction()
	{
		return topAction;
	}
}
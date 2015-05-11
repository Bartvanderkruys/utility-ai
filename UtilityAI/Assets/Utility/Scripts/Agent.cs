// This component is added to an actor. 
// It collects all actions and considerations and
// it evaluates which action has the highest utility score

using UnityEngine;
using System.Collections.Generic;

public class Agent : MonoBehaviour {

	[HideInInspector]
	public List<Consideration> agentConsiderations = new List<Consideration>();
	public List<Action> actions = new List<Action>();
	private Action topAction;
	[HideInInspector]
	public float actionTimer = 0.0f;

	void Start(){
		//link considerations to actions using the string names
		for (int i = 0; i < actions.Count; i++) {
			for (int j = 0; j < actions[i].linkedConsideration.Count; j++) {
				for (int k = 0; k < agentConsiderations.Count; k++) {
					if(actions[i].linkedConsideration[j].name == agentConsiderations[k].considerationName){
						actions[i].linkedConsideration[j].SetConsideration(agentConsiderations[k]);
					}
				}
			}
		}
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

	public Action GetActionByName(string name){
		for (int i = 0; i < actions.Count; i++) {
			if (actions [i].actionName == name) {
				return actions[i];
			}
		}
		Debug.Log ("Action: " + name + " Does not exist.");
		return null;
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

	public Consideration GetAgentConsiderationByName(string name){
		for (int i = 0; i < agentConsiderations.Count; i++) {
			if (agentConsiderations[i].considerationName == name)
			{
				return agentConsiderations[i];
			}
		}
		Debug.Log ("Consideration: " + name + " Does not exist.");
		return null;
	}
	
	public void SetAgentConsideration(string name, float value)
	{
		for (int i = 0; i < agentConsiderations.Count; i++) {
			if (agentConsiderations[i].considerationName == name)
			{
				agentConsiderations[i].SetValue(value);
				return;
			}
		}
		Debug.Log ("Setting Consideration failed. Consideration: " + name + " Does not exist.");
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
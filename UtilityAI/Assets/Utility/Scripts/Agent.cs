// This component is added to an actor. 
// It collects all actions and considerations and
// it evaluates which action has the highest utility score

using UnityEngine;
using System.Collections.Generic;

public class Agent : MonoBehaviour {

	public List<Consideration> agentConsiderations = new List<Consideration>();
	public List<Action> actions = new List<Action>();
	private Action topAction;

	void Start(){
		//link considerations to actions using the string names
		for (int i = 0; i < actions.Count; i++) {
			for (int j = 0; j < actions[i].linkedConsideration.Count; j++) {
				for (int k = 0; k < agentConsiderations.Count; k++) {
					if(actions[i].linkedConsideration[j] == agentConsiderations[k].considerationName){
						actions[i].considerations.Add(agentConsiderations[k]);
					}
				}
			}
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
		topAction = GetActionByName("Drink Coffee");
		float topActionScore = 0.0f;
		//Debug.Log (++evaluationCounter);

		//Debug.Log ("Evaluating");
		//for each action
		for (int i = 0; i < actions.Count; i++) {
			float actionScore = 0.0f;
			//evaluate appropriate considerations
			for (int j = 0; j < actions[i].considerations.Count; j++){
				//normalize value
				actionScore += actions[i].considerations[j].GetUtilityScore();
			}
			//determine average
			actionScore = actionScore / actions[i].considerations.Count;
			actions[i].SetActionScore(actionScore);
			//Debug.Log ("actionScore of " + actions[i].actionName + ": " + actionScore + " " + actions[i].GetActionScore());
			//if the score is the highest, set the action as the next action
			if(actionScore > topActionScore)
			{
				topAction = actions[i];
				topActionScore = actionScore;
			}			
		}
		//Debug.Log (topAction.actionName);
	}

	public Action GetTopAction()
	{
		return topAction;
	}
}
// This component is added to an actor. 
// It collects all actions and considerations and
// it evaluates which action has the highest utility score

using UnityEngine;
using System.Collections.Generic;

public class Agent : MonoBehaviour {

	public List<Consideration> agentConsiderations = new List<Consideration>();
	public List<Action> actions = new List<Action>();

	void OnStart(){
		//link considerations to actions using the string names
		for (int i = 0; i < actions.Count; i++) {
			for (int j = 0; j < actions[i].linkedConsideration.Count; j++) {
				for (int k = 0; k < agentConsiderations.Count; k++) {
					if(actions[i].linkedConsideration[j] == agentConsiderations[k].considerationName){
						actions[i].considerations.Add(agentConsiderations[k]);
						break;
					}
				}
			}
		}
	}

	public void SetAgentConsideration(string name, ref float value)
	{
		for (int i = 0; i < agentConsiderations.Count; i++) {
			if (agentConsiderations[i].considerationName == name)
			{
				Consideration temp = agentConsiderations[i];
				temp.value = value;
				//Debug.Log (agentConsiderations[i].considerationName);
				break;
			}
		}
		Debug.Log (value + "  " + agentConsiderations[0].value);
	}
}

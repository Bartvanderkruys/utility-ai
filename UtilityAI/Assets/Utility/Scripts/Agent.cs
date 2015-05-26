// This component is added to an actor. 
// It collects all actions and considerations and
// it evaluates which action has the highest utility score

using UnityEngine;
using System.Collections.Generic;

public class Agent : MonoBehaviour {

	public string agentName;
	public int historyStates = 10;
	public bool randomStartProperties;
	public List<Action> actions = new List<Action>();

	[HideInInspector]
	public List<string> actionHistory = new List<string>();
	private Action previousAction, topAction;
	[HideInInspector]
	public float actionTimer = 0.0f;
	private float currentActionScore;
	private bool isTiming = false;
	[HideInInspector]
	public bool newAction;
	private bool paused = false;

	void Start(){
		Evaluate ();

		if (randomStartProperties) {
			Property[] properties = GetComponentsInChildren<Property>();
			for(int i = 0; i < properties.Length; i++){
				properties[i].SetFloatValue( Random.value * properties[i].GetFloatMax() );
			}
		}
	}

	public void UpdateAI(){
		if (!paused) {
			if (actionTimer > 0.0f && isTiming) {
				actionTimer -= UtilityTime.time;
				GetTopAction ().handle ();
				if (GetTopAction ().interruptible) {
					if (EvaluateInteruption ()) {
						actionTimer = GetTopAction ().time;
					}
				}
			} else if (actionTimer > 0.0f) {
				GetTopAction ().handle ();
			} else {
				StopTimer ();
				Evaluate ();
				actionTimer = GetTopAction ().time;
			}
		}
	}

	public void SetVoidActionDelegate(string name, Action.Del del)
	{
		for (int i = 0; i < actions.Count; i++) {
			if (actions[i].name == name)
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

	public void Pause(){
		if (!paused)
			paused = true;
		else
			paused = false;
	}

	public bool IsPaused(){
		return paused;
	}

	public float Evaluate(){

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
		if (topAction != previousAction)
			newAction = true;
		else
			StartTimer ();

		actionHistory.Add (topAction.name);
		if (actionHistory.Count > historyStates){
			actionHistory.RemoveAt(0);
		}

		currentActionScore = topActionScore;
		return topActionScore;
	}

	public bool EvaluateInteruption(){

		int topActionPriority = topAction.priorityLevel;
		float topActionScore = 0.0f;
		Action topInterruption = topAction;
		bool validInterruption = false;
		
		for (int i = 0; i < actions.Count; i++) {
			if(actions[i].priorityLevel < topActionPriority){
				actions[i].EvaluateAction();
				if(actions[i].GetActionScore() > currentActionScore && 
				   actions[i].GetActionScore() > topActionScore)
				{
					topInterruption = actions[i];
					topActionScore = actions[i].GetActionScore();
					validInterruption = true;
				}	
			}
		}

		if (validInterruption) {
			newAction = true;
			topAction = topInterruption;
			actionHistory.Add ("Interruption: " + topAction.name);
			if (actionHistory.Count > historyStates){
				actionHistory.RemoveAt(0);
			}
			currentActionScore = topActionScore;
			return true;
		}
		return false;
	}

	public Action GetTopAction()
	{
		return topAction;
	}
}
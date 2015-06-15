// This component is added to an actor. 
// It collects all actions and considerations and
// it evaluates which action has the highest utility score

using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("UtilityAI/Agent")]
public class UAI_Agent : MonoBehaviour {

	public string agentName;
	public int historyStates = 10;
	public GameObject characterIndicator;
	[HideInInspector]
	public List<UAI_LinkedAction> linkedActions = new List<UAI_LinkedAction>();
	[HideInInspector]
	public List<string> actionHistory = new List<string>();
	private UAI_Action previousAction, topAction;
	[HideInInspector]
	public float actionTimer = 0.0f;
	private float currentActionScore;
	private bool isTiming = false;
	[HideInInspector]
	public bool newAction;
	private bool paused = false;

	void Start(){
		Evaluate ();
	}

	public void EnableAction(string actionName){
		for (int i = 0; i < linkedActions.Count; i++) {
			if(linkedActions[i].action.name == actionName)
				linkedActions[i].enabled = true;
		}
	}

	public void DisableAction(string actionName){
		for (int i = 0; i < linkedActions.Count; i++) {
			if(linkedActions[i].action.name == actionName)
			{
				linkedActions[i].enabled = false;
				linkedActions[i].action.SetActionScore(0.0f); 
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

	public void SetVoidActionDelegate(string name, UAI_Action.Del del)
	{
		for (int i = 0; i < linkedActions.Count; i++) {
			if (linkedActions[i].action.name == name)
			{
				linkedActions[i].action.handle = del;
				return;
			}
		}
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

		if(topAction != null)
			previousAction = topAction;

		float topActionScore = 0.0f;

		for (int i = 0; i < linkedActions.Count; i++) {
			if(linkedActions[i].enabled == true){
				linkedActions[i].action.EvaluateAction();
				if(linkedActions[i].action.GetActionScore() > topActionScore)
				{
					topAction = linkedActions[i].action;
					topActionScore = linkedActions[i].action.GetActionScore();
				}	
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
		UAI_Action topInterruption = topAction;
		bool validInterruption = false;
		
		for (int i = 0; i < linkedActions.Count; i++) {
			if(linkedActions[i].enabled == true){
				if(linkedActions[i].action.priorityLevel < topActionPriority){
					linkedActions[i].action.EvaluateAction();
					if(linkedActions[i].action.GetActionScore() > currentActionScore && 
					   linkedActions[i].action.GetActionScore() > topActionScore)
					{
						topInterruption = linkedActions[i].action;
						topActionScore = linkedActions[i].action.GetActionScore();
						validInterruption = true;
					}	
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

	public UAI_Action GetTopAction()
	{
		return topAction;
	}
}
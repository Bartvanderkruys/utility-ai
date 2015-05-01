// This component is added to an actor. 
// It collects all actions and considerations and
// it evaluates which action has the highest utility score

using UnityEngine;
using System.Collections.Generic;

public class Agent : MonoBehaviour {

	[HideInInspector]
	public List<Action> Actions = new List<Action>();
	//public List<Consideration> Considerations = new List<Consideration>();
	
}

using UnityEngine;
using System.Collections.Generic;

public class OverlayUIConsiderationPanel : MonoBehaviour {

	//list of agents
	private Agent[] agents;
	public GameObject element;

	private List<GameObject> elements = new List<GameObject>();

	void Start () {
		//find all agents and add to list
		agents = FindObjectsOfType (typeof(Agent)) as Agent[];

		//create all consideration and action elements
		for (int i = 0; i < agents.Length; i++) {
			for(int j = 0; j < agents[i].agentConsiderations.Count; j++) {
				GameObject temp = Instantiate(element, new Vector3(transform.position.x, transform.position.y + elements.Count * 25, transform.position.z), 	Quaternion.identity) as GameObject;
				temp.transform.parent = transform;
				temp.GetComponent<OverlayUIConsiderationElement>().SetConsideration(agents[i].agentConsiderations[j]);
				elements.Add (temp);
			}
		}
	}
	
	void Update () {
		for (int i = 0; i < elements.Count; i++) {
			elements[i].GetComponent<OverlayUIConsiderationElement>().SetConsiderationUI();
		}
	}
}

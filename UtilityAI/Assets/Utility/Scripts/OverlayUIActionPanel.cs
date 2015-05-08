using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OverlayUIActionPanel : MonoBehaviour {
	
	//list of agents
	private Agent[] agents;
	public GameObject element;
	public GameObject actionPanel;

	private bool mouseDown = false;
	
	private List<GameObject> elements = new List<GameObject>();
	
	void Start () {
		//find all agents and add to list
		agents = FindObjectsOfType (typeof(Agent)) as Agent[];
		
		//create all consideration and action elements
		for (int i = 0; i < agents.Length; i++) {
			for(int j = 0; j < agents[i].actions.Count; j++) {
				GameObject temp = Instantiate(element, new Vector3(transform.position.x, transform.position.y + elements.Count * -25, transform.position.z), 	Quaternion.identity) as GameObject;
				temp.transform.SetParent(transform);
				temp.GetComponent<OverlayUIActionElement>().SetAction(agents[i].actions[j]);
				elements.Add (temp);
			}
		}
	}
	
	void Update () {
		for (int i = 0; i < elements.Count; i++) {
			elements[i].GetComponent<OverlayUIActionElement>().SetActionUI();
		}
		
		if (mouseDown) {
			actionPanel.transform.position = Input.mousePosition;
			Debug.Log ("Moving Panel");
		}
		
	}
	
	public void MovePanel()
	{
		if (mouseDown) {
			mouseDown = false;
		} else {
			mouseDown = true;
		}
		Debug.Log (mouseDown);
	}
}

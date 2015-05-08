using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OverlayUIConsiderationPanel : MonoBehaviour {

	//list of agents
	private Agent[] agents;
	public GameObject element;
	public GameObject considerationPanel;
	private RectTransform rect;

	private bool mouseDown = false;

	private List<GameObject> elements = new List<GameObject>();

	void Start () {
		//find all agents and add to list
		agents = FindObjectsOfType (typeof(Agent)) as Agent[];

		//create all consideration and action elements
		for (int i = 0; i < agents.Length; i++) {
			for(int j = 0; j < agents[i].agentConsiderations.Count; j++) {
				GameObject temp = Instantiate(element, new Vector3(transform.position.x, transform.position.y + elements.Count * -25, transform.position.z), 	Quaternion.identity) as GameObject;
				temp.transform.SetParent(transform);
				temp.GetComponent<OverlayUIConsiderationElement>().SetConsideration(agents[i].agentConsiderations[j]);
				elements.Add (temp);
			}
		}
		rect.sizeDelta = new Vector2(rect.sizeDelta.x, elements.Count *25);
	}
	
	void Update () {
		for (int i = 0; i < elements.Count; i++) {
			elements[i].GetComponent<OverlayUIConsiderationElement>().SetConsiderationUI();
		}

		if (mouseDown) {
			considerationPanel.transform.position = Input.mousePosition;
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

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OverlayUI : MonoBehaviour {

	public Text currentActionText, actionTimerText;

	private Agent[] agents;
	public GameObject considerationElement, actionElement, considerationContent, actionContent;
	public GameObject actionPanel, considerationPanel, eventPanel, utilityPanel;
	public GameObject showActionsPanel, showConsiderationPanel, showEventPanel, showUtilityPanel;

	public Image utilityCurveRenderer;
	
	private List<GameObject> actionElements = new List<GameObject>();
	private List<GameObject> considerationElements = new List<GameObject>();

	// Use this for initialization
	void Start () {
		agents = FindObjectsOfType (typeof(Agent)) as Agent[];
		BuildUtilityCurve (agents[0].actions[0].considerations[0]);
		//create all consideration and action elements
		for (int i = 0; i < agents.Length; i++) {
			for(int j = 0; j < agents[i].actions.Count; j++) {
				GameObject tempAct = Instantiate(actionElement, 
				                              new Vector3(actionContent.transform.position.x, 
				            actionContent.transform.position.y + actionElements.Count * -25, 
				            actionContent.transform.position.z), 	Quaternion.identity) as GameObject;
				tempAct.transform.SetParent(actionContent.transform);
				tempAct.GetComponent<OverlayUIActionElement>().SetAction(agents[i].actions[j]);
				actionElements.Add (tempAct);

				for(int k = 0; k < agents[i].actions[j].considerations.Count; k++){
					GameObject tempCon = Instantiate(considerationElement, 
					                              new Vector3(considerationContent.transform.position.x, 
					            considerationContent.transform.position.y + considerationElements.Count * -28, 
					            considerationContent.transform.position.z), 	Quaternion.identity) as GameObject;
					tempCon.transform.SetParent(considerationContent.transform);
					tempCon.GetComponent<OverlayUIConsiderationElement>().SetConsideration(agents[i].actions[j].considerations[k]);
					considerationElements.Add (tempCon);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < considerationElements.Count; i++) {
			considerationElements[i].GetComponent<OverlayUIConsiderationElement>().SetConsiderationUI();
		}
		for (int i = 0; i < actionElements.Count; i++) {
			actionElements[i].GetComponent<OverlayUIActionElement>().SetActionUI();
		}
		currentActionText.text = "Current Action: " + agents [0].GetTopAction().actionName;
		actionTimerText.text = "Time Left: " + agents [0].actionTimer.ToString ("0.00");
	}

	void BuildUtilityCurve(Consideration con){
		Texture2D texture = new Texture2D (128, 128, TextureFormat.RGBA32, false);
			for (int i = 0; i < 128; i++) {
			int y = Mathf.FloorToInt(con.utilityCurve.Evaluate(i/128.0f)*128.0f);
			texture.SetPixel (i, y, Color.black);
		}
		texture.Apply ();
		Rect rect = utilityCurveRenderer.sprite.rect;
		utilityCurveRenderer.sprite = Sprite.Create (texture, rect, new Vector2 (0.5f, 0.5f));
	}

	public void OnClickHide(string button){
		if (button == "HideActions") {
			actionPanel.SetActive (false);
			showActionsPanel.SetActive (true);
		} else if (button == "HideConsiderations") {
			considerationPanel.SetActive (false);
			showConsiderationPanel.SetActive (true);
		} else if (button == "HideEvents") {
			eventPanel.SetActive (false);
			showEventPanel.SetActive (true);
		} else if (button == "HideUtilityCurve") {
			utilityPanel.SetActive (false);
			showUtilityPanel.SetActive (true);
		}
	}

	public void OnClickShow(string button){
		if (button == "ShowActions") {
			actionPanel.SetActive (true);
			showActionsPanel.SetActive (false);
		} else if (button == "ShowConsiderations") {
			considerationPanel.SetActive (true);
			showConsiderationPanel.SetActive (false);
		} else if (button == "ShowEvents") {
			eventPanel.SetActive (true);
			showEventPanel.SetActive (false);
		} else if (button == "ShowUtilityCurve") {
			utilityPanel.SetActive (true);
			showUtilityPanel.SetActive (false);
		}
	}
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OverlayUI : MonoBehaviour {

	public Text currentActionText, actionTimerText;

	private UAI_Agent[] agents;
	private UAI_Agent displayedAgent;
	private UAI_Consideration displayedConsideration, displayedActionConsideration;
	private bool displayingCurve = false, displayingAgent = false, displayingActionCurve = false;

	public GameObject ModifiablePropertyElement, PropertyElement, considerationElement, actionElement, agentElement, historyElement, 
					  propertyContent, considerationContent, actionContent, agentContent, actionHistoryContent, actionConsiderationContent,
					  actionsPanel, propertiesPanel, eventsPanel, propertyConsiderationsPanel, agentsPanel, utilityCurve, actionConsiderationCurve,
					  showActionsPanel, showPropertiesPanel, showEventPanel, showAgentsPanel, actionConsiderationsPanel,	
	                  utilityIndicator, propertyIndicator, actionPropertyIndicator, actionUtilityIndicator;

	public Text utilitySpeedText;
	public Image utilityCurveRenderer, actionUtilityCurveRenderer;
	public Button pauseButton;

	private List<UAI_Property> agentProperties = new List<UAI_Property>();	
	private List<GameObject> actionElements = new List<GameObject>();
	private List<GameObject> agentElements = new List<GameObject>();
	private List<GameObject> considerationElements = new List<GameObject>();
	private List<GameObject> actionConsiderationElements = new List<GameObject>();
	private List<GameObject> propertyElements = new List<GameObject>();
	private List<GameObject> historyElements = new List<GameObject>();

	private UAI_Action selectedAction;
	private UAI_Property selectedProperty;
	private UAI_Consideration selectedPropertyConsideration, selectedActionConsideration;
	private ColorBlock normalColorBlock, selectedColorBlock;

	// Use this for initialization
	void Start () {

		normalColorBlock = pauseButton.colors;
		selectedColorBlock = pauseButton.colors;
		selectedColorBlock.normalColor = new Color (0.5f, 0.5f, 0.5f, 1.0f);

		agents = FindObjectsOfType (typeof(UAI_Agent)) as UAI_Agent[];

		//create all consideration and action elements
		for (int i = 0; i < agents.Length; i++) {
			//populate UI agents
			GameObject tempAgent = Instantiate(agentElement, 
			                                   new Vector3(agentContent.transform.position.x +100, 
			            agentContent.transform.position.y + agentElements.Count * - 22, 
			            agentContent.transform.position.z), 	Quaternion.identity) as GameObject;
			tempAgent.transform.SetParent(agentContent.transform);
			tempAgent.GetComponent<OverlayUIAgentElement>().SetAgent(agents[i]);
			agentElements.Add (tempAgent);
		}
		agentContent.GetComponent<RectTransform>().sizeDelta = new Vector2(200, agentElements.Count * 22);
		utilitySpeedText.text = "Speed: " + UtilityTime.speed.ToString("0.00") + "x";
	}

	public void DisplayAgent(UAI_Agent agent, bool selected){

		for (int i = 0; i < actionElements.Count; i++) {
			Destroy (actionElements[i]);
		}
		for (int i = 0; i < considerationElements.Count; i++) {
			Destroy (considerationElements[i]);
		}
		for (int i = 0; i < propertyElements.Count; i++) {
			Destroy (propertyElements[i]);
		}
		actionElements.Clear ();
		considerationElements.Clear ();
		agentProperties.Clear ();
		propertyElements.Clear ();

		propertyConsiderationsPanel.SetActive (false);
		actionConsiderationsPanel.SetActive (false);
		selectedAction = null;
		selectedProperty = null;
		selectedActionConsideration = null;
		selectedPropertyConsideration = null;

		//deselect other agents
		if (!selected) {

			if (displayedAgent != null) {
				for (int i = 0; i < agentElements.Count; i++) {
					if (agentElements [i].GetComponent<OverlayUIAgentElement> ().GetAgent () == displayedAgent) {
						agentElements [i].GetComponent<OverlayUIAgentElement> ().Select ();
						break;
					}
				}
			}

			displayedAgent = agent;
			displayingAgent = true;

			for (int i = 0; i < agent.GetComponentsInChildren<UAI_Property>().Length; i++) {
				agentProperties.Add (agent.GetComponentsInChildren<UAI_Property> () [i]);
			}

			for (int i = 0; i < agentProperties.Count; i++) {
				GameObject tempProp;
				if (agentProperties [i].modifiable) {
					tempProp = Instantiate (ModifiablePropertyElement, 
				                                 new Vector3 (propertyContent.transform.position.x, 
				            considerationContent.transform.position.y + propertyElements.Count * - 27, 
				            considerationContent.transform.position.z), Quaternion.identity) as GameObject;
				} else {
					tempProp = Instantiate (PropertyElement, 
				                                  new Vector3 (propertyContent.transform.position.x, 
				            considerationContent.transform.position.y + propertyElements.Count * - 27, 
				            considerationContent.transform.position.z), Quaternion.identity) as GameObject;
				}
				tempProp.transform.SetParent (propertyContent.transform);
				tempProp.GetComponent<OverlayUIPropertyElement> ().SetProperty (agentProperties [i]);
				propertyElements.Add (tempProp);
			}
			propertyContent.GetComponent<RectTransform>().sizeDelta = new Vector2(200, propertyElements.Count * 27);


			for (int i = 0; i < agent.linkedActions.Count; i++) {
				//populate UI actions
				GameObject tempAct = Instantiate (actionElement, 
			                                 new Vector3 (actionContent.transform.position.x + 100, 
			            actionContent.transform.position.y + actionElements.Count * - 27, 
			            actionContent.transform.position.z), Quaternion.identity) as GameObject;
				tempAct.transform.SetParent (actionContent.transform);
				tempAct.GetComponent<OverlayUIActionElement> ().SetAction (agent.linkedActions[i].action);
				actionElements.Add (tempAct);
			}
			actionContent.GetComponent<RectTransform>().sizeDelta = new Vector2(200, actionElements.Count * 27);

		} else {
			displayingAgent = false;
			displayedAgent = null;
		}
	}

	public void DisplayConsiderations(UAI_Property property, bool selected){

		for (int i = 0; i < considerationElements.Count; i++) {
			Destroy (considerationElements[i]);
		}
		considerationElements.Clear ();

		if (!selected) {
			if (selectedProperty != null) {
				for (int i = 0; i < propertyElements.Count; i++) {
					UAI_Property tempProperty = propertyElements[i].GetComponent<OverlayUIPropertyElement>().GetProperty ();
					if (selectedProperty == tempProperty){
						propertyElements[i].GetComponent<OverlayUIPropertyElement> ().Select ();
						break;
					}
				}
			}

			for (int i = 0; i < displayedAgent.linkedActions.Count; i++) {
				for (int j = 0; j < displayedAgent.linkedActions[i].action.considerations.Count; j++) {
					if (displayedAgent.linkedActions [i].action.considerations [j].property == property) {
						GameObject tempCon = Instantiate (considerationElement, 
					                                  new Vector3 (considerationContent.transform.position.x, 
					             considerationContent.transform.position.y + considerationElements.Count * - 27, 
					             considerationContent.transform.position.z), Quaternion.identity) as GameObject;
						tempCon.transform.SetParent (considerationContent.transform);
						tempCon.GetComponent<OverlayUIConsiderationElement> ().SetConsideration (
							displayedAgent.linkedActions [i].action.considerations [j], false, displayedAgent.linkedActions[i].action.name);
						considerationElements.Add (tempCon);
					}
				}
			}
			considerationContent.GetComponent<RectTransform>().sizeDelta = new Vector2(200, considerationElements.Count * 27);

			utilityCurve.SetActive (false);
			displayingCurve = false;
			propertyConsiderationsPanel.SetActive (true);
			selectedProperty = property;
		} else {
			utilityCurve.SetActive (false);
			displayingCurve = false;
			propertyConsiderationsPanel.SetActive (false);
			selectedProperty = null;
			selectedPropertyConsideration = null;
		}
	}

	public void DisplayActionConsiderations(UAI_Action action, bool selected){

		for (int i = 0; i < actionConsiderationElements.Count; i++) {
			Destroy (actionConsiderationElements[i]);
		}
		actionConsiderationElements.Clear ();

		if (!selected) {
			if (selectedAction != null){
				for(int i = 0; i < actionElements.Count; i++){
					UAI_Action tempAction = actionElements[i].GetComponent<OverlayUIActionElement>().GetAction();
					if(selectedAction == tempAction){
						actionElements[i].GetComponent<OverlayUIActionElement>().Select ();
						break;
					}
				}
			}

			for (int i = 0; i < action.considerations.Count; i++) {
				GameObject tempCon = Instantiate (considerationElement, 
				                                  new Vector3 (actionConsiderationContent.transform.position.x, 
				             actionConsiderationContent.transform.position.y + actionConsiderationElements.Count * - 27, 
				             actionConsiderationContent.transform.position.z), Quaternion.identity) as GameObject;
				tempCon.transform.SetParent (actionConsiderationContent.transform);
				tempCon.GetComponent<OverlayUIConsiderationElement> ().SetConsideration (action.considerations [i], true, action.name);
				actionConsiderationElements.Add (tempCon);
			}
			actionConsiderationContent.GetComponent<RectTransform>().sizeDelta = new Vector2(200, actionConsiderationElements.Count * 27);

			actionConsiderationCurve.SetActive (false);
			displayingActionCurve = false;
			actionConsiderationsPanel.SetActive (true);
			selectedAction = action;
		} else {
			actionConsiderationCurve.SetActive (false);
			displayingActionCurve = false;
			actionConsiderationsPanel.SetActive (false);
			selectedAction = null;
			selectedActionConsideration = null;
		}
	}

	// Update is called once per frame
	void Update () {

		for (int i = 0; i < propertyElements.Count; i++) {
			propertyElements[i].GetComponent<OverlayUIPropertyElement>().SetPropertyUI();
		}
		for (int i = 0; i < considerationElements.Count; i++) {
			considerationElements[i].GetComponent<OverlayUIConsiderationElement>().SetConsiderationUI();
		}
		for (int i = 0; i < actionConsiderationElements.Count; i++) {
			actionConsiderationElements[i].GetComponent<OverlayUIConsiderationElement>().SetConsiderationUI();
		}
		for (int i = 0; i < actionElements.Count; i++) {
			actionElements[i].GetComponent<OverlayUIActionElement>().SetActionUI();
		}
		if (displayingAgent) {
			currentActionText.text = "Current Action: \n" + displayedAgent.GetTopAction ().name;
			actionTimerText.text = "Time Left: " + displayedAgent.actionTimer.ToString ("0.00");
		}

		for (int i = 0; i < historyElements.Count; i++) {
			Destroy(historyElements[i]);
		}

		historyElements.Clear ();

		if (displayingAgent) {
			for (int i = 0; i < displayedAgent.actionHistory.Count; i++) {
				actionHistoryContent.GetComponent<RectTransform>().sizeDelta = new Vector2(155, historyElements.Count * 15 + 15);
				GameObject tempHistory = Instantiate (historyElement, 
				                                     new Vector3 (actionHistoryContent.transform.position.x, 
				             									 actionHistoryContent.transform.position.y,
	                                                             actionHistoryContent.transform.position.z), Quaternion.identity) as GameObject;
				tempHistory.GetComponent<Text> ().text = displayedAgent.actionHistory [i];
				tempHistory.transform.SetParent (actionHistoryContent.transform);
				historyElements.Add (tempHistory);
			}

			if (displayingCurve) {
				utilityIndicator.transform.localPosition = new Vector3 (0, (displayedConsideration.utilityScore) * 128 - 64, 0);
				propertyIndicator.transform.localPosition = new Vector3 (displayedConsideration.propertyScore * 128 - 64, 0, 0);
			}

			if (displayingActionCurve) {
				actionUtilityIndicator.transform.localPosition = new Vector3 (0, (displayedActionConsideration.utilityScore) * 128 - 64, 0);
				actionPropertyIndicator.transform.localPosition = new Vector3 (displayedActionConsideration.propertyScore * 128 - 64, 0, 0);
			}
		}
	}

	public void DisplayCurve(UAI_Consideration consideration, bool isActionConsideration, bool selected){
		if (!selected) {
			if (isActionConsideration) {
				if (selectedActionConsideration != null){
					for(int i = 0; i < actionConsiderationElements.Count; i++){
						UAI_Consideration tempCon = actionConsiderationElements[i].GetComponent<OverlayUIConsiderationElement>().GetConsideration();
						if(selectedActionConsideration == tempCon){
							actionConsiderationElements[i].GetComponent<OverlayUIConsiderationElement>().Select ();
							break;
						}
					}
				}
				BuildUtilityCurve (consideration, true);
				displayingActionCurve = true;
				displayedActionConsideration = consideration;
				actionConsiderationCurve.SetActive (true);
				selectedActionConsideration = consideration;
			} else {
				if (selectedPropertyConsideration != null){
					for(int i = 0; i < considerationElements.Count; i++){
						UAI_Consideration tempCon = considerationElements[i].GetComponent<OverlayUIConsiderationElement>().GetConsideration();
						if(selectedPropertyConsideration == tempCon){
							considerationElements[i].GetComponent<OverlayUIConsiderationElement>().Select ();
							break;
						}
					}
				}
				BuildUtilityCurve (consideration, false);
				displayingCurve = true;
				displayedConsideration = consideration;
				utilityCurve.SetActive (true);
				selectedPropertyConsideration = consideration;
			}
		} else {
			if (isActionConsideration) {
				actionConsiderationCurve.SetActive (false);
				displayingActionCurve = false;
				selectedActionConsideration = null;
			} else {
				utilityCurve.SetActive (false);
				displayingCurve = false;
				selectedPropertyConsideration = null;
			}
		}
	}

	void BuildUtilityCurve(UAI_Consideration con, bool isActionConsideration){
		Texture2D texture = new Texture2D (128, 128, TextureFormat.RGBA32, false);
			for (int i = 0; i < 128; i++) {
			int y = Mathf.FloorToInt(con.utilityCurve.Evaluate(i/128.0f)*128.0f);
			texture.SetPixel (i, y, Color.black);
		}
		texture.Apply ();

		if (isActionConsideration) {
			Rect rect = actionUtilityCurveRenderer.sprite.rect;
			actionUtilityCurveRenderer.sprite = Sprite.Create (texture, rect, new Vector2 (0.5f, 0.5f));
		} else {
			Rect rect = utilityCurveRenderer.sprite.rect;
			utilityCurveRenderer.sprite = Sprite.Create (texture, rect, new Vector2 (0.5f, 0.5f));
		}
	}
	
	public void ChangeUtilityTime(int function){
		if (function == 0 && !UtilityTime.paused) {
			UtilityTime.paused = true;
			pauseButton.colors = selectedColorBlock;
		} else if (function == 0 && UtilityTime.paused){
			UtilityTime.paused = false;
			pauseButton.colors = normalColorBlock;
		} else if (function == 1 && UtilityTime.speed > 0.25f) {
			UtilityTime.speed -= 0.25f;
		} else if (function == 2) {
			UtilityTime.speed += 0.25f;
		}
		utilitySpeedText.text = "Speed: " + UtilityTime.speed.ToString("0.00") + "x";
	}

	public void OnClickHide(string button){
		if (button == "HideActionsPanel") {
			actionsPanel.SetActive (false);
			showActionsPanel.SetActive (true);
		} else if (button == "HidePropertiesPanel") {
			propertiesPanel.SetActive (false);
			showPropertiesPanel.SetActive (true);
		} else if (button == "HideEventsPanel") {
			eventsPanel.SetActive (false);
			showEventPanel.SetActive (true);
		} else if (button == "HideAgentsPanel") {
			agentsPanel.SetActive (false);
			showAgentsPanel.SetActive (true);
		}
	}

	public void OnClickShow(string button){
		if (button == "ShowActionsPanel") {
			actionsPanel.SetActive (true);
			showActionsPanel.SetActive (false);
		} else if (button == "ShowPropertiesPanel") {
			propertiesPanel.SetActive (true);
			showPropertiesPanel.SetActive (false);
		} else if (button == "ShowEventsPanel") {
			eventsPanel.SetActive (true);
			showEventPanel.SetActive (false);
		} else if (button == "ShowAgentsPanel") {
			showAgentsPanel.SetActive (false);
			agentsPanel.SetActive (true);
		}
	}
}

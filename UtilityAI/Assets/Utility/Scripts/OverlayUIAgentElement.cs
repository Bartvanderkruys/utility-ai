using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayUIAgentElement : MonoBehaviour {

	private Agent agent;
	public Text text;
	private OverlayUI ui;
	private bool selected = false;
	private ColorBlock normalColorBlock, selectedColorBlock;

	// Use this for initialization
	void Start()
	{
		text = GetComponentInChildren<Text> ();
		ui = GetComponentInParent<OverlayUI> ();
		normalColorBlock = GetComponent<Button> ().colors;
		selectedColorBlock = GetComponent<Button> ().colors;
		selectedColorBlock.normalColor = new Color (0.4f, 0.4f, 0.3f, 1.0f);
	}

	public void SetAgent(Agent p_agent){
		agent = p_agent;
		SetAgentUI ();
	}

	public Agent GetAgent(){
		return agent;
	}

	public void Select(){
		if (!selected) {
			ui.DisplayAgent(agent, false);
			selected = true;
			GetComponent<Button>().colors = selectedColorBlock;
		} else {
			ui.DisplayAgent(agent, true);
			selected = false;
			GetComponent<Button>().colors = normalColorBlock;
		}
	}
	
	void SetAgentUI()
	{
		text.text = agent.agentName;
	}
}

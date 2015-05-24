using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayUIAgentElement : MonoBehaviour {

	private Agent agent;
	public Text text;
	private OverlayUI ui;
	private bool selected = false;

	// Use this for initialization
	void Start()
	{
		text = GetComponentInChildren<Text> ();
		ui = GetComponentInParent<OverlayUI> ();
	}

	public void SetAgent(Agent p_agent){
		agent = p_agent;
		SetAgentUI ();
	}

	public void Select(){
		if (!selected) {
			ui.DisplayAgent (agent);
			selected = true;
		} else {
			ui.DeselectedAgent (agent);
			selected = false;
		}
	}
	
	void SetAgentUI()
	{
		text.text = agent.agentName;
	}
}

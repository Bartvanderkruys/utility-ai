using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayUIAgentElement : MonoBehaviour {

	private UAI_Agent agent;
	public Text text;
	private OverlayUI ui;
	private bool selected = false;
	private ColorBlock normalColorBlock, selectedColorBlock, pauseNormalColorBlock, pauseSelectedColorBlock;
	public Button button, pauseButton;

	// Use this for initialization
	void Start()
	{
		text = GetComponentInChildren<Text> ();
		ui = GetComponentInParent<OverlayUI> ();
		normalColorBlock = button.colors;
		selectedColorBlock = button.colors;
		selectedColorBlock.normalColor = new Color (0.4f, 0.4f, 0.3f, 1.0f);
		pauseNormalColorBlock = pauseButton.colors;
		pauseSelectedColorBlock = pauseButton.colors;
		pauseSelectedColorBlock.normalColor = new Color (0.5f, 0.5f, 0.5f, 1.0f);
	}

	public void SetAgent(UAI_Agent p_agent){
		agent = p_agent;
		SetAgentUI ();
	}

	public UAI_Agent GetAgent(){
		return agent;
	}

	public void Select(){
		if (!selected) {
			ui.DisplayAgent(agent, false);
			selected = true;
			agent.characterIndicator.SetActive(true);
			GetComponent<Button>().colors = selectedColorBlock;
		} else {
			ui.DisplayAgent(agent, true);
			selected = false;
			agent.characterIndicator.SetActive(false);
			GetComponent<Button>().colors = normalColorBlock;
		}
	}

	public void PauseAgent(){
		agent.Pause ();
		if (agent.IsPaused()) 
			pauseButton.colors = pauseSelectedColorBlock;
		else 
			pauseButton.colors = pauseNormalColorBlock;
	}
	
	void SetAgentUI()
	{
		text.text = agent.agentName;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayUIActionElement : MonoBehaviour {

	private OverlayUI ui;
	private UAI_Action action;
	public Text text;
	public Text ActionScoreText;
	public Slider slider;
	private ColorBlock normalColorBlock, selectedColorBlock;
	private bool selected = false;

	void Start(){
		ui = GetComponentInParent<OverlayUI> ();
		normalColorBlock = GetComponent<Button> ().colors;
		selectedColorBlock = GetComponent<Button> ().colors;
		selectedColorBlock.normalColor = new Color (0.4f, 0.4f, 0.3f, 1.0f);
	}

	public UAI_Action GetAction(){
		return action;
	}
	
	public void SetAction(UAI_Action p_action){
		action = p_action;
		text.text = action.name;
	}
	
	public void SetActionUI()
	{
		ActionScoreText.text = "A: " + action.GetActionScore ().ToString("0.00");
		slider.value = action.GetActionScore() / 1.0f;
	}

	public void Select(){
		if (!selected) {
			ui.DisplayActionConsiderations (action, false);
			selected = true;
			GetComponent<Button>().colors = selectedColorBlock;
		} else {
			ui.DisplayActionConsiderations (action, true);
			selected = false;
			GetComponent<Button>().colors = normalColorBlock;
		}
	}
}

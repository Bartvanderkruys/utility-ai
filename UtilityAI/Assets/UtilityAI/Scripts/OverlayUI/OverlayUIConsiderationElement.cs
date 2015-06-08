using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayUIConsiderationElement : MonoBehaviour {

	private OverlayUI ui;
	private UAI_Consideration consideration;
	public Text nameText, utilityText;
	public Slider utilitySlider;
	private ColorBlock normalColorBlock, selectedColorBlock;
	private bool isActionConsideration = false, selected = false;

	public void Start(){
		ui = GetComponentInParent<OverlayUI> ();
		normalColorBlock = GetComponent<Button> ().colors;
		selectedColorBlock = GetComponent<Button> ().colors;
		selectedColorBlock.normalColor = new Color (0.4f, 0.4f, 0.3f, 1.0f);
	}

	public void SetConsideration(UAI_Consideration p_consideration, bool isAction, string actionName){
		consideration = p_consideration;
		isActionConsideration = isAction;
		if (isAction)
			nameText.text = consideration.property.name;
		else
			nameText.text = actionName;
	}

	public void SetConsiderationUI()
	{
		float utilityValue = consideration.utilityScore;
		utilityText.text = "U: " + utilityValue.ToString("0.00");
		utilitySlider.value = utilityValue;
	}

	public UAI_Consideration GetConsideration(){
		return consideration;
	}

	public void Select(){
		if (isActionConsideration) {
			if(!selected){
				ui.DisplayCurve (consideration, true, false);
				selected = true;
				GetComponent<Button> ().colors = selectedColorBlock;
			} else {
				ui.DisplayCurve (consideration, true, true);
				selected = false;
				GetComponent<Button>().colors = normalColorBlock;
			}
		} else {
			if(!selected){
				ui.DisplayCurve (consideration, false, false);
				selected = true;
				GetComponent<Button>().colors = selectedColorBlock;
			} else {
				ui.DisplayCurve (consideration, false, true);
				selected = false;
				GetComponent<Button>().colors = normalColorBlock;
			}
		}
	}
}

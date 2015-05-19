using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayUIConsiderationElement : MonoBehaviour {

	private OverlayUI ui;
	private Consideration consideration;
	public Text nameText;
	public Text propertyText;
	public Text utilityText;
	public Slider propertySlider;
	public Slider utilitySlider;

	public void Start(){
		nameText.text = consideration.name;
		ui = GetComponentInParent<OverlayUI> ();
	}

	public void SetConsideration(Consideration p_consideration){
		consideration = p_consideration;
	}

	public void SetConsiderationUI()
	{
		float propertyValue = consideration.propertyScore;
		float utilityValue = consideration.utilityScore;

		propertyText.text = "P: " + propertyValue.ToString("0.00");
		propertySlider.value = propertyValue;
		utilityText.text = "U: " + utilityValue.ToString("0.00");
		utilitySlider.value = utilityValue;
	}

	public void Select(){
		ui.SetUtilityCurve (consideration);
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayUIConsiderationElement : MonoBehaviour {

	private Consideration consideration;
	public Text nameText;
	public Text propertyText;
	public Text utilityText;
	public Slider propertySlider;
	public Slider utilitySlider;

	public void Start(){
		nameText.text = consideration.considerationName;
	}

	public void SetConsideration(Consideration p_consideration){
		consideration = p_consideration;
	}

	public void SetConsiderationUI()
	{
		float propertyValue = consideration.GetValue ();
		float utilityValue = consideration.GetUtilityScore ();

		propertyText.text = "P: " + propertyValue.ToString("0");
		propertySlider.value = propertyValue / 100.0f;
		utilityText.text = "U: " + utilityValue.ToString("0.00");
		utilitySlider.value = utilityValue;
	}
}

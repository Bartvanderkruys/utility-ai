using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayUIPropertyElement : MonoBehaviour {
	
	private OverlayUI ui;
	private UAI_Property property;
	private bool selected = false;
	public Text nameText, propertyText;
	public Slider propertySlider;
	private ColorBlock normalColorBlock, selectedColorBlock;

	void Start(){
		ui = GetComponentInParent<OverlayUI> ();
		normalColorBlock = GetComponent<Button> ().colors;
		selectedColorBlock = GetComponent<Button> ().colors;
		selectedColorBlock.normalColor = new Color (0.4f, 0.4f, 0.3f, 1.0f);
	}

	public void SetProperty(UAI_Property p_property){
		property = p_property;
		nameText.text = property.transform.name;
	}

	public UAI_Property GetProperty(){
		return property;
	}
	
	public void SetPropertyUI()
	{
		float propertyValue = property.normalizedValue;
		propertyText.text = "P: " + propertyValue.ToString("0.00");
		propertySlider.value = propertyValue;
	}

	public void Select(){
		if (!selected) {
			ui.DisplayConsiderations (property, false);
			selected = true;
			GetComponent<Button> ().colors = selectedColorBlock;
		} else {
			ui.DisplayConsiderations (property, true);
			selected = false;
			GetComponent<Button> ().colors = normalColorBlock;
		}
	}

	public void SliderValueChange(){
		if (property is UAI_PropertyBoundedFloat) {
			UAI_PropertyBoundedFloat temp = (UAI_PropertyBoundedFloat)property;
			temp.value = propertySlider.value * temp.maxValue + temp.minValue;
		} else if (property is UAI_PropertyBoundedDouble) {
			UAI_PropertyBoundedDouble temp = (UAI_PropertyBoundedDouble)property;
			temp.value = propertySlider.value * temp.maxValue + temp.minValue;
		} else if (property is UAI_PropertyBoundedInt) {
			UAI_PropertyBoundedInt temp = (UAI_PropertyBoundedInt)property;
			temp.value = Mathf.FloorToInt (propertySlider.value * temp.maxValue + temp.minValue);
		} else if (property is UAI_PropertyBoolean) {
			UAI_PropertyBoolean temp = (UAI_PropertyBoolean)property;
			if(propertySlider.value < 0.5)
				temp.value = false;
			else
				temp.value = true;
		}
	}
}

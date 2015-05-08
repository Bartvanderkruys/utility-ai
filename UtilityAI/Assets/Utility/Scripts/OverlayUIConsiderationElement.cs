using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayUIConsiderationElement : MonoBehaviour {

	private Consideration consideration;
	private Text text;
	private Slider slider;

	void Start()
	{
		text = GetComponentInChildren<Text> ();
		slider = GetComponentInChildren<Slider> ();
	}

	public void SetConsideration(Consideration p_consideration){
		consideration = p_consideration;
	}

	public void SetConsiderationUI()
	{
		text.text = consideration.considerationName + ":   " + consideration.GetValue();
		slider.value = consideration.GetValue () / 100.0f;
	}
}

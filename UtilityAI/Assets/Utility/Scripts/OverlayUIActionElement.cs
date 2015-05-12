using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayUIActionElement : MonoBehaviour {
	
	private Action action;
	private Text text;
	private Slider slider;
	
	void Start()
	{
		text = GetComponentInChildren<Text> ();
		slider = GetComponentInChildren<Slider> ();
	}
	
	public void SetAction(Action p_action){
		action = p_action;
	}
	
	public void SetActionUI()
	{
		text.text = action.actionName + ":   " + action.GetActionScore ().ToString("0.00");
		slider.value = action.GetActionScore() / 1.0f;
	}

	public void Select(){
		Debug.Log ("Click");
	}
}

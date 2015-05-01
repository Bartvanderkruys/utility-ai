using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DemoUI : MonoBehaviour {

	public GameObject character;
	DemoCharacters characterScript;

	public Text simulationSpeedText;

	[Header("Consideration Text")]
	public Text energyText;
	public Text hungerText;
	public Text hygieneText;
	public Text socialText;
	public Text entertainmentText;
	public Text suppliesText;
	public Text currentActionText;

	[Header("Action Text")]
	public Text eatText;
	public Text sleepText;
	public Text showerText;
	public Text getGroceriesText;
	public Text watchMovieText;
	public Text drinkCoffeeText;

	// Use this for initialization
	void Start () {
		characterScript = (DemoCharacters)character.GetComponent(typeof(DemoCharacters));
		simulationSpeedText.text = "Speed: " + characterScript.speed + "x";
	}
	
	// Update is called once per frame
	void Update () {

		//update consideration text and panels
		SetConsiderationUI ("Energy: ", energyText, characterScript.energyConsideration);
		SetConsiderationUI ("Hunger: ", hungerText, characterScript.hungerConsideration);
		SetConsiderationUI ("Hygiene: ", hygieneText, characterScript.hygieneConsideration);
		SetConsiderationUI ("Social: ", socialText, characterScript.socialConsideration);
		SetConsiderationUI ("Entertainment: ", entertainmentText, characterScript.entertainmentConsideration);
		SetConsiderationUI ("Supplies: ", suppliesText, characterScript.suppliesConsideration);

		//update current action
		currentActionText.text = "Current Action: " + characterScript.GetCurrentActionName () 
			+ "\nTime Remaining: " + characterScript.actionTimer;
	}


	void SetConsiderationUI(string p_textString, Text p_textObject, Consideration p_consideration)
	{
		p_textObject.text = p_textString + p_consideration.GetValue();
		Slider slider = p_textObject.GetComponentInChildren<Slider> ();
		slider.value = p_consideration.GetValue () / 100.0f;
	}

	public void SetActionScores()
	{
		//update actionscores, text and panels
		SetActionScoreUI ("Eat: ", eatText, characterScript.eatAction);
		SetActionScoreUI ("Sleep: ", sleepText, characterScript.sleepAction);
		SetActionScoreUI ("Shower: ", showerText, characterScript.showerAction);
		SetActionScoreUI ("Get Groceries: ", getGroceriesText, characterScript.getGroceriesAction);
		SetActionScoreUI ("Watch Movie : ", watchMovieText, characterScript.watchMovieAction);
		SetActionScoreUI ("Drink Coffee : ", drinkCoffeeText, characterScript.drinkCoffeeAction);
	}

	void SetActionScoreUI(string p_textString, Text p_textObject, Action p_action)
	{
		p_textObject.text = p_textString + p_action.GetActionScore();
		Slider slider = p_textObject.GetComponentInChildren<Slider> ();
		slider.value = p_action.GetActionScore();
	}

	//unity doesn't pass an ID for which slider has changed. Therefore I need to 
	//make a function for each slider
	public void OnEnergySliderChanged(float val)
	{
		characterScript.energy = val * 100.0f;
	}
	public void OnHygieneSliderChanged(float val)
	{
		characterScript.hygiene = val * 100.0f;
	}
	public void OnHungerSliderChanged(float val)
	{
		characterScript.hunger = val * 100.0f;
	}
	public void OnSocialSliderChanged(float val)
	{
		characterScript.socialInteraction = val * 100.0f;
	}
	public void OnEntertainmentSliderChanged(float val)
	{
		characterScript.entertainment = val * 100.0f;
	}
	public void OnSuppliesSliderChanged(float val)
	{
		characterScript.supplies = val * 100.0f;
	}

	//onclick events for speed buttons
	public void OnSpeedUp()
	{
		characterScript.speed += 0.25f;
		characterScript.movementSpeed += 0.25f;
		simulationSpeedText.text = "Speed: " + characterScript.speed + "x";
	}

	public void OnSpeedDown()
	{
		if (characterScript.speed != 0.25f) {
			characterScript.speed -= 0.25f;
			characterScript.movementSpeed -= 0.25f;
			simulationSpeedText.text = "Speed: " + characterScript.speed + "x";
		}
	}
}

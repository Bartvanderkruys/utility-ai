using UnityEngine;
using System.Collections.Generic;

public class DemoCharacters : MonoBehaviour {

	//demo values
	public float speed;
	public float movementSpeed;
	public GameObject UIObject; 
	private DemoUI demoUI;
	[HideInInspector]
	public float actionTimer = 0.0f; 
	private int evaluationCounter = 0;
	private Vector3 destination;

	[Header("Character Properties")]
	public float energy;
	public float hygiene;
	public float hunger;
	public float socialInteraction;
	public float entertainment;
	public float work;
	public float supplies;

	[Header("Considerations")]
	public Consideration energyConsideration;
	public Consideration hungerConsideration;
	public Consideration hygieneConsideration;
	public Consideration socialConsideration;
	public Consideration entertainmentConsideration;
	public Consideration workConsideration;
	public Consideration suppliesConsideration;

	private List<Action> actions;
	private Action currentAction;
	[Header("Actions")]
	public Action sleepAction;
	public Action showerAction;
	public Action eatAction;
	public Action watchMovieAction;
	public Action workAction;
	public Action getGroceriesAction;
	public Action drinkCoffeeAction;

	[Header("Waypoints")]
	public GameObject homeWaypoint;
	public GameObject officeWaypoint;
	public GameObject restaurantWaypoint;
	public GameObject cinemaWaypoint;
	public GameObject groceryStoreWaypoint;
	

	// Use this for initialization
	void Start () {
		//link UI to this script
		demoUI = (DemoUI)UIObject.GetComponent(typeof(DemoUI));

		actions = new List<Action>();

		//add function delegate to action
		//add considerations to action
		//add actions to list
		sleepAction.handle = Sleep;
		sleepAction.considerations.Add (energyConsideration);
		actions.Add (sleepAction);

		showerAction.handle = Shower;
		showerAction.considerations.Add (hygieneConsideration);
		actions.Add (showerAction);

		eatAction.handle = Eat;
		eatAction.considerations.Add (hungerConsideration);
		actions.Add (eatAction);

		watchMovieAction.handle = WatchMovie;
		watchMovieAction.considerations.Add (entertainmentConsideration);
		watchMovieAction.considerations.Add (socialConsideration);
		actions.Add (watchMovieAction);

		getGroceriesAction.handle = GetGroceries;
		getGroceriesAction.considerations.Add (suppliesConsideration);
		actions.Add (getGroceriesAction);

		drinkCoffeeAction.handle = DrinkCoffee;
		drinkCoffeeAction.considerations.Add (energyConsideration);
		drinkCoffeeAction.considerations.Add (hungerConsideration);
		actions.Add (drinkCoffeeAction);
	}
	
	// Update is called once per frame
	void Update () {

		//keep characterproperties in range
		energy = KeepPropertyInRange (energy, 0, 100);
		hunger = KeepPropertyInRange (hunger, 0, 100);
		hygiene = KeepPropertyInRange (hygiene, 0, 100);
		socialInteraction = KeepPropertyInRange (socialInteraction, 0, 100);
		entertainment = KeepPropertyInRange (entertainment, 0, 100);
		supplies = KeepPropertyInRange (supplies, 0, 100);

		//Passing by reference does not seem to work,
		//therefore, I update the values every frame for now.
		energyConsideration.SetValue(ref energy);
		hungerConsideration.SetValue(ref hunger);
		hygieneConsideration.SetValue(ref hygiene);
		socialConsideration.SetValue(ref socialInteraction);
		entertainmentConsideration.SetValue(ref entertainment);
		workConsideration.SetValue(ref work);
		suppliesConsideration.SetValue(ref supplies);


		if (actionTimer > 0.0f) {
			actionTimer -= speed * Time.deltaTime;
			currentAction.handle();
		} else {
			Evaluate ();
		}
	}

	void Evaluate()
	{
		Action topAction = drinkCoffeeAction;
		float topActionScore = 0.0f;
		Debug.Log (++evaluationCounter);

		//Debug.Log ("Evaluating");
		//for each action
		for (int i = 0; i < actions.Count; i++) {
			float actionScore = 0.0f;
			//evaluate appropriate considerations
			for (int j = 0; j < actions[i].considerations.Count; j++){
				//normalize value
				float x = actions[i].considerations[j].GetValue() / (actions[i].considerations[j].maximum_value - actions[i].considerations[j].minimum_value);
				float utilityScore = 1 - actions[i].considerations[j].utilityCurve.Evaluate(x);
				actionScore += utilityScore;
			}
			//determine average
			actionScore = actionScore / actions[i].considerations.Count;
			actions[i].SetActionScore(actionScore);
			//if the score is the highest, set the action as the next action
			Debug.Log ("actionScore of " + actions[i].actionName + ": " + actionScore);
			if(actionScore > topActionScore)
			{
				topAction = actions[i];
				topActionScore = actionScore;
			}			
		}
		//update UI with new ActionScores
		demoUI.SetActionScores ();
		Debug.Log ("Topaction: " + topAction.actionName);
		currentAction = topAction;
		actionTimer = topAction.time;
	}

	void MoveToTarget()
	{
		// move to action position
		while (this.transform.position != destination) {
			float step = movementSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destination, step);

			energy -= 2.0f * speed * Time.deltaTime;
			hygiene -= 1.5f * speed * Time.deltaTime;
			hunger -= 1.0f * speed * Time.deltaTime;
			entertainment -= 2.0f * speed * Time.deltaTime;
			socialInteraction -= 1.0f * speed * Time.deltaTime;
		}
	}

	void Sleep()
	{
		// 6 - 9 hours at home
		energy += 15.0f * speed * Time.deltaTime;
		hygiene -= 5.0f * speed * Time.deltaTime;
		hunger -= 5.0f * speed * Time.deltaTime;
	}

	void Shower()
	{
		hygiene += 80.0f * speed * Time.deltaTime;
		hunger -= 5.0f * speed * Time.deltaTime;
	}

	void Eat()
	{
		energy += 2.0f * speed * Time.deltaTime;
		hygiene -= 3.0f * speed * Time.deltaTime;
		hunger += 40.0f * speed * Time.deltaTime;
		supplies -= 0.5f * speed * Time.deltaTime;
	}

	void WatchMovie()
	{
		energy -= 1.0f * speed * Time.deltaTime;
		hygiene -= 0.5f * speed * Time.deltaTime;
		hunger -= 1.0f * speed * Time.deltaTime;
		entertainment += 20.0f * speed * Time.deltaTime;
		socialInteraction += 15.0f * speed * Time.deltaTime;
	}

	void Work()
	{
		energy -= 2.0f * speed * Time.deltaTime;
		hygiene -= 2.0f * speed * Time.deltaTime;
		hunger -= 2.0f * speed * Time.deltaTime;
		socialInteraction += 2.0f * speed * Time.deltaTime;
	}

	void GetGroceries()
	{
		energy -= 2.0f * speed * Time.deltaTime;
		hygiene -= 1.5f * speed * Time.deltaTime;
		socialInteraction += 1.0f * speed * Time.deltaTime;
		supplies += 50.0f * speed * Time.deltaTime;
	}

	void DrinkCoffee()
	{
		energy += 2.0f * speed * Time.deltaTime;
		hygiene -= 1.5f * speed * Time.deltaTime;
		hunger += 2.0f * speed * Time.deltaTime;
		supplies -= 0.5f * speed * Time.deltaTime;
	}

	public string GetCurrentActionName()
	{
		return currentAction.actionName;
	}

	float KeepPropertyInRange(float property, float min, float max)
	{
		if (property < min)
			return min;
		else if (property > max)
			return max;
		else
			return property;
	}
}

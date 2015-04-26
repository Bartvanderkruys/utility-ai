using UnityEngine;
using System.Collections.Generic;

public class DemoCharacters : MonoBehaviour {

	//demo values
	public float speed;
	public float movementSpeed;
	private float actionTimer = 0.0f; 
	private Vector3 destination;

	[Header("Character Parameters")]
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

	[Header("Actions")]
	private List<Action> actions;
	private Action currentAction;
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

		actions = new List<Action>();

		//add function delegate to action
		//add considerations to action
		//add actions to list

		sleepAction.actionName = "Sleeping";
		sleepAction.handle = Sleep;
		sleepAction.considerations.Add (energyConsideration);
		actions.Add (sleepAction);

		showerAction.actionName = "Showering";
		showerAction.handle = Shower;
		showerAction.considerations.Add (hygieneConsideration);
		actions.Add (showerAction);

		eatAction.actionName = "Eating";
		eatAction.handle = Eat;
		eatAction.considerations.Add (energyConsideration);
		eatAction.considerations.Add (hungerConsideration);
		actions.Add (eatAction);

		watchMovieAction.actionName = "Watching Movie";
		watchMovieAction.handle = WatchMovie;
		watchMovieAction.considerations.Add (entertainmentConsideration);
		watchMovieAction.considerations.Add (socialConsideration);
		actions.Add (watchMovieAction);

		getGroceriesAction.actionName = "Getting Groceries";
		getGroceriesAction.handle = GetGroceries;
		getGroceriesAction.considerations.Add (suppliesConsideration);
		actions.Add (getGroceriesAction);

		drinkCoffeeAction.actionName = "Drinking Coffee";
		drinkCoffeeAction.handle = DrinkCoffee;
		drinkCoffeeAction.considerations.Add (energyConsideration);
		drinkCoffeeAction.considerations.Add (hungerConsideration);
		actions.Add (drinkCoffeeAction);
	}
	
	// Update is called once per frame
	void Update () {
	
		//update considerations, need to find a way to reference them to the values
		energyConsideration.value = energy;
		hungerConsideration.value = hunger;
		hygieneConsideration.value = hygiene;
		socialConsideration.value = socialInteraction;
		entertainmentConsideration.value = entertainment;
		workConsideration.value = work;
		suppliesConsideration.value = supplies;


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

		//Debug.Log ("Evaluating");
		//for each action
		for (int i = 0; i < actions.Count; i++) {
			float actionScore = 0.0f;
			//evaluate appropriate considerations
			for (int j = 0; j < actions[i].considerations.Count; j++){
				//normalize value
				float x = actions[i].considerations[j].value / (actions[i].considerations[j].maximum_value - actions[i].considerations[j].minimum_value);
				float utilityScore = actions[i].considerations[j].utilityCurve.Evaluate(x);
				actionScore += utilityScore;

			}
			//determine average
			actionScore = actionScore / actions[i].considerations.Count;
			//if the score is the highest, set the action as the next action
			if(actionScore > topActionScore)
			{
				topAction = actions[i];
				topActionScore = actionScore;
			}			
		}
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
		energy += 10.0f * speed * Time.deltaTime;
		hygiene -= 0.5f * speed * Time.deltaTime;
		hunger += 1.0f * speed * Time.deltaTime;
	}

	void Shower()
	{
		hygiene += 20.0f * speed * Time.deltaTime;
		hunger += 1.0f * speed * Time.deltaTime;
	}

	void Eat()
	{
		energy += 2.0f * speed * Time.deltaTime;
		hygiene -= 1.5f * speed * Time.deltaTime;
		hunger -= 10.0f * speed * Time.deltaTime;
		supplies -= 0.5f * speed * Time.deltaTime;
	}

	void WatchMovie()
	{
		energy -= 1.0f * speed * Time.deltaTime;
		hygiene -= 0.5f * speed * Time.deltaTime;
		hunger -= 1.0f * speed * Time.deltaTime;
		socialInteraction += 1.0f * speed * Time.deltaTime;
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
}

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

	//agent
	Agent agent;

	[Header("Character Properties")]
	public float energy;
	public float hygiene;
	public float hunger;
	public float socialInteraction;
	public float entertainment;
	public float supplies;
	
	[Header("Waypoints")]
	public GameObject homeWaypoint;
	public GameObject officeWaypoint;
	public GameObject restaurantWaypoint;
	public GameObject cinemaWaypoint;
	public GameObject groceryStoreWaypoint;
	
	void Start () {
		//link UI to this script
		agent = GetComponent<Agent> ();
		demoUI = (DemoUI)UIObject.GetComponent(typeof(DemoUI));

		//add function delegate to action
		agent.SetVoidActionDelegate("Sleep", Sleep);
		agent.SetVoidActionDelegate("Shower", Shower);
		agent.SetVoidActionDelegate("Eat", Eat);
		agent.SetVoidActionDelegate("Watch Movie", WatchMovie);
		agent.SetVoidActionDelegate("Get Groceries", GetGroceries);
		agent.SetVoidActionDelegate("Drink Coffee", DrinkCoffee);
	}
	
	// Update is called once per frame
	void Update () {

		//keep characterproperties in range
		energy = KeepPropertyInRange (energy, 0.0f, 100.0f);
		hunger = KeepPropertyInRange (hunger, 0.0f, 100.0f);
		hygiene = KeepPropertyInRange (hygiene, 0.0f, 100.0f);
		socialInteraction = KeepPropertyInRange (socialInteraction, 0.0f, 100.0f);
		entertainment = KeepPropertyInRange (entertainment, 0.0f, 100.0f);
		supplies = KeepPropertyInRange (supplies, 0.0f, 100.0f);

		//I cannot get pointers and references to work in C#,
		//therefore, I update the values every frame for now.
		agent.SetAgentConsideration("Energy", energy);
		agent.SetAgentConsideration("Hunger", hunger);
		agent.SetAgentConsideration("Hygiene", hygiene);
		agent.SetAgentConsideration("Social", socialInteraction);
		agent.SetAgentConsideration("Entertainment", entertainment);
		agent.SetAgentConsideration("Supplies", supplies);

		if (actionTimer > 0.0f) {
			actionTimer -= speed * Time.deltaTime;
			agent.GetTopAction().handle();
		} else {
			agent.Evaluate ();
			actionTimer = agent.GetTopAction().time;
			demoUI.SetActionScores();
		}
	}

//	void MoveToTarget()
//	{
//		// move to action position
//		while (this.transform.position != destination) {
//			float step = movementSpeed * Time.deltaTime;
//			transform.position = Vector3.MoveTowards (transform.position, destination, step);
//
//			energy -= 2.0f * speed * Time.deltaTime;
//			hygiene -= 1.5f * speed * Time.deltaTime;
//			hunger -= 1.0f * speed * Time.deltaTime;
//			entertainment -= 2.0f * speed * Time.deltaTime;
//			socialInteraction -= 1.0f * speed * Time.deltaTime;
//		}
//	}

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

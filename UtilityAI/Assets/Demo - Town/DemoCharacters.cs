using UnityEngine;
using System.Collections.Generic;

public class DemoCharacters : MonoBehaviour {

	//demo values
	public float speed;
	public float movementSpeed;
	[HideInInspector]
	private Vector3 destination;
	private bool atDestination = false;

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

	public Property property1;
	public Property property2;

	void Start () {
		//link UI to this script
		agent = GetComponent<Agent> ();

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

		agent.UpdateAI ();

	}

	void FixedUpdate(){

	}

	void DetermineTarget()
	{
		if (agent.GetTopAction().handle == Sleep) {
			destination = homeWaypoint.transform.position;
		} else if (agent.GetTopAction().handle == Shower) {
			destination = homeWaypoint.transform.position;
		} else if (agent.GetTopAction().handle == Eat) {
			destination = homeWaypoint.transform.position;
		} else if (agent.GetTopAction().handle == WatchMovie) {
			destination = cinemaWaypoint.transform.position;
		} else if (agent.GetTopAction().handle == Work) {
			destination = officeWaypoint.transform.position;
		} else if (agent.GetTopAction().handle == GetGroceries) {
			destination = groceryStoreWaypoint.transform.position;
		} else if (agent.GetTopAction().handle == DrinkCoffee) {
			destination = homeWaypoint.transform.position;
		}
	}

	void MoveToTarget()
	{
		// move to action position
		DetermineTarget();
		float step = movementSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, destination, step);

		energy -= 2.0f * speed * Time.deltaTime;
		hygiene -= 1.5f * speed * Time.deltaTime;
		hunger -= 1.0f * speed * Time.deltaTime;
		entertainment -= 2.0f * speed * Time.deltaTime;
		socialInteraction -= 5.0f * speed * Time.deltaTime;

		if (transform.position == destination)
			atDestination = true;
	}

	void Sleep()
	{
		if (atDestination) {
			energy += 10.0f * speed * Time.deltaTime;
			hygiene -= 5.0f * speed * Time.deltaTime;
			hunger -= 5.0f * speed * Time.deltaTime;
			supplies -= 2.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void Shower()
	{
		if(atDestination){
		hygiene += 80.0f * speed * Time.deltaTime;
		hunger -= 5.0f * speed * Time.deltaTime;
		entertainment -= 2.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void Eat()
	{
		if(atDestination){
		energy += 2.0f * speed * Time.deltaTime;
		hygiene -= 3.0f * speed * Time.deltaTime;
		hunger += 40.0f * speed * Time.deltaTime;
		supplies -= 10.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void WatchMovie()
	{
		if(atDestination){
		energy -= 2.0f * speed * Time.deltaTime;
		hygiene -= 2.0f * speed * Time.deltaTime;
		hunger -= 4.0f * speed * Time.deltaTime;
		entertainment += 8.0f * speed * Time.deltaTime;
		socialInteraction += 15.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void Work()
	{
		if(atDestination){
		energy -= 2.0f * speed * Time.deltaTime;
		hygiene -= 2.0f * speed * Time.deltaTime;
		hunger -= 2.0f * speed * Time.deltaTime;
		entertainment -= 5.0f * speed * Time.deltaTime;
		socialInteraction += 2.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void GetGroceries()
	{
		if(atDestination){
		energy -= 3.0f * speed * Time.deltaTime;
		hygiene -= 2.5f * speed * Time.deltaTime;
		entertainment -= 5.0f * speed * Time.deltaTime;
		socialInteraction += 2.0f * speed * Time.deltaTime;
		supplies += 30.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void DrinkCoffee()
	{
		if(atDestination){
		energy += 1.0f * speed * Time.deltaTime;
		hygiene -= 1.5f * speed * Time.deltaTime;
		hunger += 2.0f * speed * Time.deltaTime;
		supplies -= 0.5f * speed * Time.deltaTime;
		socialInteraction -= 2.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
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

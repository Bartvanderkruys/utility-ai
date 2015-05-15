using UnityEngine;
using System.Collections.Generic;

public class DemoCharacters : MonoBehaviour {

	//demo values
	public float speed;
	public float movementSpeed;
	[HideInInspector]
	private Vector3 destination;
	static bool atDestination = false;

	//agent
	Agent agent;

	[Header("Character Properties")]
	public List<Property> CharacterProperties;

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
		agent = GetComponent<Agent> ();

		//add function delegate to action
//		agent.SetVoidActionDelegate("Sleep", Sleep);
//		agent.SetVoidActionDelegate("Shower", Shower);
//		agent.SetVoidActionDelegate("Eat", Eat);
//		agent.SetVoidActionDelegate("Watch Movie", WatchMovie);
//		agent.SetVoidActionDelegate("Get Groceries", GetGroceries);
//		agent.SetVoidActionDelegate("Drink Coffee", DrinkCoffee);
	}
	
	// Update is called once per frame
	void Update () {
		agent.UpdateAI ();
	}

	void DetermineTarget()
	{
		if (agent.GetTopAction().handle == Sleep) {
			destination = homeWaypoint.transform.position;
		} else if (agent.GetTopAction().handle == Shower) {
			destination = homeWaypoint.transform.position;
		} else if (agent.GetTopAction().handle == Eat) {
			destination = restaurantWaypoint.transform.position;
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
	}

	void Sleep()
	{
		DetermineTarget();
		if (transform.position == destination) {
			atDestination = true;
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
		DetermineTarget();
		if (transform.position == destination) {
		atDestination = true;
		hygiene += 80.0f * speed * Time.deltaTime;
		hunger -= 5.0f * speed * Time.deltaTime;
		entertainment -= 2.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void Eat()
	{
		DetermineTarget();
		if (transform.position == destination) {
		atDestination = true;
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
		DetermineTarget();
		if (transform.position == destination) {
		atDestination = true;	
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
		DetermineTarget();
		if (transform.position == destination) {
		atDestination = true;
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
		DetermineTarget();
		if (transform.position == destination) {
		atDestination = true;
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
		DetermineTarget();
		if (transform.position == destination) {
		atDestination = true;
		energy += 1.0f * speed * Time.deltaTime;
		hygiene -= 1.5f * speed * Time.deltaTime;
		hunger += 2.0f * speed * Time.deltaTime;
		supplies -= 0.5f * speed * Time.deltaTime;
		socialInteraction -= 2.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}
}

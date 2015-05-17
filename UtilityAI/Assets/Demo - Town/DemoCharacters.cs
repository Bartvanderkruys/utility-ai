using UnityEngine;
using System.Collections.Generic;

public class DemoCharacters : MonoBehaviour {

	//demo values
	public float speed;
	public float movementSpeed;
	[HideInInspector]
	private Vector3 destination, preDestination;
	private bool isOutside = false; 
	private bool isOutsideDestination = false;
	private bool atDestination = false;

	//agent
	Agent agent;
	public PropertyBoundedFloat energy, hunger, hygiene, social, entertainment, supplies;
	
	[Header("Waypoints")]
	public GameObject homeWaypointIn;
	public GameObject homeWaypointOut;
	public GameObject officeWaypointIn;
	public GameObject officeWaypointOut;
	public GameObject restaurantWaypointIn;
	public GameObject restaurantWaypointOut;
	public GameObject cinemaWaypointIn;
	public GameObject cinemaWaypointOut;
	public GameObject groceryStoreWaypointIn;
	public GameObject groceryStoreWaypointOut;

	void Start () {
		agent = GetComponent<Agent> ();
		preDestination = transform.position;

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
		agent.UpdateAI ();
	}

	void DetermineTarget()
	{
		if (agent.GetTopAction().handle == Sleep) {
			destination = homeWaypointIn.transform.position;
			preDestination = homeWaypointOut.transform.position;
		} else if (agent.GetTopAction().handle == Shower) {
			destination = homeWaypointIn.transform.position;
			preDestination = homeWaypointOut.transform.position;
		} else if (agent.GetTopAction().handle == Eat) {
			destination = restaurantWaypointIn.transform.position;
			preDestination = restaurantWaypointOut.transform.position;
		} else if (agent.GetTopAction().handle == WatchMovie) {
			destination = cinemaWaypointIn.transform.position;
			preDestination = cinemaWaypointOut.transform.position;
		} else if (agent.GetTopAction().handle == Work) {
			destination = officeWaypointIn.transform.position;
			preDestination = officeWaypointOut.transform.position;
		} else if (agent.GetTopAction().handle == GetGroceries) {
			destination = groceryStoreWaypointIn.transform.position;
			preDestination = groceryStoreWaypointOut.transform.position;
		} else if (agent.GetTopAction().handle == DrinkCoffee) {
			destination = homeWaypointIn.transform.position;
			preDestination = homeWaypointIn.transform.position;
		}
	}

	void MoveToTarget()
	{
		float step = movementSpeed * Time.deltaTime;

		if (!isOutside) {
			transform.position = Vector3.MoveTowards (transform.position, preDestination, step);
			if (transform.position == preDestination) {
				DetermineTarget ();
				isOutside = true;
			}
		} else 	if (isOutside && !isOutsideDestination) {
			transform.position = Vector3.MoveTowards (transform.position, preDestination, step);
			if (transform.position == preDestination) {
				isOutsideDestination = true;
			}
		} else if (isOutside && isOutsideDestination) {
			transform.position = Vector3.MoveTowards (transform.position, destination, step);
			if(transform.position == destination){
				agent.StartTimer();
				atDestination = true;
			}
		}
	}

	void Sleep()
	{
		if (agent.newAction) {
			isOutside = false;
			isOutsideDestination = false;
			atDestination = false;
			agent.newAction = false;
		}
		if(atDestination){	
			energy.value += 10.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void Shower()
	{
		if (agent.newAction) {
			isOutside = false;
			isOutsideDestination = false;
			atDestination = false;
			agent.newAction = false;
		}
		if(atDestination){	
			hygiene.value += 80.0f * speed * Time.deltaTime;
			hunger.value -= 5.0f * speed * Time.deltaTime;
			entertainment.value -= 2.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void Eat()
	{
		if (agent.newAction) {
			isOutside = false;
			isOutsideDestination = false;
			atDestination = false;
			agent.newAction = false;
		}
		if(atDestination){	
			energy.value += 2.0f * speed * Time.deltaTime;
			hunger.value += 60.0f * speed * Time.deltaTime;
			supplies.value -= 10.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void WatchMovie()
	{
		if (agent.newAction) {
			isOutside = false;
			isOutsideDestination = false;
			atDestination = false;
			agent.newAction = false;
		}
		if(atDestination){	
		entertainment.value += 15.0f * speed * Time.deltaTime;
		social.value += 15.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void Work()
	{
		if (agent.newAction) {
			isOutside = false;
			isOutsideDestination = false;
			atDestination = false;
			agent.newAction = false;
		}
		if(atDestination){	
		social.value += 2.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void GetGroceries()
	{
		if (agent.newAction) {
			isOutside = false;
			isOutsideDestination = false;
			atDestination = false;
			agent.newAction = false;
		}
		if(atDestination){	
		supplies.value += 60.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}

	void DrinkCoffee()
	{
		if (agent.newAction) {
			isOutside = false;
			isOutsideDestination = false;
			atDestination = false;
			agent.newAction = false;
		}
		if(atDestination){	
		energy.value += 1.0f * speed * Time.deltaTime;
		hunger.value += 2.0f * speed * Time.deltaTime;
		} else {
			MoveToTarget ();
		}
	}
}

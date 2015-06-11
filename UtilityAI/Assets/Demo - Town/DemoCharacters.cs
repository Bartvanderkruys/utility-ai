using UnityEngine;
using System.Collections.Generic;

public class DemoCharacters : MonoBehaviour {

	//demo values
	public float movementSpeed;
	[HideInInspector]
	private Vector3 destination, preDestination;
	private bool isOutside = false; 
	private bool isOutsideDestination = false;
	private bool atDestination = false;

	//agent
	UAI_Agent agent;
	public UAI_PropertyBoundedFloat energy, hunger, hygiene, entertainment, supplies, money;
	
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
		agent = GetComponent<UAI_Agent> ();
		preDestination = transform.position;

		//add function delegate to action
		agent.SetVoidActionDelegate("Sleep", Sleep);
		agent.SetVoidActionDelegate("Shower", Shower);
		agent.SetVoidActionDelegate("Eat", Eat);
		agent.SetVoidActionDelegate("Watch Movie", WatchMovie);
		agent.SetVoidActionDelegate("Get Groceries", GetGroceries);
		agent.SetVoidActionDelegate("Drink Coffee", DrinkCoffee);
		agent.SetVoidActionDelegate("Work", Work);
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
			preDestination = homeWaypointOut.transform.position;
		}
	}

	void MoveToTarget()
	{
		float step = movementSpeed * UtilityTime.time;

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

	void ResetPositions(){
		if (agent.newAction) {
			isOutside = false;
			isOutsideDestination = false;
			atDestination = false;
			agent.newAction = false;
		}
	}

	void Sleep()
	{
		ResetPositions ();

		if(atDestination){	
			energy.value += 20.0f * UtilityTime.time;
		} else {
			MoveToTarget ();
		}
	}

	void Shower()
	{
		ResetPositions ();

		if(atDestination){	
			hygiene.value += 80.0f * UtilityTime.time;
		} else {
			MoveToTarget ();
		}
	}

	void Eat()
	{
		ResetPositions ();

		if(atDestination){	
			energy.value += 2.0f * UtilityTime.time;
			hunger.value -= 60.0f * UtilityTime.time;
			money.value -= 10.0f * UtilityTime.time;
		} else {
			MoveToTarget ();
		}
	}

	void WatchMovie()
	{
		ResetPositions ();

		if(atDestination){	
			entertainment.value += 15.0f * UtilityTime.time;
			money.value -= 5.0f * UtilityTime.time;
		} else {
			MoveToTarget ();
		}
	}

	void Work()
	{
		ResetPositions ();

		if(atDestination){
			money.value += 50.0f * UtilityTime.time;
		} else {
			MoveToTarget ();
		}
	}

	void GetGroceries()
	{
		ResetPositions ();

		if(atDestination){	
			supplies.value += 60.0f * UtilityTime.time;
			money.value -= 10.0f * UtilityTime.time;
		} else {
			MoveToTarget ();
		}
	}

	void DrinkCoffee()
	{
		ResetPositions ();

		if(atDestination){	
			energy.value += 1.0f * UtilityTime.time;
			hunger.value += 2.0f * UtilityTime.time;
		} else {
			MoveToTarget ();
		}
	}
}

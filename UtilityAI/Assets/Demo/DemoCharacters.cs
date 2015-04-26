using UnityEngine;
using System.Collections;

public class DemoCharacters : MonoBehaviour {

	public int speed;
	private bool inAnAction = false;
	private float actionTimer = 0.0f;
	private Vector3 destination; 

	//Considerations
	public float energy;
	public float hygiene;
	public float hunger;
	public float socialInteraction;
	public float bladder;
	public float entertainment;
	public float work;
	public float supplies;

	//waypoints
	public GameObject homeWaypoint;
	public GameObject officeWaypoint;
	public GameObject restaurantWaypoint;
	public GameObject cinemaWaypoint;
	public GameObject groceryStoreWaypoint;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//evaluate
	}

	void Sleep()
	{
		// 6 - 9 hours at home
		actionTimer = 6.0f;
		destination = homeWaypoint.transform.position;
		
		// move to action position
		while (this.transform.position != destination) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destination, step);
		}

		//perform action
		while (actionTimer < 0.0f) {
			actionTimer -= speed * Time.deltaTime;
			//change character parameters
			energy += 10.0f * Time.deltaTime;
		}
	}

	void Shower()
	{
		actionTimer = 0.5f;
		destination = homeWaypoint.transform.position;
		
		// move to action position
		while (this.transform.position != destination) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destination, step);
		}
		
		//perform action
		while (actionTimer < 0.0f) {
			actionTimer -= speed * Time.deltaTime;
			//change character parameters
			energy += 10.0f * Time.deltaTime;
		}
	}

	void Eat()
	{
		actionTimer = 1.0f;
		destination = homeWaypoint.transform.position;
		
		// move to action position
		while (this.transform.position != destination) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destination, step);
		}
		
		//perform action
		while (actionTimer < 0.0f) {
			actionTimer -= speed * Time.deltaTime;
			//change character parameters
			energy += 10.0f * Time.deltaTime;
		}
	}

	void VisitToilet()
	{
		// 2 - 20 minutes everywhere
		actionTimer = 6.0f;
		destination = homeWaypoint.transform.position;
		
		// move to action position
		while (this.transform.position != destination) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destination, step);
		}
		
		//perform action
		while (actionTimer < 0.0f) {
			actionTimer -= speed * Time.deltaTime;
			//change character parameters
			energy += 10.0f * Time.deltaTime;
		}
	}

	void WatchMovie()
	{
		// 2 hours at cinema or at home
		actionTimer = 6.0f;
		destination = homeWaypoint.transform.position;
		
		// move to action position
		while (this.transform.position != destination) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destination, step);
		}
		
		//perform action
		while (actionTimer < 0.0f) {
			actionTimer -= speed * Time.deltaTime;
			//change character parameters
			energy += 10.0f * Time.deltaTime;
		}
	}

	void Work()
	{
		// 4 hours at office
		actionTimer = 6.0f;
		destination = officeWaypoint.transform.position;
		
		// move to action position
		while (this.transform.position != destination) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destination, step);
		}
		
		//perform action
		while (actionTimer < 0.0f) {
			actionTimer -= speed * Time.deltaTime;
			//change character parameters
			energy += 10.0f * Time.deltaTime;
		}
	}

	void GetGroceries()
	{
		// 30 minutes to one hour at grocerie store
		actionTimer = 6.0f;
		destination = homeWaypoint.transform.position;
		
		// move to action position
		while (this.transform.position != destination) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destination, step);
		}
		
		//perform action
		while (actionTimer < 0.0f) {
			actionTimer -= speed * Time.deltaTime;
			//change character parameters
			energy += 10.0f * Time.deltaTime;
		}
	}

	void DrinkCoffee()
	{
		// 5 to 10 minutes at home or office
		actionTimer = 6.0f;
		destination = homeWaypoint.transform.position;
		
		// move to action position
		while (this.transform.position != destination) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, destination, step);
		}
		
		//perform action
		while (actionTimer < 0.0f) {
			actionTimer -= speed * Time.deltaTime;
			//change character parameters
			energy += 10.0f * Time.deltaTime;
		}
	}
}

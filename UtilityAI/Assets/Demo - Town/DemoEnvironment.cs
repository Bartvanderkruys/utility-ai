using UnityEngine;
using System.Collections;

public class DemoEnvironment : MonoBehaviour {

	public GameObject character;

	//time of day
	private enum weekDays{
		monday,
		tuesday,
		wednesday,
		thursday,
		friday,
		saturday,
		sunday
	};

	private float timeOfDay = 6.0f;

	//buildings
	public GameObject home;
	public GameObject office;
	public GameObject restaurant;
	public GameObject cinema;
	public GameObject groceryStore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//update time of day
	}
}

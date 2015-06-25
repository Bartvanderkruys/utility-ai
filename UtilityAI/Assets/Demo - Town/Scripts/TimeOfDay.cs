using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeOfDay : MonoBehaviour {

	public UAI_PropertyBoundedFloat timeProperty;
	public UAI_PropertyBoolean weekendProperty;

	public Text timeOfDayText;
	public Text dayText;

	enum week{
		SUNDAY = 0,
		MONDAY = 1,
		TUESDAY = 2,
		WEDNESDAY = 3,
		THURSDAY = 4,
		FRIDAY = 5,
		SATURDAY = 6
	};

	float timeOfDay = 12.0f;
	int day = (int)week.WEDNESDAY;
	
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}

		//update time
		timeOfDay += UtilityTime.time;
		//check if a day has passed
		if (timeOfDay >= 24.0f) {
			timeOfDay = 0;
			UpdateDay ();
		}

		timeProperty.value = timeOfDay;

		int displayedTime = Mathf.FloorToInt(timeOfDay);
		if (displayedTime > 12) {
			timeOfDayText.text = (displayedTime - 12).ToString("0") + " pm";
		} else {
			timeOfDayText.text = displayedTime.ToString("0") + " am";
		}
	}

	void UpdateDay(){
		switch (day) {
		case (int)week.SUNDAY:
			day = (int)week.MONDAY;
			dayText.text = "Monday";
			weekendProperty.value = false;
			break;
		case (int)week.MONDAY:
			day = (int)week.TUESDAY;
			dayText.text = "Tuesday";
			break;
		case (int)week.TUESDAY:
			day = (int)week.WEDNESDAY;
			dayText.text = "Wednesday";
			break;
		case (int)week.WEDNESDAY:
			day = (int)week.THURSDAY;
			dayText.text = "Thursday";
			break;
		case (int)week.THURSDAY:
			day = (int)week.FRIDAY;
			dayText.text = "Friday";
			break;
		case (int)week.FRIDAY:
			day = (int)week.SATURDAY;
			dayText.text = "Saturday";
			weekendProperty.value = true;
			break;
		case (int)week.SATURDAY:
			day = (int)week.SUNDAY;
			dayText.text = "Sunday";
			break;
		}
	}
}

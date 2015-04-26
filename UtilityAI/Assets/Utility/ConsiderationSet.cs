using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Utility AI/Consideration Manager")]
public class ConsiderationSet : MonoBehaviour {

	public string setName;
	public GameObject location;

	public List<Consideration> Considerations = new List<Consideration>();

	// Returns a Utility Score for a Value
	//float Evaluate (float inputValue) {
	//	return utilityCurve.Evaluate(inputValue / maximum_value - minimum_value);
	//}
}

using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Utility AI/Consideration Manager")]
public class ConsiderationSet : MonoBehaviour {

	public string setName;
	public GameObject location;

	public List<Consideration> Considerations = new List<Consideration>();
}

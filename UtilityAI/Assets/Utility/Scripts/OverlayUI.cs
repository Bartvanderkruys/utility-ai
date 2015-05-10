using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class OverlayUI : MonoBehaviour {

	public Text currentActionText;

	private Agent[] agents;
	public GameObject considerationElement;
	public GameObject actionElement;
	public GameObject considerationContent;
	public GameObject actionContent;

	public Image utilityCurveRenderer;

	private List<GameObject> actionElements = new List<GameObject>();
	private List<GameObject> considerationElements = new List<GameObject>();

	// Use this for initialization
	void Start () {
		agents = FindObjectsOfType (typeof(Agent)) as Agent[];
		BuildUtilityCurve (agents[0].agentConsiderations[0]);
		//create all consideration and action elements
		for (int i = 0; i < agents.Length; i++) {
			for(int j = 0; j < agents[i].agentConsiderations.Count; j++) {
				GameObject temp = Instantiate(considerationElement, 
				                              new Vector3(considerationContent.transform.position.x, 
				            considerationContent.transform.position.y + considerationElements.Count * -28, 
				            considerationContent.transform.position.z), 	Quaternion.identity) as GameObject;
				temp.transform.SetParent(considerationContent.transform);
				temp.GetComponent<OverlayUIConsiderationElement>().SetConsideration(agents[i].agentConsiderations[j]);
				considerationElements.Add (temp);
			}
			for(int j = 0; j < agents[i].actions.Count; j++) {
				GameObject temp = Instantiate(actionElement, 
				                              new Vector3(actionContent.transform.position.x, 
				            actionContent.transform.position.y + actionElements.Count * -25, 
				            actionContent.transform.position.z), 	Quaternion.identity) as GameObject;
				temp.transform.SetParent(actionContent.transform);
				temp.GetComponent<OverlayUIActionElement>().SetAction(agents[i].actions[j]);
				actionElements.Add (temp);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < considerationElements.Count; i++) {
			considerationElements[i].GetComponent<OverlayUIConsiderationElement>().SetConsiderationUI();
		}
		for (int i = 0; i < actionElements.Count; i++) {
			actionElements[i].GetComponent<OverlayUIActionElement>().SetActionUI();
		}
		currentActionText.text = "Current Action: " + agents [0].GetTopAction ().actionName;
	}

	void BuildUtilityCurve(Consideration con){
		Texture2D texture = new Texture2D (128, 128, TextureFormat.RGBA32, false);
			for (int i = 0; i < 128; i++) {
			int y = Mathf.FloorToInt(con.utilityCurve.Evaluate(i/128.0f)*128.0f);
			texture.SetPixel (i, y, Color.black);
		}
		texture.Apply ();
		Rect rect = utilityCurveRenderer.sprite.rect;
		utilityCurveRenderer.sprite = Sprite.Create (texture, rect, new Vector2 (0.5f, 0.5f));
	}
}

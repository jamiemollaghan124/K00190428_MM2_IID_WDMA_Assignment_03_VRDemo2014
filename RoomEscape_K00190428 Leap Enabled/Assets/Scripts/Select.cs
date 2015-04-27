using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {
	public RaycastHit hit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject.Find ("SafeButton1").GetComponent<Renderer>().material.color = new Color (0.58f, 0.749f, 0.7647f, 1);
		GameObject.Find ("SafeButton2").GetComponent<Renderer>().material.color = new Color (0.86f, 0.517f, 0.517f, 1);
		GameObject.Find ("SafeButton3").GetComponent<Renderer>().material.color = new Color (0.874f, 0.93333f, 0.3176f, 1);
		GameObject.Find ("SafeButton4").GetComponent<Renderer>().material.color = new Color (0.874f, 0.6117f, 0.219f, 1);

		Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));

		if(Physics.Raycast(ray, out hit, 3)){
			if(hit.collider.gameObject.GetComponent<Interact>() != null){
			hit.collider.gameObject.GetComponent<Interact>().OnLookEnter();
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class whiteBoard : MonoBehaviour {

	bool active = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Interact.screw1 && Interact.screw2 && Interact.screw3 && Interact.screw4 && active) {
			GetComponent<Animation>().Play();
			active = false;
				}
	}
}

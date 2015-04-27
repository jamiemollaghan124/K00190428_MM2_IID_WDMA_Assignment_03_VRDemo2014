using UnityEngine;
using System.Collections;

public class correctSafeCombo : MonoBehaviour {

	public AudioClip safeCorrect;
	private AudioSource source; 

	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!safeHandCollide.arrayActive) {
			source.PlayOneShot(safeCorrect, 1);
		}
	}
}

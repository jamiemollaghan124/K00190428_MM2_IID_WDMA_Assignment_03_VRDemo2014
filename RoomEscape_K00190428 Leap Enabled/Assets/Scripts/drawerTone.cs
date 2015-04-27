using UnityEngine;
using System.Collections;

public class drawerTone : MonoBehaviour {

	public AudioClip drawerSlide;
	public AudioSource source;

	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void playDrawerSound(){
		source.PlayOneShot (drawerSlide, 1);
	}
}

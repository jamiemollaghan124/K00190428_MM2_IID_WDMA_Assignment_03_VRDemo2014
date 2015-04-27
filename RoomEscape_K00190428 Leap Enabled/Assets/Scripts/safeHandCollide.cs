using UnityEngine;
using System.Collections;

public class safeHandCollide : MonoBehaviour {

	public static ArrayList codeGuess = new ArrayList();
	public static ArrayList codeBreak = new ArrayList();
	public static bool arrayActive = true;
	public static int guessLength = 0;
	public static int buttonTimerCooldown = 0;
	public static bool button1active = true;
	public static bool button2active = true;
	public static bool button3active = true;
	public static bool button4active = true;
	public static bool key = false;
	public static bool finalDoorKey = false;
	public static bool screwdriver = false;
	public AudioClip safeBeep;
	private AudioSource source;
	private AudioSource source2;
	public bool wrongSafeComboGUIShown = false;



	void Start () {
		codeBreak.Add (2);
		codeBreak.Add (4);
		codeBreak.Add (3);
		codeBreak.Add (1);
		AudioSource[] sounds = GameObject.Find("SafeButton1").GetComponents<AudioSource> ();
		source = sounds [0];
		source2 = sounds[1];
	}
	
	// Update is called once per frame
	void Update () {

	}
	private bool IsHand(Collider collider)
	{
		if (collider.transform.parent && collider.transform.parent.parent && collider.transform.parent.parent.GetComponent<HandModel>())
			return true;
		else
			return false;
	}
	void OnTriggerEnter(Collider collider){
		if (IsHand (collider) && this.name.Equals ("key")) {
			gameObject.active = false;
			key = true;
		}
		if (IsHand (collider) && this.name.Equals ("doorKey")) {
			gameObject.active = false;
			finalDoorKey = true;
		}

	}
	void OnGUI(){
		if(wrongSafeComboGUIShown) 
		{
			GUI.Box(new Rect((Screen.width/2)-200,10,400,30) , "I guess that wasn't right");
		}
	}

	void showWrongComboBox ()
	{
		wrongSafeComboGUIShown = true;
		CancelInvoke("HideBox");
		Invoke ("HideBox", 2.0F);
	}
	
	void HideBox ()
	{
		wrongSafeComboGUIShown = false;
	}

	void OnTriggerExit(Collider collider){
		if (IsHand (collider) && this.name.Equals ("SafeButton1") && arrayActive && button1active) {
			if(codeGuess.Count != 4){
				source.Play();
				codeGuess.Add(1);
				guessLength++;
				Debug.Log("Added");
				button1active = false;
				this.GetComponent<Animation>().Play();
				if(!codeGuess[guessLength-1].Equals(codeBreak[guessLength-1])){
					codeGuess.Clear();
					guessLength = 0;
					source2.Play();
					showWrongComboBox ();
					Debug.Log("Incorrect");
					button1active = true;
					button2active = true;
					button3active = true;
					button4active = true;
				}
				if(guessLength == 4){
					if(codeGuess[guessLength-1].Equals(codeBreak[guessLength-1])){
						arrayActive = false;
						GameObject.Find("SafeDoorHinge").GetComponent<Animation>().Play("SafeDoorOpen");
						Interact.safeOpenClose = 1;
					}
					else {
						button1active = true;
						button2active = true;
						button3active = true;
						button4active = true;
						showWrongComboBox ();
					}
				}
			}
		}
		if (IsHand (collider) && this.name.Equals ("SafeButton2") && arrayActive && button2active) {
			if(codeGuess.Count != 4){
				codeGuess.Add(2);
				source.Play();
				guessLength++;
				Debug.Log("Added");
				button2active = false;
				this.GetComponent<Animation>().Play();
				if(!codeGuess[guessLength-1].Equals(codeBreak[guessLength-1])){
					codeGuess.Clear();
					guessLength = 0;
					source2.Play();
					showWrongComboBox ();
					Debug.Log("Incorrect");
					button1active = true;
					button2active = true;
					button3active = true;
					button4active = true;
				}
				if(guessLength == 4){
					if(codeGuess[guessLength-1].Equals(codeBreak[guessLength-1])){
						arrayActive = false;
						GameObject.Find("SafeDoorHinge").GetComponent<Animation>().Play("SafeDoorOpen");
						Interact.safeOpenClose = 1;
					}
					else {
						button1active = true;
						button2active = true;
						button3active = true;
						button4active = true;
						showWrongComboBox ();
					}
				}
			}
		}
		if (IsHand (collider) && this.name.Equals ("SafeButton3") && arrayActive && button3active) {
			if(codeGuess.Count != 4){
				codeGuess.Add(3);
				guessLength++;
				source.Play();
				Debug.Log("Added");
				button3active = false;
				this.GetComponent<Animation>().Play();
				if(!codeGuess[guessLength-1].Equals(codeBreak[guessLength-1])){
					codeGuess.Clear();
					guessLength = 0;
					source2.Play();
					showWrongComboBox ();
					Debug.Log("Incorrect");
					button1active = true;
					button2active = true;
					button3active = true;
					button4active = true;
				}
				if(guessLength == 4){
					if(codeGuess[guessLength-1].Equals(codeBreak[guessLength-1])){
						arrayActive = false;
						GameObject.Find("SafeDoorHinge").GetComponent<Animation>().Play("SafeDoorOpen");
						Interact.safeOpenClose = 1;
					}
					else {
						button1active = true;
						button2active = true;
						button3active = true;
						button4active = true;
						showWrongComboBox ();
					}
				}
			}
		}
		if (IsHand (collider) && this.name.Equals ("SafeButton4") && arrayActive && button4active) {
			if(codeGuess.Count != 4){
				codeGuess.Add(4);
				guessLength++;
				source.Play();
				Debug.Log("Added");
				button4active = false;
				this.GetComponent<Animation>().Play();
				if(!codeGuess[guessLength-1].Equals(codeBreak[guessLength-1])){
					codeGuess.Clear();
					guessLength = 0;
					source2.Play();
					showWrongComboBox ();
					Debug.Log("Incorrect");
					button1active = true;
					button2active = true;
					button3active = true;
					button4active = true;
				}
				if(guessLength == 4){
					if(codeGuess[guessLength-1].Equals(codeBreak[guessLength-1])){
						arrayActive = false;
						GameObject.Find("SafeDoorHinge").GetComponent<Animation>().Play("SafeDoorOpen");
						Interact.safeOpenClose = 1;
					}
					else {
						button1active = true;
						button2active = true;
						button3active = true;
						button4active = true;
						showWrongComboBox ();
					}
				}
			}
		}
		}
	}


using UnityEngine;
using System.Collections;
using Leap;

public class Interact: MonoBehaviour {

	bool active = false;
	int topdrawerInOut = 0;
	int bottomdrawerInOut = 0;
	int WDLeftOpenClose = 0;
	int WDRightOpenClose = 0;
	public static int safeOpenClose = 0;
	int toolboxOpenClose = 0;
	public static bool screw1 = false;
	public static bool screw2 = false;
	public static bool screw3 = false;
	public static bool screw4 = false;
	public static ArrayList codeGuess = new ArrayList();
	public static ArrayList codeBreak = new ArrayList();
	public static int guessLength = 0;
	Controller controller;
	Vector swipeDirection;
	public AudioClip drawerSlide;
	public AudioSource source;
	public AudioClip safeBeep;
	private AudioSource source1;
	private AudioSource source2;
	public bool wrongSafeComboGUIShown = false;
	public static bool pickedUpScrewdriverGUIShown = false;
	public static bool pickedUpWDKey = false;
	public static bool win = false;


	void Start () {
		codeBreak.Add (2);
		codeBreak.Add (4);
		codeBreak.Add (3);
		codeBreak.Add (1);
		source = GetComponent<AudioSource> ();
		controller = new Controller ();
		controller.EnableGesture (Gesture.GestureType.TYPESWIPE);
		controller.EnableGesture (Gesture.GestureType.TYPECIRCLE);
		controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		controller.Config.SetFloat ("Gesture.Circle.MinRadius", 20.0f);
		controller.Config.SetFloat ("Gesture.Circle.MinArc", 2f);
		controller.Config.SetFloat ("Gesture.Swipe.MinLength", 100.0f);
		controller.Config.SetFloat ("Gesture.Swipe.MinVelocity", 400.0f);
		controller.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", 30.0f);
		controller.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", .5f);
		controller.Config.SetFloat("Gesture.ScreenTap.MinDistance", 1.0f);
		controller.Config.Save ();
		AudioSource[] sounds = GameObject.Find("SafeButton1").GetComponents<AudioSource> ();
		source1 = sounds [0];
		source2 = sounds[1];
	}

	void OnGUI(){
		if (pickedUpScrewdriverGUIShown) {
			GUI.Box(new Rect((UnityEngine.Screen.width/2)-200,10,400,30) , "Found a screwdriver!");
		}
		if(wrongSafeComboGUIShown) 
		{
			GUI.Box(new Rect((UnityEngine.Screen.width/2)-200,10,400,30) , "I guess that wasn't right");
		}
		if (pickedUpWDKey) {
			GUI.Box(new Rect((UnityEngine.Screen.width/2)-200,10,400,30) , "Found a key!");
		}
		if (win) {
			GUI.Box(new Rect((UnityEngine.Screen.width/2)-200,10,400,30) , "Escaped!");
		}
	}
	
	void HideBox ()
	{
		wrongSafeComboGUIShown = false;
		pickedUpScrewdriverGUIShown = false;
		pickedUpWDKey = false;
	}

	void showWrongComboBox ()
	{
		wrongSafeComboGUIShown = true;
		CancelInvoke("HideBox");
		Invoke ("HideBox", 2.0F);
	}
	void showPickedUpScrewdriver ()
	{
		pickedUpScrewdriverGUIShown = true;
		CancelInvoke("HideBox");
		Invoke ("HideBox", 2.0F);
	}
	void showPickedUpWDKey(){
		pickedUpWDKey = true;
		CancelInvoke("HideBox");
		Invoke ("HideBox", 2.0F);
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void OnLookEnter(){
		if (this.name.Equals ("DoorHandle") && safeHandCollide.finalDoorKey) {
			Frame frame = controller.Frame ();
			GestureList gestures = frame.Gestures ();
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures [i];
				if (gesture.Type == Gesture.GestureType.TYPESWIPE && safeHandCollide.finalDoorKey) {
					win = true;
					}
			}
		}
		if (this.name.Equals ("DoorHandle") && safeHandCollide.finalDoorKey && Input.GetKeyUp("b")) {
			win = true;
		}

		if (this.name.Equals ("screwdriver")) {
			Frame frame = controller.Frame ();
			GestureList gestures = frame.Gestures ();
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures[i];
				if(gesture.Type == Gesture.GestureType.TYPE_SWIPE){
					SwipeGesture Swipe = new SwipeGesture(gesture);
					swipeDirection = Swipe.Direction;
				if(swipeDirection.z > 0){
				showPickedUpScrewdriver();
				gameObject.active = false;
				safeHandCollide.screwdriver = true;
					}
				}
			}
		}
		if (this.name.Equals ("screwdriver") && Input.GetKeyUp("b")) {
			showPickedUpScrewdriver();
			gameObject.active = false;
			safeHandCollide.screwdriver = true;
		}

		if (this.name.Equals ("CoDTopDrawerFront")) {
			Frame frame = controller.Frame ();
			GestureList gestures = frame.Gestures ();
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures[i];
				if(gesture.Type == Gesture.GestureType.TYPE_SWIPE){
					SwipeGesture Swipe = new SwipeGesture(gesture);
					swipeDirection = Swipe.Direction;
					if(swipeDirection.z < 0 && topdrawerInOut == 1){
						GameObject.Find("Drawer").GetComponent<Animation>().Play("DrawerClose");
						topdrawerInOut = 0;
						source.PlayOneShot (drawerSlide, 1);
					}else if(swipeDirection.z > 0 && topdrawerInOut == 0){
						GameObject.Find("Drawer").GetComponent<Animation>().Play("DrawerOpen");
						topdrawerInOut = 1;
						source.PlayOneShot (drawerSlide, 1);
					}
				}
			}
				}
		if (this.name.Equals ("CoDTopDrawerFront")) {
			if(Input.GetKeyUp("b") && topdrawerInOut == 1){
				GameObject.Find("Drawer").GetComponent<Animation>().Play("DrawerClose");
				topdrawerInOut = 0;
				source.PlayOneShot (drawerSlide, 1);
			}else if(Input.GetKeyUp("b") && topdrawerInOut == 0){
				GameObject.Find("Drawer").GetComponent<Animation>().Play("DrawerOpen");
				topdrawerInOut = 1;
				source.PlayOneShot (drawerSlide, 1);
			}
		}

		if (this.name.Equals ("CoDBottomDrawerFront")) {
			Frame frame = controller.Frame ();
			GestureList gestures = frame.Gestures ();
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures[i];
				if(gesture.Type == Gesture.GestureType.TYPE_SWIPE){
					SwipeGesture Swipe = new SwipeGesture(gesture);
					swipeDirection = Swipe.Direction;
					if(swipeDirection.z < 0 && bottomdrawerInOut == 1){
						GameObject.Find("Drawer1").GetComponent<Animation>().Play("BtmDrawerClose");
						bottomdrawerInOut = 0;
						source.PlayOneShot (drawerSlide, 1);
					}else if(swipeDirection.z > 0 && bottomdrawerInOut == 0){
						GameObject.Find("Drawer1").GetComponent<Animation>().Play("BtmDrawerOpen");
						bottomdrawerInOut = 1;
						source.PlayOneShot (drawerSlide, 1);
					}
				}
			}
		}
		if (this.name.Equals ("CoDBottomDrawerFront")) {
			if (Input.GetKeyUp ("b") && bottomdrawerInOut == 1) {
				GameObject.Find("Drawer1").GetComponent<Animation>().Play("BtmDrawerClose");
				bottomdrawerInOut = 0;
				source.PlayOneShot (drawerSlide, 1);
			}
			else if (Input.GetKeyUp ("b") && bottomdrawerInOut == 0) {
				GameObject.Find("Drawer1").GetComponent<Animation>().Play("BtmDrawerOpen");
				bottomdrawerInOut = 1;
				source.PlayOneShot (drawerSlide, 1);
			}
		}

		if (this.name.Equals ("WDLeftDoor") && safeHandCollide.key) {
			if (Input.GetKeyUp ("b") && WDLeftOpenClose == 1){
				GameObject.Find("Hinge").GetComponent<Animation>().Play("WardrobeDoorClose");
				WDLeftOpenClose = 0;
				source.PlayOneShot (drawerSlide, 1);
			}
			else if (Input.GetKeyUp ("b") && WDLeftOpenClose == 0){
				GameObject.Find("Hinge").GetComponent<Animation>().Play("WardrobeDoor");
				WDLeftOpenClose = 1;
				source.PlayOneShot (drawerSlide, 1);
			}
		}
		if (this.name.Equals ("WDRightDoor")  && safeHandCollide.key) {
			if (Input.GetKeyUp ("b") && WDRightOpenClose == 1){
				GameObject.Find("RightHinge").GetComponent<Animation>().Play("WardrobeDoorRightClose");
				WDRightOpenClose = 0;
				source.PlayOneShot (drawerSlide, 1);
			}
			else if (Input.GetKeyUp ("b") && WDRightOpenClose == 0){
				GameObject.Find("RightHinge").GetComponent<Animation>().Play("WardrobeDoorRight");
				WDRightOpenClose = 1;
				source.PlayOneShot (drawerSlide, 1);
			}
		}

		if (this.name.Equals ("WDLeftDoor") && safeHandCollide.key) {
			Frame frame = controller.Frame ();
			GestureList gestures = frame.Gestures ();
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures[i];
				if(gesture.Type == Gesture.GestureType.TYPE_SWIPE){
					SwipeGesture Swipe = new SwipeGesture(gesture);
					swipeDirection = Swipe.Direction;
					if(swipeDirection.x > 0 && WDLeftOpenClose == 1){
						GameObject.Find("Hinge").GetComponent<Animation>().Play("WardrobeDoorClose");
						WDLeftOpenClose = 0;
						source.PlayOneShot (drawerSlide, 1);
					}else if(swipeDirection.x < 0  && WDLeftOpenClose == 0){
						GameObject.Find("Hinge").GetComponent<Animation>().Play("WardrobeDoor");
						WDLeftOpenClose = 1;
						source.PlayOneShot (drawerSlide, 1);
					}
				}
			}
				}
		if (this.name.Equals ("WDRightDoor")  && safeHandCollide.key) {
			Frame frame = controller.Frame ();
			GestureList gestures = frame.Gestures ();
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures[i];
				if(gesture.Type == Gesture.GestureType.TYPE_SWIPE){
					SwipeGesture Swipe = new SwipeGesture(gesture);
					swipeDirection = Swipe.Direction;
					if(swipeDirection.x < 0  && WDRightOpenClose == 1){
						GameObject.Find("RightHinge").GetComponent<Animation>().Play("WardrobeDoorRightClose");
						WDRightOpenClose = 0;
						source.PlayOneShot (drawerSlide, 1);
					}else if(swipeDirection.x > 0  && WDRightOpenClose == 0){
						GameObject.Find("RightHinge").GetComponent<Animation>().Play("WardrobeDoorRight");
						WDRightOpenClose = 1;
						source.PlayOneShot (drawerSlide, 1);
					}
				}
			}
		}
		if (this.name.Equals ("SafeDoor")) {
			Frame frame = controller.Frame ();
			GestureList gestures = frame.Gestures ();
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures[i];
				if(gesture.Type == Gesture.GestureType.TYPE_SWIPE){
					SwipeGesture Swipe = new SwipeGesture(gesture);
					swipeDirection = Swipe.Direction;
					if(swipeDirection.x > 0 && safeOpenClose == 1){
						GameObject.Find("SafeDoorHinge").GetComponent<Animation>().Play("SafeDoorClose");
						Interact.safeOpenClose = 0;
					}else if(swipeDirection.x < 0  && safeOpenClose == 0 && !safeHandCollide.arrayActive){
						GameObject.Find("SafeDoorHinge").GetComponent<Animation>().Play("SafeDoorOpen");
						Interact.safeOpenClose = 1;
					}
				}
			}
				}
		if (this.name.Equals ("SafeDoor")) {
			if (Input.GetKeyUp ("b") && safeOpenClose == 1) {
				GameObject.Find ("SafeDoorHinge").GetComponent<Animation> ().Play ("SafeDoorClose");
				Interact.safeOpenClose = 0;
			} else if (Input.GetKeyUp ("b") && safeOpenClose == 0 && safeHandCollide.arrayActive != true) {
				GameObject.Find ("SafeDoorHinge").GetComponent<Animation> ().Play ("SafeDoorOpen");
				Interact.safeOpenClose = 1;
			}
		}
			if (this.name.Equals ("toolboxTop")) {
				Frame frame = controller.Frame ();
				GestureList gestures = frame.Gestures ();
				for (int i = 0; i < gestures.Count; i++) {
					Gesture gesture = gestures [i];
					if (gesture.Type == Gesture.GestureType.TYPE_SWIPE) {
						SwipeGesture Swipe = new SwipeGesture (gesture);
						swipeDirection = Swipe.Direction;
						if (swipeDirection.z < 0 && toolboxOpenClose == 0) {
							GameObject.Find ("ToolBoxHinge").GetComponent<Animation> ().Play ("ToolboxOpening");
							toolboxOpenClose = 1;
						}
					}
				}
			}
		if(this.name.Equals("toolboxTop")){
			if (Input.GetKeyUp ("b") && toolboxOpenClose == 0){
				GameObject.Find ("ToolBoxHinge").GetComponent<Animation> ().Play ("ToolboxOpening");
				toolboxOpenClose = 1;
			}
		}


		if(this.name.Equals("ScrewBR")){
			Frame frame = controller.Frame ();
			GestureList gestures = frame.Gestures ();
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures[i];
				if(gesture.Type == Gesture.GestureType.TYPECIRCLE && safeHandCollide.screwdriver && !screw1){
					GetComponent<Animation>().Play();
					screw1 = true;
				}
			}
				}

		if(this.name.Equals("screwBL")){
			Frame frame = controller.Frame ();
			GestureList gestures = frame.Gestures ();
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures[i];
				if(gesture.Type == Gesture.GestureType.TYPECIRCLE && safeHandCollide.screwdriver && !screw2){
					GetComponent<Animation>().Play();
					screw2 = true;
				}
			}
		}
		if(this.name.Equals("screwTR")){
			Frame frame = controller.Frame ();
			GestureList gestures = frame.Gestures ();
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures[i];
				if(gesture.Type == Gesture.GestureType.TYPECIRCLE && safeHandCollide.screwdriver && !screw3){
					GetComponent<Animation>().Play();
					screw3 = true;
				}
			}
		}
		if(this.name.Equals("screwTL")){
			Frame frame = controller.Frame ();
			GestureList gestures = frame.Gestures ();
			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures[i];
				if(gesture.Type == Gesture.GestureType.TYPECIRCLE && safeHandCollide.screwdriver && !screw4){
					GetComponent<Animation>().Play();
					screw4 = true;
				}
			}
		}
		if(this.name.Equals("ScrewBR") && safeHandCollide.screwdriver && Input.GetKeyUp ("b") && !screw1){
			GetComponent<Animation>().Play();
			screw1 = true;
		}
		
		if(this.name.Equals("screwBL") && Input.GetKeyUp ("b") && safeHandCollide.screwdriver && !screw2){
			GetComponent<Animation>().Play();
			screw2 = true;
		}
		if(this.name.Equals("screwTR") && Input.GetKeyUp ("b") && safeHandCollide.screwdriver && !screw3){
			GetComponent<Animation>().Play();
			screw3 = true;
		}
		if(this.name.Equals("screwTL") && Input.GetKeyUp ("b") && safeHandCollide.screwdriver && !screw4){
			GetComponent<Animation>().Play();
			screw4 = true;
		}

		if (this.name.Equals ("key") && Input.GetKeyUp ("b")) {
			gameObject.active = false;
			safeHandCollide.key = true;
			showPickedUpWDKey();
		}
		if (this.name.Equals ("doorKey") && Input.GetKeyUp ("b")) {
			gameObject.active = false;
			safeHandCollide.finalDoorKey = true;
		}




		if (this.name.Equals ("SafeButton1") && safeHandCollide.arrayActive) {
			
			if(Input.GetKeyUp ("b") && safeHandCollide.codeGuess.Count != 4){
				source.Play();
				safeHandCollide.codeGuess.Add(1);
				safeHandCollide.guessLength++;
				Debug.Log("Added");
				safeHandCollide.button1active = false;
				this.GetComponent<Animation>().Play();
				if(!safeHandCollide.codeGuess[safeHandCollide.guessLength-1].Equals(safeHandCollide.codeBreak[safeHandCollide.guessLength-1]) && safeHandCollide.guessLength <= 3){
					safeHandCollide.codeGuess.Clear();
					safeHandCollide.guessLength = 0;
					source2.Play();
					Debug.Log("Incorrect");
					showWrongComboBox ();
					safeHandCollide.button1active = true;
					safeHandCollide.button2active = true;
					safeHandCollide.button3active = true;
					safeHandCollide.button4active = true;
				}
				if(Input.GetKeyUp ("b") && safeHandCollide.guessLength == 4){
					if(safeHandCollide.codeGuess[safeHandCollide.guessLength-1].Equals(safeHandCollide.codeBreak[safeHandCollide.guessLength-1])){
						safeHandCollide.arrayActive = false;
						GameObject.Find("SafeDoorHinge").GetComponent<Animation>().Play("SafeDoorOpen");
						Interact.safeOpenClose = 1;
					}
					else {
						safeHandCollide.button1active = true;
						safeHandCollide.button2active = true;
						safeHandCollide.button3active = true;
						safeHandCollide.button4active = true;
						showWrongComboBox ();
					}
				}
			}
			
			
		}
		if (this.name.Equals ("SafeButton2") && safeHandCollide.arrayActive) {
			if(Input.GetKeyUp ("b")&& safeHandCollide.codeGuess.Count != 4){
				safeHandCollide.codeGuess.Add(2);
				source.Play();
				safeHandCollide.guessLength++;
				Debug.Log("Added");
				safeHandCollide.button2active = false;
				this.GetComponent<Animation>().Play();
				if(!safeHandCollide.codeGuess[safeHandCollide.guessLength-1].Equals(safeHandCollide.codeBreak[safeHandCollide.guessLength-1]) && safeHandCollide.guessLength <= 3){
					safeHandCollide.codeGuess.Clear();
					safeHandCollide.guessLength = 0;
					source2.Play();
					Debug.Log("Incorrect");
					showWrongComboBox ();
					safeHandCollide.button1active = true;
					safeHandCollide.button2active = true;
					safeHandCollide.button3active = true;
					safeHandCollide.button4active = true;
				}
				if(Input.GetKeyUp ("b") && safeHandCollide.guessLength == 4){
					if(safeHandCollide.codeGuess[safeHandCollide.guessLength-1].Equals(safeHandCollide.codeBreak[safeHandCollide.guessLength-1])){
						safeHandCollide.arrayActive = false;
						GameObject.Find("SafeDoorHinge").GetComponent<Animation>().Play("SafeDoorOpen");
						Interact.safeOpenClose = 1;
					}
					else {
						safeHandCollide.button1active = true;
						safeHandCollide.button2active = true;
						safeHandCollide.button3active = true;
						safeHandCollide.button4active = true;
						showWrongComboBox ();
					}
				}
			}
			
		}
		if (this.name.Equals ("SafeButton3") && safeHandCollide.arrayActive) {
			if(Input.GetKeyUp ("b")&& safeHandCollide.codeGuess.Count != 4){
				safeHandCollide.codeGuess.Add(3);
				safeHandCollide.guessLength++;
				source.Play();
				Debug.Log("Added");
				safeHandCollide.button3active = false;
				this.GetComponent<Animation>().Play();
				if(!safeHandCollide.codeGuess[safeHandCollide.guessLength-1].Equals(safeHandCollide.codeBreak[safeHandCollide.guessLength-1]) && safeHandCollide.guessLength <= 3){
					safeHandCollide.codeGuess.Clear();
					safeHandCollide.guessLength = 0;
					source2.Play();
					Debug.Log("Incorrect");
					safeHandCollide.button1active = true;
					safeHandCollide.button2active = true;
					safeHandCollide.button3active = true;
					safeHandCollide.button4active = true;
					showWrongComboBox ();
				}
				if(Input.GetKeyUp ("b") && safeHandCollide.guessLength == 4){
					if(safeHandCollide.codeGuess[safeHandCollide.guessLength-1].Equals(safeHandCollide.codeBreak[safeHandCollide.guessLength-1])){
						safeHandCollide.arrayActive = false;
						GameObject.Find("SafeDoorHinge").GetComponent<Animation>().Play("SafeDoorOpen");
						Interact.safeOpenClose = 1;
					}
					else {
						safeHandCollide.button1active = true;
						safeHandCollide.button2active = true;
						safeHandCollide.button3active = true;
						safeHandCollide.button4active = true;
						showWrongComboBox ();
					}
				}
			}
		}
		if (this.name.Equals ("SafeButton4") && safeHandCollide.arrayActive) {	
			if(Input.GetKeyUp ("b")&& safeHandCollide.codeGuess.Count != 4){
				safeHandCollide.codeGuess.Add(4);
				safeHandCollide.guessLength++;
				source.Play();
				Debug.Log("Added");
				safeHandCollide.button4active = false;;
				this.GetComponent<Animation>().Play();
				if(!safeHandCollide.codeGuess[safeHandCollide.guessLength-1].Equals(safeHandCollide.codeBreak[safeHandCollide.guessLength-1]) && safeHandCollide.guessLength <= 3){
					safeHandCollide.codeGuess.Clear();
					safeHandCollide.guessLength = 0;
					source2.Play();
					Debug.Log("Incorrect");
					safeHandCollide.button1active = true;
					safeHandCollide.button2active = true;
					safeHandCollide.button3active = true;
					safeHandCollide.button4active = true;
					showWrongComboBox ();
				}
				if(Input.GetKeyUp ("b") && guessLength == 4){
					if(safeHandCollide.codeGuess[safeHandCollide.guessLength-1].Equals(safeHandCollide.codeBreak[safeHandCollide.guessLength-1])){
						safeHandCollide.arrayActive = false;
						GameObject.Find("SafeDoorHinge").GetComponent<Animation>().Play("SafeDoorOpen");
						Interact.safeOpenClose = 1;
					}
					else {
						safeHandCollide.button1active = true;
						safeHandCollide.button2active = true;
						safeHandCollide.button3active = true;
						safeHandCollide.button4active = true;
						showWrongComboBox ();
					}
				}
			}
		}
		}
		
	}
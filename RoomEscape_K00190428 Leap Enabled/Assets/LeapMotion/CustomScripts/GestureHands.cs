using UnityEngine;
using System.Collections;
using Leap;

public class GestureHands : MonoBehaviour {

	Controller controller;

	void Start () {
		controller = new Controller ();
		controller.EnableGesture (Gesture.GestureType.TYPESWIPE);
		controller.Config.SetFloat ("Gesture.Swipe.MinLength", 100.0f);
		controller.Config.SetFloat ("Gesture.Swipe.MinVelocity", 400.0f);
		controller.Config.Save ();
	}

	void Update () {
		Frame frame = controller.Frame ();
		GestureList gestures = frame.Gestures ();
		for (int i = 0; i < gestures.Count; i++) {
			Gesture gesture = gestures[i];
			if(gesture.Type == Gesture.GestureType.TYPE_SWIPE){
				SwipeGesture Swipe = new SwipeGesture(gesture);
				Vector swipeDirection = Swipe.Direction;
				if(swipeDirection.x < 0){
					Debug.Log("Left");
				}else if(swipeDirection.x > 0){
					Debug.Log("Right");
				}
			}
				}

	}
}

using UnityEngine;
using System.Collections;

public class SwipeDetection : MonoBehaviour {
	public enum Swipe{Left,Right,Up,Down,Tap};
	float screenDiagonalSize;
	float minSwipeDistancePixels;
	bool touchStarted;
	float touchTime;
	Vector2 touchStartPos;
	public float minSwipeDistance = 0.1f;
	public static event System.Action<Swipe> OnSwipeDetected;
	
	void Start() {
		screenDiagonalSize = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
		minSwipeDistancePixels = minSwipeDistance * screenDiagonalSize; 
	}
	
	void Update() {

		if (touchStarted) {
			touchTime += Time.deltaTime;
			if (touchTime >= 0.08f) {
				touchStarted = false;
				OnSwipeDetected(Swipe.Tap);
			}
		}

		if (Input.touchCount > 0) {
			var touch = Input.touches[0];
			
			switch (touch.phase) {
				case TouchPhase.Began:
					touchStarted = true;
					touchStartPos = touch.position;
					touchTime = 0;
					break;
					
				case TouchPhase.Ended:
					if (touchStarted) {
						touchStarted = false;
						OnSwipeDetected(Swipe.Tap);
					}
					break;
					
				case TouchPhase.Canceled:
					touchStarted = false;
					break;

				case TouchPhase.Stationary:
					break;
					
				case TouchPhase.Moved:
					if (touchStarted) {
						if (TestForSwipeGesture(touch)) {
							touchStarted = false;
						}
					}
					break;
			}
		}
	}

	bool TestForSwipeGesture(Touch touch){
		Vector2 lastPos = touch.position;
		float distance = Vector2.Distance(lastPos, touchStartPos);
		
		if (distance > minSwipeDistancePixels) {
			float dy = lastPos.y - touchStartPos.y;
			float dx = lastPos.x - touchStartPos.x;
			
			float angle = Mathf.Rad2Deg * Mathf.Atan2(dx, dy);
			
			angle = (360 + angle - 45) % 360;
			
			if (angle < 90) {
				d("SwipeRight");
				OnSwipeDetected(Swipe.Right);
			} else if (angle < 180) {
				d("SwipeDown");
				OnSwipeDetected(Swipe.Down);
			} else if (angle < 270) {
				d("SwipeLeft");
				OnSwipeDetected(Swipe.Left);
			} else {
				d("SwipeUp");
				OnSwipeDetected(Swipe.Up);
			}
			return true;
		}

		return false;
	}

	public void d(string msg) {
		Debug.Log (msg);
	}
}

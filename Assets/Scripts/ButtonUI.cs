using UnityEngine;
using System.Collections;

public class ButtonUI : MonoBehaviour {

	public void OnClick() {
		Debug.Log("Click: " + gameObject.name);

		switch (gameObject.name) {
			
		case "ButtonRetry":
		case "ButtonPlay":
			Application.LoadLevel("Game");
			//Destroy(transform.parent.parent.gameObject);
			//if (ScreenControl.lastInstance) {
			//	ScreenControl.lastInstance.PickLevel();
			//}
			break;

		case "ButtonPause":
			GameControl.GetInstance().GamePause();
			break;

		case "ButtonResume":
			Destroy(transform.parent.parent.gameObject);
			GameControl.GetInstance().GameResume();
			break;

		case "ButtonQuit":
			Application.LoadLevel("MenuScreen");
			break;
		}
	}
	
	public void OnPress(bool isDown) {
		if (!isDown) return;
		//Debug.Log("Press: " + gameObject.name);
		switch (gameObject.name) {
			case "Input":
			GameControl.GetInstance().Tap();
			break;
		}
	}
}

using UnityEngine;
using System.Collections;

public class ScreenControl : MonoBehaviour {

	public Transform uiRoot;
	public GameObject prefabPauseScreen;
	public GameObject prefabFailScreen;
	public GameObject prefabLevelScreen;

	public static ScreenControl lastInstance;

	public void Start() {
		lastInstance = this;
	}

	public void Pause() {
		if (prefabPauseScreen != null && uiRoot != null) {
			GameObject pauseScreen =  Instantiate(prefabPauseScreen) as GameObject;
			pauseScreen.transform.parent = uiRoot;
			pauseScreen.transform.localPosition = Vector3.zero;
			pauseScreen.transform.localScale = Vector3.one;
		}
	}

	public void Fail() {
		if (prefabFailScreen != null && uiRoot != null) {
			GameObject failScreen =  Instantiate(prefabFailScreen) as GameObject;
			failScreen.transform.parent = uiRoot;
			failScreen.transform.localPosition = Vector3.zero;
			failScreen.transform.localScale = Vector3.one;
		}
	}

	public void PickLevel() {
		if (prefabLevelScreen != null && uiRoot != null) {
			GameObject levelScreen =  Instantiate(prefabLevelScreen) as GameObject;
			levelScreen.transform.parent = uiRoot;
			levelScreen.transform.localPosition = Vector3.zero;
			levelScreen.transform.localScale = Vector3.one;
		}
	}
}
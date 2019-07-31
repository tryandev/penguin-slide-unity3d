using UnityEngine;
using System.Collections;

public class EventStopper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseDown() {
		Debug.Log ("OnMouseDown");
	}

	public void OnMouseUp() {
		Debug.Log ("OnMouseUp");
	}

	public void OnPress() {
		Debug.Log ("OnPress");
	}

	public void OnClick() {
		Debug.Log ("OnClick");
	}
}

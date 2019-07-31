using UnityEngine;
using System.Collections;

public class SnowmanBehaviour : MonoBehaviour {

	private bool used;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (used) return;
		used = true;
		other.gameObject.SendMessage ("ItemCollect", gameObject, SendMessageOptions.DontRequireReceiver);
		GetComponentInChildren<Animator>().SetInteger("state", 1);
	}
}

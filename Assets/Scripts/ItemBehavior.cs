using UnityEngine;
using System.Collections;

public class ItemBehavior : MonoBehaviour {

	//private Transform model;
	//public AudioClip soundFish;

	void Start () {
		//glow = transform.FindChild("Glow");
		//model = transform.FindChild("Model");
	}
	
	// Update is called once per frame
	void Update () {
		//model.Rotate(-Vector3.up*Time.deltaTime * 100f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		other.gameObject.SendMessage ("ItemCollect", gameObject, SendMessageOptions.DontRequireReceiver);
	}
}

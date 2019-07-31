using UnityEngine;
using System.Collections;

public class GarbageCollector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D c) {
		//Debug.Log (c.gameObject.name);
		if (c.gameObject.transform.parent != null) {
			ItemCollect(c.gameObject.transform.parent.gameObject);
		} else {
			ItemCollect(c.gameObject);
		}
	}
	
	public void ItemCollect(GameObject item) {
		//Debug.Log ("Garbage: " + item.name);
		Destroy (item);
	}
}

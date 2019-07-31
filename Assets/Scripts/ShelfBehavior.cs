using UnityEngine;
using System.Collections;

public class ShelfBehavior : MonoBehaviour {

	public int size = 1;
	public GameObject spriteLeft;
	public GameObject spriteMid;
	public GameObject spriteRight;

	// Use this for initialization
	void Start () {
		float currentLeft = 0;

		GameObject go = Instantiate (spriteLeft) as GameObject;
		go.transform.parent = transform;
		go.transform.localPosition = new Vector3 (currentLeft, 0, 0);
		currentLeft += 1;

		for (int i = 0; i < size; i++) {
			go = Instantiate (spriteMid) as GameObject;
			go.transform.parent = transform;
			go.transform.localPosition = new Vector3 (currentLeft, 0, 0);
			currentLeft += 1.8f;
		}
		
		go = Instantiate (spriteRight) as GameObject;
		go.transform.parent = transform;
		go.transform.localPosition = new Vector3 (currentLeft, 0, 0);
		currentLeft += 1;

		BoxCollider box = GetComponent<BoxCollider> ();
		box.size = new Vector3(currentLeft,1,1);
		box.center = new Vector3 (box.size.x/2,-0.5f,0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

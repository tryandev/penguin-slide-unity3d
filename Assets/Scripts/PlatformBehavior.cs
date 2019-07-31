using UnityEngine;
using System.Collections;

public class PlatformBehavior : MonoBehaviour {
	public GameObject spriteLeft;
	public GameObject spriteMid;
	public GameObject spriteRight;
	
	private int _pieces;
	private float _scale;

	public void Init(int width) {
		_pieces = width;
		_scale = 0.5f;
		float currentLeft = 0;
		GameObject go;

		go = Instantiate (spriteLeft) as GameObject;
		go.transform.parent = transform;
		go.transform.position = new Vector3 (currentLeft, 0, 0);
		currentLeft += 2.0f * _scale;
		go.transform.localScale = Vector3.one * 0.5f;
		
		for (int i = 0; i < _pieces; i++) {
			go = Instantiate (spriteMid) as GameObject;
			go.transform.parent = transform;
			go.transform.position = new Vector3 (currentLeft, 0, 0);
			currentLeft += 3.6f * _scale;
			go.transform.localScale = Vector3.one * 0.5f;
		}
		
		go = Instantiate (spriteRight) as GameObject;
		go.transform.parent = transform;
		go.transform.position = new Vector3 (currentLeft, 0, 0);
		go.transform.localScale = Vector3.one * 0.5f;
		currentLeft += 2.37f * _scale;
		
		BoxCollider2D box = GetComponent<BoxCollider2D> ();
		box.size = new Vector2(currentLeft,12);
		box.center = new Vector2 (box.size.x / 2,-box.size.y / 2);
		//transform.localScale *= 1;
		//transform.RotateAround(box.center, Vector3.forward, -5f);
	}

	public float GetWidth() {
		return (4.37f + _pieces * 3.6f)* _scale;
	}
}

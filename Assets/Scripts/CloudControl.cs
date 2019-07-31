using UnityEngine;
using System.Collections;

public class CloudControl : MonoBehaviour {


	private float speed;
	// Use this for initialization
	void Start () {
		Reset(false, this.transform.parent);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition += Vector3.left * Time.deltaTime * speed;
	}

	public void Reset(bool respawn, Transform parent) {
		transform.parent = parent;
		Vector3 scale = Vector3.one * Random.Range (2f, 8f);
		Vector3 pos = transform.localPosition;
		if (respawn) pos += Vector3.right * 30f;
		pos.y = Random.Range (1f,7f);
		transform.localPosition = pos;
		transform.localScale = scale;
		speed = (1f+ Random.Range(1f,3f)) * 10f;
	}
}

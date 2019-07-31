using UnityEngine;
using System.Collections;

public class SectionBehavior : MonoBehaviour {

	// Use this for initialization

	public GameObject coinPrefab;
	public GameObject fishPrefab;

	void Start () {
		for (int i = 0; i < 6; i++) {
			Vector3 myPos = transform.position;
			myPos.x = -4 + i*1.5f;
			myPos.y = 3.5f;
			GameObject prefab = i==5? fishPrefab:coinPrefab;
			GameObject go = Instantiate (prefab) as GameObject;
			go.name = prefab.name;
			go.transform.parent = gameObject.transform;
			go.transform.localPosition = myPos;
		}
		//transform.Rotate (Vector3.forward * Random.Range(-1,1) * 45);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

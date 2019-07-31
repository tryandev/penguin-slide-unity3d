using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	public float trackSpeed;
	private Transform target;
	private PlayerControl pc;

	public void SetTarget(Transform t) {
		target = t;
		pc = t.gameObject.GetComponent<PlayerControl> ();
	}

	void FixedUpdate() {

		if (!pc.isAlive) return;

		if (Camera.main.WorldToScreenPoint(target.position).x < 0f) {
			target.gameObject.SendMessage("Die");
			return;
		}

		transform.Find ("Garbage").transform.localPosition = Vector2.right * -75f;

		if (target) {
			float x = Mathf.Max(transform.position.x + pc.runSpeed/10f, LagTowards(transform.position.x, target.position.x + 4f, trackSpeed));
			float y = LagTowards(transform.position.y, target.position.y + 0.5f, trackSpeed);
			//float y = LagTowards(transform.position.y, gameObject.GetComponent<GameController>().exitY+3, 0.1f);
			transform.position = new Vector3(x,transform.position.y, transform.position.z);
			Material material = transform.FindChild("Background").GetChild(0).gameObject.renderer.material;
			//Debug.Log("count: " + transform.childCount + " - " + transform.FindChild("Background1").GetChild(i).name);
			material.SetTextureOffset("_MainTex", new Vector2(x / 400, 0));
		}
	}
	
	private float LagTowards(float n, float target, float a) {
		return n + (target - n) * a;
	}

}

using UnityEngine;
using System.Collections;

public class BearBehaviour : MonoBehaviour {
	
	public LayerMask collisionMask;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		Vector2 vel = rigidbody2D.velocity;
		float scale = transform.localScale.x;
		CircleCollider2D col = GetComponent<CircleCollider2D> ();
		Vector2 pos = new Vector2 (transform.position.x,transform.position.y);
		float length = col.radius * scale;

		Ray2D rayDown = new Ray2D(pos + col.center, Vector2.up * -1);
		Ray2D rayLeft = new Ray2D(pos + col.center, Vector2.right * -1);

		RaycastHit2D hitDown;
		RaycastHit2D hitLeft;
		
		//hitDown = Physics2D.Raycast (rayDown.origin, rayDown.direction, Mathf.Infinity, collisionMask);
		//hitLeft = Physics2D.Raycast (rayLeft.origin, rayLeft.direction, Mathf.Infinity, collisionMask);
		hitDown = Physics2D.Raycast (rayDown.origin, rayDown.direction, length+0.2f, collisionMask);
		hitLeft = Physics2D.Raycast (rayLeft.origin, rayLeft.direction, length+0.2f, collisionMask);

		float distDown;
		float distLeft;
		distDown = hitDown.collider ? (hitDown.point - rayDown.origin).magnitude : length*2;
		distLeft = hitLeft.collider ? (hitLeft.point - rayLeft.origin).magnitude : length*2;
		
		Debug.DrawRay (rayDown.origin, rayDown.direction * distDown, Color.red);
		Debug.DrawRay (rayLeft.origin, rayLeft.direction * distLeft, Color.black);
		vel.x = distLeft > length ? -1:0;
		vel.y = distDown > length ? -3:0;
		rigidbody2D.velocity = vel;
//		Debug.Log ();
	}
}

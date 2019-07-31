using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	public bool canDoubleJump;
	public float runSpeed;
	public float jumpPower;
	//public float accel;
	public LayerMask collisionMask;

	private bool grounded = false;
	private bool doubleJumped = false;
	private float airTime = 100f;
	private float skin = 0.05f;
	private RaycastHit2D hit1;
	private RaycastHit2D hit2;
	private Animator anim;
	private bool alive = true;

//	private bool sliding = false;
//	private float slideTimeMax = 0.5f;
//	private float slideTimeCurrent = 0f;

	private int coins;
	private bool tapped;


	private float lastAngle = 0;
	private bool lastGrounded = false;

	void Start () {
		anim = GetComponentInChildren<Animator> ();
	}
	
	void FixedUpdate () {
		Vector2 currentVelocity = rigidbody2D.velocity;
		if (!alive) {
			currentVelocity.x = currentVelocity.x * 0.98f;
			rigidbody2D.velocity = currentVelocity;
			return;
		}

		GameControl.GetInstance().DistanceSet(transform.position.x);

		BoxCollider2D collider = GetComponent<BoxCollider2D> ();
		Vector2 pos = new Vector2 (transform.position.x,transform.position.y);
		
		Ray2D ray1;
		Ray2D ray2;
		float dist1;
		float dist2;
		Vector2 normal;
		float forceFieldSize = 0.66f;
		float groundThreshold = 0.09f;

		ray1 = new Ray2D(pos - collider.center - Vector2.right * (collider.size.x/2 - skin), -Vector2.up);
		ray2 = new Ray2D(pos - collider.center + Vector2.right * (collider.size.x/2 - skin), -Vector2.up);

		hit1 = Physics2D.Raycast (ray1.origin, ray1.direction, Mathf.Infinity, collisionMask);
		hit2 = Physics2D.Raycast (ray2.origin, ray2.direction, Mathf.Infinity, collisionMask);
		
		dist1 = (hit1.point - ray1.origin).magnitude;
		dist2 = (hit2.point - ray2.origin).magnitude;
		float dist = Mathf.Min (dist1, dist2);
		normal = dist1 < dist2 ? hit1.normal : hit2.normal;

		Debug.DrawRay (ray1.origin, ray1.direction * dist1, Color.red);
		Debug.DrawRay (ray2.origin, ray2.direction * dist2, Color.black);

		if (dist - forceFieldSize < groundThreshold) {
			grounded = true;
		} else {
			grounded = false;
		}
		
		if (grounded) {
//			if (sliding) {
//				slideTimeCurrent += Time.deltaTime;
//				if (slideTimeCurrent >= slideTimeMax) {
//					sliding = false;
//				}
//			}
			doubleJumped = false;
			airTime = 0;
		}else{
			//sliding = false;
			airTime += Time.deltaTime;
		}

		float newAngle = Vector2.Angle (Vector2.up, normal);
		//Debug.Log("new Angle: " + newAngle + "\t" + "dist: " + dist);
		if (lastAngle == 0 && newAngle > 15 && lastGrounded) {
			//Debug.Log("Correction!!!");
			float r = currentVelocity.magnitude;
			currentVelocity = new Vector2(r * Mathf.Cos(newAngle), -r * Mathf.Sin(newAngle));
		}
		lastGrounded = grounded;
		lastAngle = newAngle;

		if (tapped) {
			if (grounded) {
				currentVelocity.y = jumpPower;
			} else if (tapped){
				if (canDoubleJump && !doubleJumped && airTime > 0.020f * 5f) {
					doubleJumped = true;
					currentVelocity.y = jumpPower;
				}else{
					Debug.Log("can't double jump: " + canDoubleJump + ", " + doubleJumped);
				}
			}
		}

		//float addForce = Mathf.Max(0, (runSpeed - currentVelocity.x) * 2700f);
		//addForce = Mathf.Min (270f, addForce);
		//Debug.Log ("addForce: " + addForce);
		//rigidbody2D.AddForce(Vector2.right * addForce);
		currentVelocity.x = Mathf.Max (currentVelocity.x, runSpeed * 6f);
		//currentVelocity.x = Mathf.Min (currentVelocity.x, 10f);
		rigidbody2D.velocity = currentVelocity;//currentVelocity.normalized * Mathf.Min(currentVelocity.magnitude,15f);
		tapped = false;

	}
	// Update is called once per frame
	
	void Update() {
		
		
//		if (!alive) {
//			anim.SetInteger("state",4);
//			return;
//		}
		if (grounded) {
			anim.SetInteger("state",0);
		} else if (rigidbody2D.velocity.y < 0) {
			anim.SetInteger("state",2);
		}
//		else if (doubleJumped) {
//			anim.SetInteger("state",3);
//		} 
		else {
			anim.SetInteger("state",1);
		}
	}
	
	public void Die() {
		if (alive) {
			alive = false;
			GameControl.GetInstance().GameFail();
			//Destroy(this.gameObject);
			ParticleSystem deathParticles = GetComponentInChildren<ParticleSystem>();
			SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
			sprite.enabled = false;
			deathParticles.Simulate(3f);
			deathParticles.Stop();
			deathParticles.Play();
		}
	}
	
	public void ItemCollect(GameObject item) {
		if (item.name.StartsWith ("Fish")) {
			//AudioSource.PlayClipAtPoint(fishSound, transform.position);
			Destroy(item);
		}

		if (item.name.StartsWith ("Coin")) {
			//AudioSource.PlayClipAtPoint(coinSound, transform.position);
			//;
			GameControl.GetInstance().CoinAdd(1);
			Destroy(item);
		}
		
		if (item.name.StartsWith ("Pit")) {
			Destroy(item);
			Die();
		}
		
		if (item.name.StartsWith ("Bear")) {
			Die();
		}
		
		if (item.name.StartsWith ("Snowman")) {
			doubleJumped = false;
			Vector2 vel = rigidbody2D.velocity;
			vel.y = jumpPower*1.5f;
			rigidbody2D.velocity = vel;
		}
	}

	public void Tap() {
		tapped = true;
	}

	public bool isAlive{
		get {
			return alive;
		}
	}
}

using UnityEngine;
using System.Collections;


[RequireComponent(typeof(PlayerPhysics))]
public class PlayerControlOld : MonoBehaviour {

	public float gravity = 30;
	public float speed = 8;
	public float accel = 30;
	public float maxJumps = 2;
	public bool autoRun = false;


	public AudioClip jumpSound;
	public AudioClip coinSound;
	public AudioClip fishSound;

	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	private float jumpSpeed = 16;
	private int jumpCount = int.MaxValue;

	private bool isGrown;
	private float growTimer;
	private bool jumpButtonPressed;
	private PlayerPhysics playerPhysics;



	// Use this for initialization
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (isGrown) {
			growTimer -= Time.deltaTime;
			if (growTimer <=0) {
				isGrown = false;
				transform.localScale = Vector3.one * 1f;
			}
		}

		if (playerPhysics.stopped) {
			targetSpeed = 0;
			currentSpeed = 0;
		}

		if (autoRun) {
			targetSpeed = speed;
		} else {
			targetSpeed = Input.GetAxisRaw ("Horizontal") * speed;
		}

		currentSpeed = IncrementTowards(currentSpeed,targetSpeed,accel);
		amountToMove.x = currentSpeed;
		amountToMove.y -= playerPhysics.grounded ? 0 : gravity * Time.deltaTime;

		if (playerPhysics.grounded) {
			amountToMove.y = 0;
			jumpCount = 0;
		}

		if (jumpButtonPressed) {
			jumpButtonPressed = false;
			if (jumpCount < maxJumps){
				jumpCount++;
				AudioSource.PlayClipAtPoint(jumpSound, transform.position);
				amountToMove.y = jumpSpeed;
			}
		}

		//Vector2 delta = playerPhysics.Move(amountToMove * Time.deltaTime);
		
		//Animation animation = GetComponentInChildren<Animation>();
		//string clipName;
		//Debug.Log("Grounded TEST: " + playerPhysics.grounded);

		Transform penguinSprite = transform.FindChild ("PenguinSprite");

		if (playerPhysics.grounded) {
			penguinSprite.rotation = Quaternion.identity;
		}else{
			penguinSprite.rotation =  Quaternion.identity * Quaternion.Euler(0, 0, 90);
		}

		return;
//		if (playerPhysics.grounded) {
//			clipName = "PenguinRun"; 
//		}else{
//			clipName = "PenguinJump"; 
//		}
//		if (!animation.IsPlaying(clipName)) {
//			animation.CrossFade(clipName,0.15F);
//			//Debug.Log("Animation: " + clipName);
//		}
//		animation["PenguinRun"].speed = delta.x*10f/transform.lossyScale.x;
//		animation["PenguinJump"].speed = delta.x*10f/transform.lossyScale.x;
	}

	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;
		} else {
			float dir = Mathf.Sign(target - n);
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target -n)) ? n:target;
		}
	}

	void Update() {
		jumpButtonPressed = jumpButtonPressed || Input.GetButtonDown("Jump");
		jumpButtonPressed = jumpButtonPressed || Input.GetMouseButtonDown(0);
		jumpButtonPressed = jumpButtonPressed || (Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
	}

	public void Grow() {
		growTimer = 5;
		isGrown = true;
		transform.localScale = Vector3.one * 2f;
	}

	public void Die() {

	}

	public void ItemCollect(GameObject item) {
		if (item.name == "Fish") {
			AudioSource.PlayClipAtPoint(fishSound, transform.position);
			Grow ();
		}
		if (item.name == "Coin") {
			AudioSource.PlayClipAtPoint(coinSound, transform.position);
			//;
		}
	}
}






using UnityEngine;
using System.Collections;

[RequireComponent((typeof(BoxCollider)))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;

	private BoxCollider boxCollider = null;
	private Vector3 colliderSize;
	private Vector3 colliderCenter;
	
	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public bool stopped;

	private float skin = 0.001000047f;

	private Ray ray;
	private RaycastHit hit;
	private RaycastHit groundHit;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider> ();
		colliderCenter = boxCollider.center;
		colliderSize = boxCollider.size;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector2 Move(Vector2 moveAmount) {
		Vector3 c = colliderCenter;
		Vector3 s = colliderSize * transform.lossyScale.x;

		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;

		Vector2 p = transform.position;

		grounded = false;

		float x = 0;
		float y = 0;
		float insetX = s.x / 2;
		float insetY = s.y / 2;
		float dir = deltaY <= 0 ? -1:1;
		deltaY = Mathf.Abs(deltaY);
		float lastDist = float.MaxValue;
		//int shortIndex = -1;

		for (int i = 0; i < 3; i++) {

			//Debug.Log("deltaY: " + deltaY + " " + dir);
			x = (p.x + c.x - s.x / 2) + s.x / 2 * i;
			//y = p.y + c.y + s.y / 2 * dir;
			y = p.y + c.y;

			ray = new Ray(new Vector2(x,y),new Vector2(0, dir));
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, collisionMask)) {
				Debug.DrawRay(ray.origin,ray.direction*hit.distance,Color.red);
				if (hit.distance < lastDist)  {
					lastDist = hit.distance;
					float dst = hit.distance - insetY;
					
					//Debug.Log (i + ") deltaY: " + deltaY + ", dst - skin: " + dst + " - " + skin + " = " + (dst-skin));

					if (deltaY > dst - skin) {
						deltaY = dst - skin;
						Debug.DrawRay(hit.point, hit.normal, Color.blue);
						//shortIndex = i;
						groundHit = hit;
					}

					if (deltaY >= dst - skin - 0.00001f) {
						grounded = dir <= 0;
					}
				}
			}
			//if (i==3) Debug.Log("3 Grounded set to "+grounded+": " + deltaY +"           y : " + y);
		}
		deltaY = deltaY * dir;
		//Debug.Log ("short index: " + shortIndex);
		//Debug.Log("deltaY: " + deltaY);

		stopped = false;
		dir = deltaX <= 0 ? -1:1;
		deltaX = Mathf.Abs(deltaX);		
		lastDist = float.MaxValue;

		Vector3 xVect = new Vector3 (dir, 0, 0);

		if (grounded) {
			xVect = Vector3.Cross(groundHit.normal, Vector3.forward);
		}

		for (int i = 0; i < 3; i++) {
			//float dir = Mathf.Sign (deltaX);
			x = p.x + c.x + s.x / 2 * dir - insetX * dir;
			y = p.y + c.y - s.y / 2 + s.y / 2 * i + deltaY + skin;

			ray = new Ray(new Vector3(x,y), xVect);
			Debug.DrawRay(ray.origin, xVect * (Mathf.Abs(deltaX + insetX) + skin),Color.cyan);

			if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaX + insetX) + skin, collisionMask)) {
				if (hit.distance < lastDist)  {
					lastDist = hit.distance;
					float dst = hit.distance - insetX;
					if (deltaX >= dst - skin - 0.00001f) {
						deltaX = dst - skin;
						stopped = true;
					}
				}
			}
		}
		xVect *= deltaX;
		deltaX = xVect.x * dir;
		deltaY += xVect.y;
		
		//Debug.Log ("deltaY: " + deltaY);

//		if (!stopped && !grounded) {
//			Vector3 playerDir = new Vector3(deltaX, deltaY);
//			Vector3 o = new Vector3(p.x + c.x + s.x /2 * Mathf.Sign(deltaX), p.y + c.y + s.y / 2 * Mathf.Sign(deltaY));
//			ray = new Ray(o,playerDir.normalized);
//			Debug.DrawRay(ray.origin,ray.direction);
//			if (Physics.Raycast(ray, out hit, Mathf.Sqrt (deltaX * deltaX + deltaY * deltaY), collisionMask)) {
//				if (hit.normal == Vector3.up) {
//					grounded = true;
//					deltaY = 0;
//					Debug.Log("Ground");
//				}else{
//					stopped = true;
//					deltaX = 0;
//					Debug.Log("Stopped");
//				}
//			}
//		}

		Vector2 finalTransform = new Vector2 (deltaX, deltaY);
		transform.Translate (finalTransform);
		//Debug.Log("deltaXY: " + finalTransform);
		return finalTransform;
	}


}

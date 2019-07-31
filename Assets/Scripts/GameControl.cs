using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HudControl))]
[RequireComponent(typeof(ScreenControl))]
[RequireComponent(typeof(FloorControl))]

public class GameControl : MonoBehaviour {

	public GameObject prefabPlayer;
	public GameObject prefabCoin;
	public GameObject prefabBear;

	private GameObject player;
	private GameCamera cam;

	private float exitX = 0;
	private float exitY = 0;

	private bool keepY = false;

	//private bool paused;
	private bool tapped;

	private int coins = 0;
	private int distance = 0;

	private static GameControl instance;

	public static GameControl GetInstance() {
		return instance;
	}

	void Start () {
		instance = this;
		cam = GetComponent<GameCamera> ();
		SpawnPlayer();
		Update();
		GameResume();
	}
	
	void Update() {
		//Debug.Log ("GameController: " + this.GetHashCode() + " Update");
		gameObject.GetComponent<HudControl> ().UpdateUI (coins, distance);

		if (player != null) {
			float playerX = player.transform.position.x;
			PlayerControl playerControl = player.GetComponent<PlayerControl>();

			if (tapped) {
				tapped = false;
				playerControl.Tap();
			}

			if (playerX >= exitX - 15) {
				exitX += SpawnGround(exitX);
				//SpawnCoin(exitX);
			}
		}
	}
	
	void SpawnPlayer() {
		player = Instantiate (prefabPlayer, Vector3.zero, Quaternion.identity) as GameObject;
		player.transform.Translate (Vector3.up * 3f);
		//player.transform.localScale = Vector3.one * 3f;
		cam.SetTarget(player.transform);
	}
	
	float SpawnGround(float positionX) {
		Debug.Log ("SpawnGround: " + positionX);
		FloorSet fs = GetComponent<FloorControl> ().GetSet ();
		fs.go.transform.Translate (new Vector3(positionX, 0, 0));
		return fs.width;

//		if (Random.Range (0, 5) < 2) {
//			float scale = 0.5f;
//			piece = Instantiate (prefabFloorSlantDown,  Vector3.zero, Quaternion.identity) as GameObject;
//			piece.transform.Translate (new Vector3(positionX, exitY, 0));
//			piece.transform.localScale = Vector2.one * scale;
//			exitY -= 3.61f * scale;
//			keepY = true;
//			return 3.6f * scale;
//		}else if (keepY) {
//			float scale = 0.5f;
//			piece = Instantiate (prefabFloorCurve,  Vector3.zero, Quaternion.identity) as GameObject;
//			piece.transform.Translate (new Vector3(positionX, exitY, 0));
//			piece.transform.localScale = Vector2.one * scale;
//			keepY = false;
//
//			piece = Instantiate (prefabFloorPit,  Vector3.zero, Quaternion.identity) as GameObject;
//			piece.transform.Translate (new Vector3(positionX + 3.6f*scale, exitY-3, 0));
//			piece.transform.localScale = new Vector2((6f-3.6f) * scale,1);
//
//			return 6f * scale;
//		}else{
//			int size = Random.Range(0,5);
//			piece = Instantiate (prefabFloorFlatLeft, Vector3.zero, Quaternion.identity) as GameObject;
//			PlatformBehavior platformBehavior = piece.GetComponent<PlatformBehavior> ();
//			platformBehavior.Init (size);
//			if (!keepY) exitY += Random.Range (-2.0f, 2.0f);
//			piece.transform.Translate (new Vector3(positionX, exitY, 0));
//			if (size > 2) {
//				GameObject bear = Instantiate(prefabBear) as GameObject;
//				bear.transform.position = piece.transform.position + Vector3.right * platformBehavior.GetWidth();
//				bear.transform.localScale = Vector2.one*1.5f;
//			}
//			keepY = false;
//			return platformBehavior.GetWidth();
//		}
		
	}
	
	float SpawnCoin(float positionX) {
		GameObject go;
		float scale = 1.0f;
		go = Instantiate (prefabCoin,  Vector3.zero, Quaternion.identity) as GameObject;
		go.transform.Translate (new Vector2(positionX, exitY + 3f));
		go.transform.localScale = Vector2.one * scale;
		go.name = prefabCoin.name;
		return 0;
	}
	
	
	public void GamePause() {
		GetComponent<ScreenControl> ().Pause();
		//paused = true;
		Time.timeScale = 0;
	}
	
	public void GameResume() {
		//paused = false;
		Time.timeScale = 1;
	}
	
	public void GameFail() {
		GetComponent<ScreenControl> ().Fail();
	}

	public void Tap() {
		tapped = true;
	}

	public int CoinAdd(int delta = 1) {
		coins += delta;
		return coins;
	}

	public int CoinGet() {
		return coins;
	}

	public int DistanceSet(float value) {
		distance = (int) value;
		return distance;
	}

	public int DistanceGet() {
		return distance;
	}
	
}

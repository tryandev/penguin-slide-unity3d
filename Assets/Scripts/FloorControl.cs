using UnityEngine;
using System.Collections;

public class FloorControl : MonoBehaviour {
	
	
	public TextAsset textCoins;
	public TextAsset textEasy;
	public TextAsset textMedium;
	public TextAsset textHard;

	public GameObject prefabFloorPit;
	public GameObject prefabCoin;
	
	public GameObject prefabFloorFlatLeft;
	public GameObject prefabFloorFlatMid;
	public GameObject prefabFloorFlatRight;
	
	public GameObject prefabFloorSlantDown;
	public GameObject prefabFloorSlantUp;
	public GameObject prefabFloorCurve;

	private IList codesEasy;
	private Hashtable coinPatterns;
	private bool init;

	// Update is called once per frame
	private void AddCode() {

		init = true;

		string[] lines = textEasy.text.Split('\n');
		codesEasy = new ArrayList();
		for (int i = 0; i < lines.Length; i ++) {
			string line = lines[i];
			if (line.IndexOf(")") < 0 ) continue;
			line = line.Split(')')[1];
			codesEasy.Add(line.Replace(" ", "").ToLower());
		}

		coinPatterns = new Hashtable ();
		lines = textCoins.text.Split(';');
		for (int i = 0; i < lines.Length; i ++) {
			string line = lines[i].Trim();
			if (line.Length < 1) continue;
			if (line.IndexOf(")") < 0 ) continue;
			string name = line.Split(')')[0];
			string code = line.Split(')')[1];
			Debug.Log(name +":\n" + code);
			coinPatterns.Add(name.ToLower(), code);
		}
	}

	public FloorSet GetSet() {
		if (!init) AddCode();
		//string code = codes.;
		string code = (string) codesEasy[0];
		codesEasy.RemoveAt (0);
		codesEasy.Add(code);


		//code = code.Replace (" ", "").ToLower();
		string[] commands = code.Split (',');
		Vector3 currentPos = new Vector3();
		GameObject go = new GameObject ();
		float knownWidth = 1f;
		float scale = 1f;
		for (int i = 0; i < commands.Length; i++){
			string command = commands[i];			
			GameObject piece = null;
			
//			GameObject coin = Instantiate (prefabCoin,  Vector3.zero, Quaternion.identity) as GameObject;
//			coin.transform.parent = go.transform;
//			coin.transform.localPosition = currentPos + Vector3.up * 2f;
//			coin.transform.localScale = Vector2.one;
//			coin.name = prefabCoin.name;

			switch(command) {
			case "mid":
				currentPos.y = 0;
				break;
			case "low":
				currentPos.y = -1.8f;
				break;
			case "hi":
				currentPos.y = 1.8f;
				break;
			case "flatleft":
				doGroundPiece(go,prefabFloorFlatLeft, 1.8f, 0.5f, ref currentPos);
				break;
			case "flatmid":
				doGroundPiece(go,prefabFloorFlatMid, 3.6f, 0.5f, ref currentPos);
				break;
			case "flatright":
				doGroundPiece(go,prefabFloorFlatRight, 2.17f, 0.5f, ref currentPos);
				break;
			case "slantdown":
				doGroundPiece(go,prefabFloorSlantDown, 3.6f, 0.5f, ref currentPos);
				currentPos.y -= 0.5f * 3.6f;
				break;
			case "slantup":
				doGroundPiece(go,prefabFloorSlantUp, 3.6f,0.5f, ref currentPos);
				currentPos.y += 0.5f * 3.6f;
				break;
			case "curve":
				doGroundPiece(go,prefabFloorCurve, 3.6f, 0.5f, ref currentPos);
				break;
			case "pit":
				doGroundPiece(go,prefabFloorPit,1f, 1f, ref currentPos).transform.position -= Vector3.up * 2f;
				break;
			default:
				doOthers(command, go, ref currentPos);
				break;
			}
		}
		FloorSet fs = new FloorSet ();
		fs.go = go;
		fs.width = currentPos.x;
		return fs;
	}

	private GameObject doOthers(string command, GameObject container, ref Vector3 pos) {
		if (command.StartsWith ("coins")) {
			string[] commandParts = command.Split(':');
			if (commandParts.Length == 3) {
				string name = commandParts[1];
				string code = coinPatterns[name] as string;
				string[] lines = code.Split('\n');
				GameObject pattern = new GameObject();
				float minY = float.MaxValue;
				for (int y = 0; y < lines.Length; y++) {
					char[] line = lines[y].ToCharArray();
					for (int x = 0; x < line.Length; x++) {
						char letter = line[x];
						if (letter != ' ') {
							GameObject coin = Instantiate(prefabCoin) as GameObject;
							coin.transform.parent = pattern.transform;
							coin.transform.localPosition = new Vector3(x,-y,0);
							coin.transform.localScale = Vector3.one;
							minY = Mathf.Min(minY,-y);
						}
					}
				}
				pattern.transform.position = pos - Vector3.up * (minY - 0.5f); 
				pattern.transform.parent = container.transform;
				return pattern;
			}
		}
		return null;
	}

	private GameObject doGroundPiece(GameObject container, GameObject prefab, float width, float scale, ref Vector3 pos) {
		GameObject piece = Instantiate(prefab) as GameObject;
		piece.transform.parent = container.transform;
		piece.transform.localPosition = pos;
		piece.transform.localScale = Vector3.one * scale;
		pos.x += scale * width;
		return piece;
	}
}

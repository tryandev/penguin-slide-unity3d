using UnityEngine;
using System.Collections;

public class HudControl : MonoBehaviour {
	
	public UILabel labelDistance;
	public UILabel labelCoins;

	// Update is called once per frame
	public void UpdateUI (int coins, int distance) {		
		labelDistance.text = distance + "";
		labelCoins.text = coins + "";	
	}
}

using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	private  float barValue;
	private float hScrollbarValue;
	void OnGUI()
	{
		barValue = gameObject.GetComponent<PlayerControl>().health;
		GUI.backgroundColor = Color.green;
		hScrollbarValue = GUI.HorizontalScrollbar (new Rect (500, 15, 200, 20), barValue, 0f, 0f, 5000f);
	}
		//for this example, the bar display is linked to the current time,
		//however you would set this value based on your desired display
		//eg, the loading progress, the player's health, or whatever.
		//barDisplay = gameObject.GetComponent<PlayerControl>().Health;
		//        barDisplay = MyControlScript.staticHealth;
}

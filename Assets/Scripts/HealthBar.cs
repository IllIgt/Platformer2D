//using UnityEngine;
//using System.Collections;
//
//public class HealthBar : MonoBehaviour {
//	private  float barValue;
//	private float hScrollbarValue;
//	void OnGUI()
//	{
//		barValue = gameObject.GetComponent<PlayerControl>().health;
//		GUI.backgroundColor = Color.green;
//		hScrollbarValue = GUI.HorizontalScrollbar (new Rect (500, 15, 200, 20), barValue, 0f, 0f, 5000f);
//	}
//		//for this example, the bar display is linked to the current time,
//		//however you would set this value based on your desired display
//		//eg, the loading progress, the player's health, or whatever.
//		//barDisplay = gameObject.GetComponent<PlayerControl>().Health;
//		//        barDisplay = MyControlScript.staticHealth;
//}
using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {
	public GUISkin[] s1;
	private float hSliderValue = 0.0F;
	private float vSliderValue = 0.0F;
	private float hSValue = 0.0F;
	private float vSValue = 0.0F;
	private int cont = 0;
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space))
			cont++;

	}
	void OnGUI() {
		GUI.skin = s1[cont % s1.Length];
		if (s1.Length == 0) {
			Debug.LogError("Assign at least 1 skin on the array");
			return;
		}
		GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");
		GUI.Box(new Rect(10, 50, 50, 50), "A BOX");
		if (GUI.Button(new Rect(10, 110, 70, 30), "A button"))
			Debug.Log("Button has been pressed");

		hSliderValue = GUI.HorizontalSlider(new Rect(10, 150, 100, 30), hSliderValue, 0.0F, 10.0F);
		vSliderValue = GUI.VerticalSlider(new Rect(10, 170, 100, 30), vSliderValue, 10.0F, 0.0F);
		hSValue = GUI.HorizontalScrollbar(new Rect(10, 210, 100, 30), hSValue, 1.0F, 0.0F, 10.0F);
		vSValue = GUI.VerticalScrollbar(new Rect(10, 230, 100, 30), vSValue, 1.0F, 10.0F, 0.0F);
	}
}

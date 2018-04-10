using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	private  float barValue;
	private float maxValue;
	private Slider _healthBar;
	private GameObject Hero;
	void Start()
	{
		_healthBar = GetComponent<Slider> ();
		Hero= GameObject.Find("hero");
		_healthBar.maxValue = Hero.GetComponent<PlayerControl> ().health;

	}

	void OnGUI()
	{
		barValue = Hero.GetComponent<PlayerControl>().health;
		Debug.Log ("111" + Hero.GetComponent<PlayerControl> ().health);
		_healthBar.value = barValue;
	}
		//for this example, the bar display is linked to the current time,
		//however you would set this value based on your desired display
		//eg, the loading progress, the player's health, or whatever.
		//barDisplay = gameObject.GetComponent<PlayerControl>().Health;
		//        barDisplay = MyControlScript.staticHealth;
}


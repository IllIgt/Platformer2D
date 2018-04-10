using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinChecker : MonoBehaviour {
    private int Enemies;
    public Image Win;

	
	// Update is called once per frame
	void FixedUpdate () {
        Enemies = FindObjectsOfType<MyEnemy>().Length + FindObjectsOfType<Turrel>().Length;
        Debug.Log("!!!!!!!!          " + Enemies);
        if (Enemies == 0)
        {
            Instantiate(Win, GameObject.Find("Canvas").transform);
            Time.timeScale = 0;
            
        }
        Debug.Log("!!!!!!!!          " + GameObject.Find("WinSprite"));

    }
}

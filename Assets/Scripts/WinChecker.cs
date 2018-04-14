using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinChecker : MonoBehaviour {
    private int Enemies;
    public Image Win;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        Enemies = FindObjectsOfType<MyEnemy>().Length + FindObjectsOfType<Turrel>().Length;
        if (Enemies == 0)
        {
            _gameManager.ShowWinPanel();   
        }

    }
}


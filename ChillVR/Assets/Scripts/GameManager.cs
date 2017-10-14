using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int totalScore = 0;
    public bool isCleaningPhase = false;

    public Text scoreText;  //to display on canvas
    public Text CleaningPhaseBanner;

	// Use this for initialization
	void Start () {
        UpdateScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateScore()   //refreshes score on canvas
    {
        if (scoreText)
        {
            scoreText.text = totalScore.ToString();
        }
    }

    public void AddScore(int value)
    {
        totalScore += value;
        UpdateScore();
    }

    public void StartCleaningPhase()
    {
        isCleaningPhase = true;
        CleaningPhaseBanner.gameObject.SetActive(true);
    }
}

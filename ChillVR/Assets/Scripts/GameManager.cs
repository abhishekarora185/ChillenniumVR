﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float AllowedDistanceFromOrigin = 5f;
    public float AllowedDifferenceInOrientationDegrees = 20.0f;

    public int totalScore = 0;
    public bool isCleaningPhase = false;
    public bool isGameOver = false;

    public Text scoreText;  //to display on canvas
    public Text CleaningPhaseBanner;

    public Material LightObjectMaterial;
    public Material MediumObjectMaterial;
    public Material HeavyObjectMaterial;
    public Material InPositionLightMaterialCleaningPhase;
    public Material OutOfPositionLightMaterialCleaningPhase;
    public Material InPositionMediumMaterialCleaningPhase;
    public Material OutOfPositionMediumMaterialCleaningPhase;
    public Material InPositionHeavyMaterialCleaningPhase;
    public Material OutOfPositionHeavyMaterialCleaningPhase;

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
        CleaningPhaseBanner.color = Color.red;
        StartCoroutine(FlashScoreColor());
    }

    public void ComputeFinalScore()     // TODO: Apply some logic here
    {
        GameObject[] interactibleGameObjects = GameObject.FindGameObjectsWithTag("Interactible");

        CleaningPhaseBanner.color = Color.black;

        string gameOverMessage = "GAME OVER.";

        float initialScore = totalScore;

        foreach (GameObject interactibleGameObject in interactibleGameObjects)
        {
            if (interactibleGameObject.GetComponent<GenericObject>() != null && (interactibleGameObject.GetComponent<GenericObject>().IsOutOfPosition() || interactibleGameObject.GetComponent<GenericObject>().IsDisoriented()))
            {
                totalScore -= 50000;    // TODO: Check this value!
                UpdateScore();
            }
        }

        Debug.Log("Initial score: " + initialScore);
        Debug.Log("Final score: " + totalScore);

        if (totalScore < 0)
        {
            gameOverMessage += "\nYOU ARE GROUNDED.";
            CleaningPhaseBanner.color = Color.red;
        }
        else
        {
            gameOverMessage += "\nYOU ARE SAFE!";
            CleaningPhaseBanner.color = Color.cyan;
        }

        CleaningPhaseBanner.text = gameOverMessage;
    }

    private IEnumerator FlashScoreColor()
    {
        scoreText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        scoreText.color = Color.black;
        if (isCleaningPhase && !isGameOver)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FlashScoreColor());
        }
    }
}

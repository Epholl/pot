using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/// <summary>
/// This class Resolves the logic at the end of the game. It calculates the score information and sends them to the server instance.
/// </summary>
public class GameLogic : MonoBehaviour {

	public void GameEnded()
    {
        string scoreString = GameObject.Find("ScoreText").GetComponent<Text>().text;
        int score = int.Parse(scoreString);

        string name = GameObject.Find("InputField").GetComponentInChildren<Text>().text;

        try
        {
            // Actively use the wcf client
            GetComponent<WCFClient>().client.SaveHighscore(name, "First Level", score);
        }
        catch (Exception e)
        {
            // We will fail silently.
        }

        // Restart the level
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1f;
    }
}

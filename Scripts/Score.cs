using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TMP_InputField inputUser;
    public Leaderboard leaderboard;

    public void saveScore()
    {

        float userScore = timer.time;

        string username = inputUser.text;

        if (username.Length < 3)
        {
            inputUser.text = "Please Enter A Name with at least 3 characters";

            return;
        }

        PlayerPrefs.SetString("CurrentPlayerName", username);
        PlayerPrefs.SetFloat("CurrentPlayerScore", userScore);

        SceneManager.LoadScene("Leaderboard");
    
    }
}

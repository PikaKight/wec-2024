using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public static void returnMenu()
    {

        Time.timeScale = 1.0f;

        timer.time = 0.0f;

        Pause.isPaused = false;

        SceneManager.LoadScene("Title");
    }

    public static void resumeGame(){
        Pause.isPaused = false;
    }

    public static void toLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}

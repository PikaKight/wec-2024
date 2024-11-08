using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public static void restartGame()
    {
        Time.timeScale = 1.0f;

        timer.time = 0.0f;

        Pause.isPaused = false;

        SceneManager.LoadScene("Main");
    }
}

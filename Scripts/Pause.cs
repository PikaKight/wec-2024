using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject menu;
    

    // Update is called once per frame
    void Update()
    {

        if (isPaused)
        {

            menu.SetActive(true);
            Time.timeScale = 0.0f;

        }
        else if (!GameEnd.isOver)
        {
            menu.SetActive(false);
            Time.timeScale = 1.0f;

        }
    }
}

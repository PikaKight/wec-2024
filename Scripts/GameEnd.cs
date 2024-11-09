using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SocialPlatforms;

public class GameEnd : MonoBehaviour
{
    public GameObject score;
    public TMP_Text text;


    public string username;

    public static bool isOver = false;

    void Start()
    {
        score.SetActive(false);
    }

    void Update()
    {
        Debug.Log(isOver);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerControl player = other.gameObject.GetComponent<PlayerControl>();

        Debug.Log(player);

        if (player != null) {
            isOver = true;
            Time.timeScale = 0;
            score.SetActive(true);

            text.text = "Final Time: " + timer.time.ToString();
        }
        
        
    }
    
}

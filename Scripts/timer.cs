using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class timer : MonoBehaviour
{
    public static float time = 0;

    public TextMeshProUGUI timeText;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        timeText.text = "Time: " + time.ToString("F2") + "s";
    }
}

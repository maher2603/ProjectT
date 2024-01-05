using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text bestLapText;
    public Text pointsText;
    public void Practice(float bestLapTime)
    {
        gameObject.SetActive(true);
        bestLapText.text = $"Best Lap Time: {Mathf.FloorToInt(bestLapTime / 60)}:{bestLapTime % 60:00.00}";
    }

    public void Drift(float points) 
    {
        gameObject.SetActive(true);
        pointsText.text = points + "Points";
    }
}


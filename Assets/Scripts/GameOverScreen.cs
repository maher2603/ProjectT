using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text bestLapText;
    public void Practice(float bestLapTime)
    {
        gameObject.SetActive(true);
        bestLapText.text = $"Best Lap Time: {Mathf.FloorToInt(bestLapTime / 60)}:{bestLapTime % 60:00.00}";
    }
}


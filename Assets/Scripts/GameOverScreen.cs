using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text bestLapText;
    public Text conesText;
    public Text caughtText;
    public void Practice(float bestLapTime)
    {
        gameObject.SetActive(true);
        bestLapText.text = $"Best Lap Time: {Mathf.FloorToInt(bestLapTime / 60)}:{bestLapTime % 60:00.00}";
    }

    public void Drift(float cones, bool caught) 
    {
        gameObject.SetActive(true);
        if (caught)
        {
            caughtText.text = "YOU WERE CAPTURED BY THE POLICE!";
            conesText.text = cones + " Cones Were Confiscated";
        }
        else
        {
            caughtText.text = "";
            conesText.text = cones + " Cones Successfully Collected";
        }
    }
}


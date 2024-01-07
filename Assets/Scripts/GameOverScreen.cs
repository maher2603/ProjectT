using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Text bestLapText;
    public Text conesText;
    public Text caughtText;
    public Text positionText;
    public void Practice(float bestLapTime, float position)
    {
        gameObject.SetActive(true);
        bestLapText.text = $"Best Lap Time: {Mathf.FloorToInt(bestLapTime / 60)}:{bestLapTime % 60:00.00}";
        if (position == 1)
        {
            positionText.text = "1st Place";
            positionText.color = Color.green;
        }
        else if (position == 2)
        {
            positionText.text = "2nd Place";
            positionText.color = new Color(1f, 0.647f, 0f);
        }
        else if (position == 3)
        {
            positionText.text = "3rd Place";
            positionText.color = Color.red;
        }
        else
        {
            positionText.text = "";
        }

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


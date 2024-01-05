using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConeCollection : MonoBehaviour
{
    private int Cone = 0;
    public Text coneText;
    public Text countdown;
    private float countdownTime = 180f; 

    public GameOverScreen GameOverScreen;
    public RemoveUI RemoveUI;

    private void Start()
    {
        UpdateCountdownText();
        StartCoroutine(StartCountdown());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Cone"))
        {
            Cone++;
            coneText.text = "Cones Collected: " + Cone.ToString();
            Debug.Log(Cone);
            other.gameObject.SetActive(false);
            StartCoroutine(ReactivateCone(other.gameObject, 20f));
        }
    }

    private IEnumerator ReactivateCone(GameObject coneObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        coneObject.SetActive(true);
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(5f);
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownTime--;
            UpdateCountdownText();
        }
        GameOver(Cone);
    }

    private void UpdateCountdownText()
    {
        string minutes = Mathf.Floor(countdownTime / 60).ToString("00");
        string seconds = (countdownTime % 60).ToString("00");
        countdown.text = "Time Remaining: " + minutes + ":" + seconds;
    }

    public void GameOver(float cones)
    {
        GameOverScreen.Drift(cones);
        RemoveUI.Setup();
    }
}
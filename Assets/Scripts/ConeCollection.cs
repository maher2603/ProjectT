using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ConeCollection : MonoBehaviour
{
    private int Cone = 0;
    public Text coneText;
    public Text countdown;
    private float countdownTime = 180f;
    private bool caught = true;
    public GameObject policeCar;

    public GameOverScreen GameOverScreen;
    public RemoveUI RemoveUI;

    [Header("Audio")]
    public AudioSource endSound;
    public AudioSource gameSong;

    private void EndRace()
    {
        endSound.Play();
        gameSong.Stop();
        print("Finished");
    }

    private void Start()
    {
        UpdateCountdownText();
        StartCoroutine(StartCountdown());
        StartCoroutine(StartGameSongDelayed());
    }

    private IEnumerator StartGameSongDelayed()
    {
        yield return new WaitForSeconds(5f);
        gameSong.Play();
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
        if (other.transform.CompareTag("Police"))
        {
            gameObject.SetActive(false);
            GameOver(Cone, caught);
            EndRace();
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
        GameOver(Cone, !caught);
        EndRace();
        policeCar.SetActive(false);
    }

    private void UpdateCountdownText()
    {
        string minutes = Mathf.Floor(countdownTime / 60).ToString("00");
        string seconds = (countdownTime % 60).ToString("00");
        countdown.text = "Time Remaining: " + minutes + ":" + seconds;
    }

    public void GameOver(float cones, bool caught)
    {
        GameOverScreen.Drift(cones, caught);
        RemoveUI.Setup();
    }
}
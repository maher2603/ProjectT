using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 5f;
    float goTextDuration = 1f;
    public AudioSource beep;
    public AudioSource goSound;

    [SerializeField] Text countdownText;
    [SerializeField] Color redColor = Color.red;
    [SerializeField] Color yellowColor = Color.yellow;
    [SerializeField] Color greenColor = Color.green;
    [SerializeField] Color outlineColor = Color.white;
    [SerializeField] float outlineSize = 1f;

    void Start()
    {
        currentTime = startingTime;
        StartCoroutine(UpdateCountdown());
    }

    IEnumerator UpdateCountdown()
    {
        // Add a shadow component for the white outline
        Shadow shadow = countdownText.gameObject.GetComponent<Shadow>();
        if (shadow == null)
        {
            shadow = countdownText.gameObject.AddComponent<Shadow>();
        }
        shadow.effectColor = outlineColor;
        shadow.effectDistance = new Vector2(outlineSize, -outlineSize);

        // Display the countdown
        while (currentTime > 0.5)
        {
            currentTime -= Time.deltaTime;

            // Set colors based on countdown value
            if (currentTime >= 3.5)
            {
                countdownText.color = redColor;
            }
            else
            {
                countdownText.color = yellowColor;
            }

            // Play the beep sound every time the countdown increments
            if (Mathf.Floor(currentTime) != Mathf.Floor(currentTime + Time.deltaTime))
            {
                beep.Play();
            }

            countdownText.text = Mathf.Ceil(currentTime).ToString("0");
            yield return null;
        }

        // Play the "GO!" sound
        goSound.Play();

        // Display "GO!" and wait for the specified duration
        countdownText.color = greenColor;
        countdownText.text = "GO!";
        yield return new WaitForSeconds(goTextDuration);

        // Hide the text
        countdownText.text = "";

        // Remove the shadow component after the countdown
        Destroy(shadow);
    }
}

// Code adapted from YouTube example
// https://www.youtube.com/watch?v=w9lSZPmget4&ab_channel=unknown1050
// source: https://github.com/TheUnknown1050/Checkpoint-Tutorial retrieved in October 2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointsandLaps : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public RemoveUI RemoveUI;

    [Header("Checkpoints")]
    public GameObject start;
    public GameObject end;
    public GameObject[] checkpoints;

    [Header("Settings")]
    public float laps;
    public Text currentlaptext;
    public Text bestlaptext;
    public Text totallaptext;

    [Header("Audio")]
    public AudioSource endSound;
    public AudioSource gameSong;

    [Header("Information")]
    private float currentCheckpoint;
    private float currentLap;
    private bool started;
    private bool finished;
    public float carPosition;

    private float currentLapTime;
    private float bestLapTime;
    private float bestLap;
    private float totalLapTime;

    private void EndRace()
    {
        endSound.Play();
        gameSong.Stop();
        print("Finished");
    }

    public void GameOver(float bestLapTime, float position) {
        GameOverScreen.Practice(bestLapTime, position);
        RemoveUI.Setup();
    }


    private void Start()
    {
        currentCheckpoint = 0;
        currentLap = 1;

        started = false;
        finished = false;

        currentLapTime = 0;
        bestLapTime = 0;
        bestLap = 0;

        StartCoroutine(StartGameSongDelayed());
    }

    private IEnumerator StartGameSongDelayed()
    {
        yield return new WaitForSeconds(5f);
        gameSong.Play();
    }

    private void Update()
    {
        if (started && !finished)
        {
            currentLapTime += Time.deltaTime;
            

            if (bestLap == 0)
            {
                bestLap = 1;
            }
        }

        if (started)
        {
            totalLapTime += currentLapTime;
            if (bestLap == currentLap)
            {
                bestLapTime = currentLapTime;
            }
        }

        if (finished)
        {
            carPosition = (GetComponent<CarPositionManager>() != null) ? GetComponent<CarPositionManager>().CarPosition : 0;
            GameOver(bestLapTime, carPosition);
        }
        
        currentlaptext.text = $"Current: {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.00} - (Lap {currentLap})";
        bestlaptext.text = $"Best: {Mathf.FloorToInt(bestLapTime / 60)}:{bestLapTime % 60:00.00} - (Lap {bestLap})";
        totallaptext.text = "Lap "+ currentLap.ToString() + "/" +laps.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;

            // Started the race
            if (thisCheckpoint == start && !started)
            {
                print("Started");
                started = true;
            }
            // Ended the lap or race
            else if (thisCheckpoint == end && started)
            {
                // If all the laps are finished, end the race
                if (currentLap == laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        if (currentLapTime < bestLapTime)
                        {
                            bestLap = currentLap;
                        }

                        finished = true;
                        EndRace();
                    }
                    else
                    {
                        print("Did not go through all checkpoints");
                    }
                }
                // If all laps are not finished, start a new lap
                else if (currentLap < laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        if (currentLapTime < bestLapTime)
                        {
                            bestLap = currentLap;
                            bestLapTime = currentLapTime; // Because the update function has already run this frame, we need to add this line or it won't work
                        }

                        currentLap++;
                        currentCheckpoint = 0;
                        currentLapTime = 0;
                        print($"Started lap {currentLap}");
                    }
                    else
                    {
                        print("Did not go through all checkpoints");
                    }
                }
            }

            // Loop through the checkpoints to compare and check which one the player touched
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (finished)
                    return;

                // If the checkpoint is correct
                if (thisCheckpoint == checkpoints[i] && i + 1 == currentCheckpoint + 1)
                {
                    print($"Correct Checkpoint: {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.000}");
                    currentCheckpoint++;
                }
                // If the checkpoint is incorrect
                else if (thisCheckpoint == checkpoints[i] && i + 1 != currentCheckpoint + 1)
                {
                    print($"Incorrect checkpoint");
                }
            }
        }
    }


}

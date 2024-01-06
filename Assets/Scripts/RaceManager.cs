using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour
{
    public GameObject Pt;
    public GameObject PositionTrackerHolder;

    public GameObject[] Cars;
    public Transform[] PositionTrackerPositions;
    public GameObject[] PositionTrackerForEachCar;

    private int totalCars;
    private int totalPositonTrackers;

    public Text PositionText;
    // Start is called before the first frame update
    void Start()
    {
        totalCars = Cars.Length;
        totalPositonTrackers = PositionTrackerHolder.transform.childCount;
        setPositionTrackers();
        setCarPosition();
    }

    void setPositionTrackers()
    {
        PositionTrackerPositions = new Transform[totalPositonTrackers];
        for (int i = 0; i < totalPositonTrackers; i++)
        {
            PositionTrackerPositions[i] = PositionTrackerHolder.transform.GetChild(i).transform;
        }
        PositionTrackerForEachCar = new GameObject[totalCars];
        for (int i = 0; i < totalCars; i++)
        {
            PositionTrackerForEachCar[i] = Instantiate(Pt, PositionTrackerPositions[0].position, PositionTrackerPositions[0].rotation);
            PositionTrackerForEachCar[i].name = "PT" + i;
            PositionTrackerForEachCar[i].layer = 7 + i;
        }
    }

    void setCarPosition()
    {
        for (int i = 0; i < totalCars; i++)
        {
            Cars[i].GetComponent<CarPositionManager>().CarPosition = i + 1;
            Cars[i].GetComponent<CarPositionManager>().CarNumber = i;
        }

        PositionText.text = "Position:" + Cars[0].GetComponent<CarPositionManager>().CarPosition + "/" + totalCars;
    }

    public void CarCollectedPt(int carNumber, int ptnumber)
    {
        PositionTrackerForEachCar[carNumber].transform.position = PositionTrackerPositions[ptnumber].transform.position;
        PositionTrackerForEachCar[carNumber].transform.rotation = PositionTrackerPositions[ptnumber].transform.rotation;
        comparePositions(carNumber);
    }

    void comparePositions(int carNumber)
    {
        if (Cars[carNumber].GetComponent<CarPositionManager>().CarPosition > 1)
        {
            GameObject currentCar = Cars[carNumber];
            int currentCarPos = currentCar.GetComponent<CarPositionManager>().CarPosition;
            int currentCarPt = currentCar.GetComponent<CarPositionManager>().ptCrossed;

            GameObject carInFront = null;
            int carInFrontPos = 0;
            int carInFrontPt = 0;

            for (int i = 0; i < totalCars; i++)
            {
                if (Cars[i].GetComponent<CarPositionManager>().CarPosition == currentCarPos - 1)
                {
                    carInFront = Cars[i];
                    carInFrontPt = carInFront.GetComponent<CarPositionManager>().ptCrossed;
                    carInFrontPos = carInFront.GetComponent<CarPositionManager>().CarPosition;
                    break;
                }
            }
            if (currentCarPt > carInFrontPt)
            {
                currentCar.GetComponent<CarPositionManager>().CarPosition = currentCarPos - 1;
                carInFront.GetComponent<CarPositionManager>().CarPosition = carInFrontPos + 1;

                Debug.Log("Car " + carNumber + " has overtaken " + carInFront.GetComponent<CarPositionManager>().CarNumber);
            }
            PositionText.text = "Position:" + Cars[0].GetComponent<CarPositionManager>().CarPosition + "/" + totalCars;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPositionManager : MonoBehaviour
{
    public int CarNumber;
    public int ptCrossed = 0;
    public int CarPosition;
    public RaceManager raceManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CP"))
        {
            ptCrossed += 1;
            raceManager.CarCollectedPt(CarNumber, ptCrossed);
        }
    }
}
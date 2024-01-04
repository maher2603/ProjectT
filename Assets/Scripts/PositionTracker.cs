using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    public List<Transform> PositionTrackers;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform tr in gameObject.GetComponentsInChildren<Transform>())
        {
            PositionTrackers.Add(tr);
        }
        PositionTrackers.Remove(PositionTrackers[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrakingZoneContainer : MonoBehaviour
{
    public List<Transform> AIBrakingZones;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform tr in gameObject.GetComponentsInChildren<Transform>())
        {
            AIBrakingZones.Add(tr);
        }
        AIBrakingZones.Remove(AIBrakingZones[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

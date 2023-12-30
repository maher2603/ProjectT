using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICheckpointContainer : MonoBehaviour
{
    public List<Transform> AICheckpoints;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Transform tr in gameObject.GetComponentsInChildren<Transform>())
        {
            AICheckpoints.Add(tr);
        }
        AICheckpoints.Remove(AICheckpoints[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

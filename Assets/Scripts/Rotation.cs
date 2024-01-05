using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Update()
    {
        if (transform.CompareTag("Cone"))
        {
            transform.Rotate(0, .5f, 0);
        }
        else
        {
            transform.Rotate(0, .05f, 0);
        }
    }
}

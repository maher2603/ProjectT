using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Update()
    {

        transform.Rotate(0, .05f, 0);
    }
}

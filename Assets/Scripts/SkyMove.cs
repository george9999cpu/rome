using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class SkyMove : MonoBehaviour
{
    public float speed = 0.2f;   // small steady drift

    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
    }
}


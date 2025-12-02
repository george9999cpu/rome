using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sea : MonoBehaviour
{
    public float amplitude = 0.1f; 
    public float frequency = 1f;   
    float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}


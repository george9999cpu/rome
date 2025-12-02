using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 5f;     // smooth move 

    private Transform target;          // the ship we follow

    void Start()
    {
        // find the ship by tag
        GameObject ship = GameObject.FindGameObjectWithTag("Ship");

        if (ship != null)
        {
            target = ship.transform;   // set it as target
        }
        else
        {
            Debug.LogError("no object with tag Ship found");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;  // nothing to follow

        // only follow x so camera stays stable
        Vector3 newPos = new Vector3(
            target.position.x,
            transform.position.y,
            transform.position.z
        );

        // move the camera smoothly to new position
        transform.position = Vector3.Lerp(
            transform.position,
            newPos,
            smoothSpeed * Time.deltaTime
        );
    }
}


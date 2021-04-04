using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : MonoBehaviour
{
    [SerializeField] private SO_ObstacleSettings obstacleSettings;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        RotateObject();
    }
    private void RotateObject()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, 0, obstacleSettings.MillRotationSpeed) * Time.fixedDeltaTime));
    }


}

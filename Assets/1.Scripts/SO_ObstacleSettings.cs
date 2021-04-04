using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CrazyRunner/ObstacleSettings")]
public class SO_ObstacleSettings : ScriptableObject
{
    [SerializeField] private float _BoxMovementSpeed;
    public float BoxMovementSpeed { get { return _BoxMovementSpeed; } set { _BoxMovementSpeed = value; } }

    [SerializeField] private float _MillRotationSpeed;
    public float MillRotationSpeed { get { return _MillRotationSpeed; } set { _MillRotationSpeed = value; } }

    [SerializeField] private float _GroundRotationSpeed;
    public float GroundRotationSpeed { get { return _GroundRotationSpeed; } set { _GroundRotationSpeed = value; } }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CrazyRunner/PlayerSettings")]
public class SO_PlayerSettings : ScriptableObject
{
    [SerializeField] private float _MovementSpeed;
    public float MovementSpeed { get { return _MovementSpeed; } set { _MovementSpeed = value; } }

    [SerializeField] private float _LeftRightSpeed;
    public float LeftRightSpeed { get { return _LeftRightSpeed; } set { _LeftRightSpeed = value; } }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CrazyRunner/CameraSettings")]
public class SO_CameraSettings : ScriptableObject
{
    [SerializeField] private Vector3 _CamPositionOffset;
    public Vector3 CamPositionOffset { get { return _CamPositionOffset; } set { _CamPositionOffset = value; } }

    [SerializeField] private Vector3 _CamRotationOffset;
    public Vector3 CamRotationOffset { get { return _CamRotationOffset; } set { _CamRotationOffset = value; } }
}

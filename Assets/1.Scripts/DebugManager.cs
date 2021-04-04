using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    private PlayerData userData;

    [SerializeField] private SO_CameraSettings cameraSettings;
    [SerializeField] private SO_PlayerSettings playerSettings;
    [SerializeField] private SO_ObstacleSettings obstacleSettings;

    [Space(20)]
    [SerializeField] private TextMeshProUGUI playerForwardSpeedText;
    [SerializeField] private TextMeshProUGUI playerLeftRightSpeedText;

    [Space(20)]
    [SerializeField] private TextMeshProUGUI cameraPosXText;
    [SerializeField] private TextMeshProUGUI cameraPosYText;
    [SerializeField] private TextMeshProUGUI cameraPosZText;

    [Space(20)]
    [SerializeField] private TextMeshProUGUI cameraRotXText;
    [SerializeField] private TextMeshProUGUI cameraRotYText;
    [SerializeField] private TextMeshProUGUI cameraRotZText;

    [Space(20)]
    [SerializeField] private TextMeshProUGUI millRotationText;
    [SerializeField] private TextMeshProUGUI boxMovementText;
    [SerializeField] private TextMeshProUGUI groundedRotationText;

    private Vector3 tempVector;

    private void Awake()
    {
        userData = saveLoad.LoadPlayer();
        LoadData();
    }

    private void Start()
    {
        
        SetTextData();
    }

    private void LoadData()
    {
        playerSettings.MovementSpeed = userData.forwardSpeed;
        playerSettings.LeftRightSpeed = userData.sideSpeed;

        cameraSettings.CamPositionOffset = new Vector3(userData.cameraPosX, userData.cameraPosY, userData.cameraPosZ);
        cameraSettings.CamRotationOffset = new Vector3(userData.cameraRotX, userData.cameraRotY, userData.cameraRotZ);

        obstacleSettings.MillRotationSpeed = userData.millRotationSpeed;
        obstacleSettings.BoxMovementSpeed = userData.boxMovementSpeed;
        obstacleSettings.GroundRotationSpeed = userData.groundedRotationSpeed;
    }

    private void SetTextData()
    {
        playerForwardSpeedText.text = playerSettings.MovementSpeed.ToString();
        playerLeftRightSpeedText.text = playerSettings.LeftRightSpeed.ToString();

        cameraPosXText.text = cameraSettings.CamPositionOffset.x.ToString();
        cameraPosYText.text = cameraSettings.CamPositionOffset.y.ToString();
        cameraPosZText.text = cameraSettings.CamPositionOffset.z.ToString();

        cameraRotXText.text = cameraSettings.CamRotationOffset.x.ToString();
        cameraRotYText.text = cameraSettings.CamRotationOffset.y.ToString();
        cameraRotZText.text = cameraSettings.CamRotationOffset.z.ToString();

        millRotationText.text = obstacleSettings.MillRotationSpeed.ToString();
        boxMovementText.text = obstacleSettings.BoxMovementSpeed.ToString();
        groundedRotationText.text=obstacleSettings.GroundRotationSpeed.ToString();

        saveLoad.SavePlayer(userData);
    }

    public void ResetMillRotationSpeed()
    {
        userData.millRotationSpeed = obstacleSettings.MillRotationSpeed = 50;
        SetTextData();
    }
    public void ResetGroundedRotationSpeed()
    {
        userData.groundedRotationSpeed = obstacleSettings.GroundRotationSpeed = 50;
        SetTextData();
    }

    public void ResetBoxMovementSpeed()
    {
        userData.boxMovementSpeed = obstacleSettings.BoxMovementSpeed = 0.5f;
        SetTextData();
    }

    public void SetBoxMovementSpeed(float value)
    {
        userData.boxMovementSpeed += value;
        obstacleSettings.BoxMovementSpeed = userData.boxMovementSpeed;
        SetTextData();
    }

    public void SetMillRotationSpeed(float value)
    {
        userData.millRotationSpeed += value;
        obstacleSettings.MillRotationSpeed = userData.millRotationSpeed;
        SetTextData();
    }

    public void SetGroundedRotationSpeed(float value)
    {
        userData.groundedRotationSpeed += value;
        obstacleSettings.GroundRotationSpeed = userData.groundedRotationSpeed;
        SetTextData();
    }

    public void ResetForwardSpeed()
    {
        userData.forwardSpeed = playerSettings.MovementSpeed = 10;
        SetTextData();
    }

    public void ResetLeftRightSpeed()
    {
        userData.sideSpeed = playerSettings.LeftRightSpeed = 0.5f;
        SetTextData();
    }

    public void ResetCameraPosOffset()
    {
        userData.cameraPosX = 0;
        userData.cameraPosY = 9;
        userData.cameraPosZ = -12;
        cameraSettings.CamPositionOffset = new Vector3(0,9,-12);
        SetTextData();
    }

    public void ResetCameraRotationOffset()
    {
        userData.cameraRotX = 20;
        userData.cameraRotY = 0;
        userData.cameraRotZ = 0;
        cameraSettings.CamRotationOffset = new Vector3(20, 0, 0);
        SetTextData();
    }

    public void SetPlayerForwardSpeed(float value)
    {
        userData.forwardSpeed += value;
        playerSettings.MovementSpeed = userData.forwardSpeed;
        SetTextData();
    }

    public void SetPlayerLRSpeed(float value)
    {
        userData.sideSpeed += value;
        playerSettings.LeftRightSpeed = userData.sideSpeed;
        SetTextData();
    }

    public void SetCamPosX(float value)
    {
        tempVector = cameraSettings.CamPositionOffset;

        userData.cameraPosX += value;
        tempVector.x = userData.cameraPosX;

        cameraSettings.CamPositionOffset = tempVector;
        SetTextData();
    }
    public void SetCamPosY(float value)
    {
        tempVector = cameraSettings.CamPositionOffset;

        userData.cameraPosY += value;
        tempVector.y = userData.cameraPosY;

        cameraSettings.CamPositionOffset = tempVector;
        SetTextData();
    }
    public void SetCamPosZ(float value)
    {
        tempVector = cameraSettings.CamPositionOffset;

        userData.cameraPosZ += value;
        tempVector.z = userData.cameraPosZ;

        cameraSettings.CamPositionOffset = tempVector;
        SetTextData();
    }
    public void SetCamRotX(float value)
    {
        tempVector = cameraSettings.CamRotationOffset;

        userData.cameraRotX += value;
        tempVector.x = userData.cameraRotX;

        cameraSettings.CamRotationOffset = tempVector;
        SetTextData();
    }
    public void SetCamRotY(float value)
    {
        tempVector = cameraSettings.CamRotationOffset;

        userData.cameraRotY += value;
        tempVector.y = userData.cameraRotY;

        cameraSettings.CamRotationOffset = tempVector;
        SetTextData();
    }
    public void SetCamRotZ(float value)
    {
        tempVector = cameraSettings.CamRotationOffset;

        userData.cameraRotZ += value;
        tempVector.z = userData.cameraRotZ;

        cameraSettings.CamRotationOffset = tempVector;
        SetTextData();
    }
}

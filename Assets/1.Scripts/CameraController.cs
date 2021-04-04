using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private SO_CameraSettings cameraSettings;

    private GameObject player;
    private Vector3 desiredPosition=Vector3.zero;


    public static CameraController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.position = player.transform.position + cameraSettings.CamPositionOffset;
        //Debug.Log(transform.position + " / " + lookAt.position);
    }

    private void LateUpdate()
    {
        desiredPosition = player.transform.position + cameraSettings.CamPositionOffset;
        desiredPosition.x = cameraSettings.CamPositionOffset.x;
        transform.position = desiredPosition;

        transform.rotation = Quaternion.Euler(cameraSettings.CamRotationOffset);
    }

}

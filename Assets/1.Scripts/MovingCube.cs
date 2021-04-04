using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private SO_ObstacleSettings obstacleSettings;

    private Rigidbody rb;

    private bool isGoingLeft = false;
    private float ratio = 0;

    private Vector3 startPosition;
    private Vector3 stopPosition;



    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();

        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        stopPosition = new Vector3(transform.position.x*-1, transform.position.y, transform.position.z);

    }

    private void FixedUpdate()
    {
        ratio += Time.fixedDeltaTime * obstacleSettings.BoxMovementSpeed;
        ratio = Mathf.Clamp(ratio, 0f, 1f);

        rb.MovePosition(Vector3.Lerp(startPosition, stopPosition, ratio));

        if (ratio == 1)
        {
            ChangeMovementWay();
            ratio = 0;
        }

    }

    private void ChangeMovementWay()
    {
        stopPosition.x = startPosition.x;
        startPosition.x = startPosition.x * -1;
    }

    //private void OnEnable()
    //{
    //    transform.localPosition = new Vector3(-3, transform.localPosition.y, transform.localPosition.z);
    //}
}

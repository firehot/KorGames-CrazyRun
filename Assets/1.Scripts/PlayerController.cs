using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private SO_PlayerSettings playerSettings; 

    private MobileInput touchInput;
    private float maxLeftRightMoveDistance = 3f;

    private bool isDead = false;
    private Vector3 movePosition;

    private Animator myAnimator;
    private CharacterController playerController;

    private float levelLength;
    private float finalMovePoint;

    public static PlayerController instance;
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

    private void Start()
    {
        movePosition = transform.position;
        playerController = GetComponent<CharacterController>();
        myAnimator = GetComponent<Animator>();
        touchInput = GetComponent<MobileInput>();

        levelLength = LevelManager.instance.GetLevelLength();        

    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isGameStarted)
            return;

        if (isDead)
            return;

        if (transform.position.z >= levelLength)
        {
            Debug.Log("Level Completed please move in the middle");
            finalMovePoint = LevelManager.instance.GetFinalPlatformMiddlePosition();

            if (transform.position.z<finalMovePoint)
            {
                //movePosition = Vector3.zero;
                //movePosition.x = 0;
                //movePosition.y = 0;
                //movePosition.z = movementSpeed/2;
                //playerController.Move(movePosition * Time.deltaTime);


            }
            else
            {
                myAnimator.SetBool("Win", true);
                LevelManager.instance.EnableEndGameDecorations();
                return;
            }
            
        }

        movePosition = Vector3.zero;
        movePosition.x = 0;
        movePosition.y = 0;
        movePosition.z = playerSettings.MovementSpeed;


        movePosition.x = touchInput.SwipeDelta.x * playerSettings.LeftRightSpeed;

        playerController.Move(movePosition * Time.deltaTime);

        if (transform.position.x < -maxLeftRightMoveDistance)
        {
            transform.position = new Vector3(-maxLeftRightMoveDistance, transform.position.y, transform.position.z);
        }
        if (transform.position.x > maxLeftRightMoveDistance)
        {
            transform.position = new Vector3(maxLeftRightMoveDistance, transform.position.y, transform.position.z);
        }
    }

    public void StartRunning()
    {
        myAnimator.SetBool("Running", true);
    }

    public void SetFinalMovePoint(float finalPosition)
    {
        finalMovePoint = finalPosition;
    }

    private void Die()
    {
        isDead = true;
        CameraShake.instance.Shake();
        myAnimator.SetBool("isDead", true);
        GameManager.instance.EnableFailedCanvas();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.isGameStarted && other.gameObject.CompareTag("Obstacle") & !isDead)
        {
            Die();
        }
    }

    public void Reset()
    {
        isDead = false;
        transform.position = Vector3.zero;
        myAnimator.SetBool("Win", false);
        myAnimator.SetBool("isDead", false);
        myAnimator.SetBool("Running", false);
    }

}

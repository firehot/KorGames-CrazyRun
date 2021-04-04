using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static MobileInput Instance { set; get; }

    private Vector2 swipeDelta, firstTouch, lastTouch;

    public Vector2 SwipeDelta { get { return swipeDelta; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //check input

        #region StandAlone Inputs
        if (Input.GetMouseButtonDown(0))
        {            
            firstTouch = lastTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            firstTouch = lastTouch = swipeDelta = Vector2.zero;
        }

        #endregion


        #region Mobile Inputs
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                firstTouch = lastTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                firstTouch = lastTouch = swipeDelta = Vector2.zero;

            }
        }
        #endregion

        ////calculate distance
        swipeDelta = Vector2.zero;

        if (firstTouch != Vector2.zero)
        {
            //Check with mobile
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - firstTouch;
                firstTouch = Input.touches[0].position;
            }//Check with standalone
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - firstTouch;
                firstTouch = (Vector2)Input.mousePosition;
            }
        }

        //Debug.Log(swipeDelta.x);

    }


}

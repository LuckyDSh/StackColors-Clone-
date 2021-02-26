/*
*	TickLuck
*	All rights reserved
*/
using System;
using UnityEngine;

public class PlayerStackColorController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Color _color;
    [SerializeField] private Renderer[] _renderers;

    [SerializeField] public static bool is_Playing;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float moveSideWaysLerp;

    private Rigidbody rb;

    private Transform parentPickUp;
    public static Transform ParentPickUp_buffer;
    [SerializeField] private Transform stackPosition;

    public static bool atEnd;
    [SerializeField] private GameObject forceAmountBar;
    [SerializeField] public static float forwardForce; // static
    [SerializeField] private float forceAdder;
    [SerializeField] private float forceReducer;

    public static Color PlayerColor_buffer;
    public static Action<float> Kick;
    #endregion

    #region UnityMethods
    void Start()
    {
        atEnd = false;
        forceAmountBar.SetActive(false);
        int rand = UnityEngine.Random.Range(0, ColorController.s_pickUpColors_Buffer.Count);
        rb = GetComponent<Rigidbody>();
        SetColor(ColorController.s_pickUpColors_Buffer[rand].Color);
    }

    void Update()
    {
        PlayerColor_buffer = _color;
        ParentPickUp_buffer = parentPickUp;

        if (is_Playing)
            MoveForward();

        if (atEnd)
        {
            forwardForce -= forceReducer * Time.deltaTime;
            if (forwardForce < 0)
                forwardForce = 0;
        }

        #region PC Control
        if (Input.GetMouseButtonDown(0))
            if (atEnd)
                forwardForce += forceAdder;

        if (Input.GetMouseButton(0)) 
        {
            if (atEnd)
                return;

            if (is_Playing == false)
                is_Playing = true;
            MoveSideWays_usingMouse();
        }
        #endregion

        #region Phone Control
        if (Input.touchCount > 0)
            if (atEnd)
                forwardForce += forceAdder;

        if (Input.touchCount > 0)
        {
            if (atEnd)
                return;

            if (is_Playing == false)
                is_Playing = true;
            MoveSideWays_usingTouch();
        }
        #endregion
    }
    #endregion

    private void MoveSideWays_usingTouch()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(hitInfo.point.x, transform.position.y, transform.position.z), moveSideWaysLerp * Time.deltaTime);
        }
    }

    private void MoveSideWays_usingMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(hitInfo.point.x, transform.position.y, transform.position.z), moveSideWaysLerp * Time.deltaTime);
        }
    }

    private void MoveForward()
    {
        rb.velocity = Vector3.forward * forwardSpeed;
    }

    private void SetColor(Color color)
    {
        _color = color;
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.SetColor("_Color", _color);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        #region FINISH ENTERED
        if (other.tag == "FinishLineStart")
        {
            forceAmountBar.SetActive(true);
            atEnd = true;
        }
        if (other.tag == "FinishLineEnd")
        {
            rb.velocity = Vector3.zero;
            is_Playing = false;
            forceAmountBar.SetActive(false);
            LaunchStack();
        }
        #endregion

        if (atEnd)
            return;

        if (other.tag == "Pickup")
        {
            Transform otherTransform = other.transform;

            if (other.transform.parent != null)
                otherTransform = other.transform.parent;

            if (_color == other.GetComponent<Pickup_Controller>().PickUp_color)
                GAME_CONTROLLER.instance.UpdateScore(otherTransform.GetComponent<Pickup_Controller>().value_buffer);
            else
            {
                #region WRONG PICKUP CHOSEN
                GAME_CONTROLLER.instance.UpdateScore(otherTransform.GetComponent<Pickup_Controller>().value_buffer * -1);
                Destroy(other.gameObject);
                if (parentPickUp != null)
                {
                    if (parentPickUp.childCount > 1)
                    {
                        parentPickUp.position -= Vector3.up * parentPickUp.GetChild(parentPickUp.childCount - 1).localScale.y;
                        Destroy(parentPickUp.GetChild(parentPickUp.childCount - 1).gameObject);
                    }
                    else
                        Destroy(parentPickUp.gameObject);
                }

                return;
                #endregion
            }

            #region SETTINGS
            Rigidbody otherRB = otherTransform.GetComponent<Rigidbody>();
            otherRB.isKinematic = true;
            other.enabled = false;
            other.gameObject.GetComponent<Pickup_Controller>().is_Picked = true;
            #endregion

            if (parentPickUp == null)
            {
                #region SET NEW PICKUP PARENT
                parentPickUp = otherTransform;
                parentPickUp.position = stackPosition.position;
                parentPickUp.parent = stackPosition;
                #endregion
            }
            else
            {
                #region PLACE PICKUP ON TOP OF PARENT
                parentPickUp.position += Vector3.up * (otherTransform.localScale.y);
                otherTransform.position = stackPosition.position;
                otherTransform.parent = parentPickUp;
                #endregion
            }

        }
    }

    private void LaunchStack()
    {
        Camera.main.GetComponent<CameraMovementController>().Target = parentPickUp;
        Kick(forwardForce);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ColorWall")
        {
            SetColor(other.GetComponent<ColorWallController>().NewColor);
        }
    }
}

/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform _target;
    public Transform Target { get { return _target; } set { _target = value; } }
    private float ZSpeed;
    //[SerializeField] private float sideWaysSpeed;
    #endregion

    #region UnityMethods
    void Start()
    {
        ZSpeed = transform.position.z - _target.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y, _target.position.z + ZSpeed);
    }
    #endregion
}

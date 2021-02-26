/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Pickup_Controller : MonoBehaviour
{
    #region Variables
    [SerializeField] private int value;
    [HideInInspector] public int value_buffer;
    [SerializeField] private Color _pickUp_color;
    private Rigidbody pickUpRB;
    private Renderer rend;
    private Collider pickUpCollider;
    /* use to mark as picked 
     * when Collides with Player*/
    public bool is_Picked = false;
    public Color PickUp_color { get { return _pickUp_color; } set { _pickUp_color = value; } }

    //private readonly Color BLUE = 
    //    ColorController.s_pickUpColors_Buffer[ColorController.s_pickUpColors_Buffer.FindIndex(i => i.Color_name == "Blue")].Color;
    //private readonly Color YELLOW =
    //   ColorController.s_pickUpColors_Buffer[ColorController.s_pickUpColors_Buffer.FindIndex(i => i.Color_name == "Yellow")].Color;
    //private readonly Color GREEN =
    //   ColorController.s_pickUpColors_Buffer[ColorController.s_pickUpColors_Buffer.FindIndex(i => i.Color_name == "Green")].Color;
    //private readonly Color ORANGE =
    //   ColorController.s_pickUpColors_Buffer[ColorController.s_pickUpColors_Buffer.FindIndex(i => i.Color_name == "Orange")].Color;
    //private readonly Color PURPLE =
    //   ColorController.s_pickUpColors_Buffer[ColorController.s_pickUpColors_Buffer.FindIndex(i => i.Color_name == "Purple")].Color;
    //private readonly Color CYAN =
    //   ColorController.s_pickUpColors_Buffer[ColorController.s_pickUpColors_Buffer.FindIndex(i => i.Color_name == "Cyan")].Color;
    #endregion

    #region UnityMethods

    private void OnEnable()
    {
        PlayerStackColorController.Kick += ThisKick;
    }
    private void OnDisable()
    {
        PlayerStackColorController.Kick -= ThisKick;
    }

    void Start()
    {
        //_pickUp_color = ColorController.s_pickUpColors_Buffer[ColorController.s_pickUpColors_Buffer.FindIndex(i => i.Color_name == "Blue")].Color;
        is_Picked = false;
        value_buffer = value;
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", _pickUp_color);
        pickUpRB = GetComponent<Rigidbody>();
        pickUpCollider = GetComponent<Collider>();

        if (transform.parent.parent != null)
            transform.parent.parent = null;
        if (transform.parent != null)
            transform.parent = null;
    }

    private void Update()
    {
        if (is_Picked)
            rend.material.SetColor("_Color", PlayerStackColorController.PlayerColor_buffer);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void ThisKick(float force)
    {
        if (is_Picked)
        {
            transform.parent = null;
            pickUpCollider.enabled = true;
            pickUpRB.isKinematic = false;
            pickUpCollider.isTrigger = false;
            pickUpRB.constraints = RigidbodyConstraints.None;
            pickUpRB.AddForce(new Vector3(0, force, force));
        }
    }
}

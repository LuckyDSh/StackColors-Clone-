/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class MultiplierController : MonoBehaviour
{
    #region Variables
    [SerializeField] private float multiplierValue;
    [SerializeField] private Color multiplierColor;
    [SerializeField] private Renderer[] renderers;
    #endregion

    #region UnityMethods
    void Start()
    {
        SetColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Pickup")
            GAME_CONTROLLER.instance.UpdateMultiplier(multiplierValue);
    }
    #endregion

    private void SetColor()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.SetColor("_Color", multiplierColor);
        }
    }
}

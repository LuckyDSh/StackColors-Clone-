/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class ColorWallController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Color newColor;
    public Color NewColor { get { return newColor; } set { newColor = value; } }
    #endregion

    #region UnityMethods
    void Start()
    {
        int rand = Random.Range(0, ColorController.s_pickUpColors_Buffer.Count);
        Color tempColor = ColorController.s_pickUpColors_Buffer[rand].Color;
        newColor = tempColor;
        tempColor.a = .5f;
        Renderer rend = transform.GetChild(0).GetComponent<Renderer>();
        rend.material.SetColor("_Color", tempColor);
    }
    #endregion
}

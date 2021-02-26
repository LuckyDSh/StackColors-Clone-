/*
*	TickLuck
*	All rights reserved
*/
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    #region Variables
    public List<CastomColor> pickUpColors;
    public static List<CastomColor> s_pickUpColors_Buffer;
    #endregion

    #region UnityMethods
    void Awake()
    {
        if (s_pickUpColors_Buffer == null)
            s_pickUpColors_Buffer = new List<CastomColor>();

        foreach (var item in pickUpColors)
        {
            s_pickUpColors_Buffer.Add(item);
        }
    }
    #endregion
}

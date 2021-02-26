/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class CollectionColorController : MonoBehaviour
{
    #region Variables
    private Pickup_Controller[] objectsToColor;
    #endregion

    #region UnityMethods
    void Awake()
    {
        objectsToColor = GetComponentsInChildren<Pickup_Controller>();
        int rand = Random.Range(0, ColorController.s_pickUpColors_Buffer.Count);
        foreach (var item in objectsToColor)
        {
            item.PickUp_color = ColorController.s_pickUpColors_Buffer[rand].Color;
        }
    }
    #endregion
}

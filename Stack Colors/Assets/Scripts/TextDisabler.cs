/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class TextDisabler : MonoBehaviour
{
    #region UnityMethods
    void Update()
    {
        if (PlayerStackColorController.is_Playing)
            gameObject.SetActive(false);
    }
	#endregion
}

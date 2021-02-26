/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class IndexSender : MonoBehaviour
{
    #region Variables
    public static int s_randIndex;
	#endregion

    #region UnityMethods
    void Start()
    {
        s_randIndex = Random.Range(0, ColorController.s_pickUpColors_Buffer.Count);
    }
	#endregion
}

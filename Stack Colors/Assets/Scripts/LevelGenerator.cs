/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject[] objects;
    #endregion

    #region UnityMethods

    public void Awake()
    {
        // Write the UNITY_EDITOR
        // Little optimization
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
         Debug.unityLogger.logEnabled = false;
#endif
    }

    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        Instantiate(objects[rand], transform.position, transform.rotation);
        Debug.Log("Ehuuu...");
    }
    #endregion
}

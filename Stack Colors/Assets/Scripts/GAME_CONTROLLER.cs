/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GAME_CONTROLLER : MonoBehaviour
{
    #region Variables
    public static GAME_CONTROLLER instance;
    [SerializeField] private GameObject GW_UI;
    [SerializeField] private GameObject HELLO_UI;
    [SerializeField] private Text score_txt;
    [SerializeField] private Text GW_Menu_score_txt;
    private int score;
    private float multiplierValue;
    private bool isFirstLaunch = true;

    private readonly int FIRST = 0;
    private readonly int SECOND = 1;
    private readonly int THIRD = 2;
    #endregion

    #region UnityMethods

    public void Start()
    {
        score = 0;
        multiplierValue = 0;
        GW_UI.SetActive(false);
    }

    void Update()
    {
        if (PlayerStackColorController.atEnd)
        {   // A bit pure optimization level (can be modified)
            if (PlayerStackColorController.ParentPickUp_buffer == null || PlayerStackColorController.ParentPickUp_buffer.GetComponent<Rigidbody>().IsSleeping())
            {
                GW_UI.SetActive(true);
                DisplayScore();
            }
        }
    }
    #endregion

    public void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    internal void UpdateMultiplier(float newValue)
    {
        if (multiplierValue >= newValue)
            return;
        multiplierValue = newValue;
        score = (int)(score * multiplierValue);
        score_txt.text = score.ToString();
    }

    public void DisplayScore()
    {
        GW_Menu_score_txt.text = score.ToString();
    }

    public void UpdateScore(int value)
    {
        score += value;
        if (score < 0)
            score = 0;
        score_txt.text = score.ToString();
    }

    public void START()
    {
        HELLO_UI.SetActive(false);
        LOAD_SECOND_SCENE();
    }

    public void LOAD_SECOND_SCENE()
    {
        SceneManager.LoadScene(SECOND);
    }

    public void RESTART()
    {
        SceneManager.LoadScene(SECOND);
    }
    public void EXIT()
    {
        Application.Quit();
    }
}

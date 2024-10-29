using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int curLv;

    //Singleton: to use methods from this class I don't need to get a component
    public static LevelManager Instance; //Capitalize cause this is a static reference to my object

    //Panels
    [Header("Panels")]
    [SerializeField] GameObject endPanel;
    [SerializeField] GameObject winPanel;

    [Tooltip("Level time in seconds")]
    [SerializeField] float levelTime = 0;
    float internalLevelTime;
    public float InternalLevelTime { get => internalLevelTime; set { if (value > 0) { internalLevelTime = value; } } } //Public setter and getter

    //PowerUps in the level
    int totalLevelPowerUps = 0;

    //PowerUps a player has at some point
    int currentPlayerPowerUps = 0;
    public int CurrentPlayerPowerUps { get => currentPlayerPowerUps; set => currentPlayerPowerUps = value; }

    //Remaining PowerUps in the level
    int remainingPowerUps = 0;
    public int RemainingPlayerPowerUps { get => remainingPowerUps; set => remainingPowerUps = value; }


    void Awake()
    {
        internalLevelTime = (float)levelTime;

        Instance = this;

        //Disable the panel
        endPanel.SetActive(false);

        //restart timeScale
        Time.timeScale = 1;
    }

    void Start()
    {
        totalLevelPowerUps = GameObject.FindGameObjectsWithTag("PowerUp").Length;
        Debug.Log(totalLevelPowerUps);

        RemainingPlayerPowerUps = totalLevelPowerUps;
    }

    void Update()
    {
        if (internalLevelTime <= 0.2f)
        {
            GameOver();
        }

        if (currentPlayerPowerUps >= totalLevelPowerUps)
        {
            GameWin();
        }

        remainingPowerUps = totalLevelPowerUps - currentPlayerPowerUps;
    }

    public void GameOver()
    {
        //Activate the panel
        endPanel.SetActive(true);

        //Set timeScale to 0 (PAUSE GAME)
        Time.timeScale = 0;
    }

    public void GameWin()
    {
        PlayerPrefs.SetInt(GameKeys.PREFS_MAXLEVEL, curLv+1);
        winPanel.SetActive(true);

        Time.timeScale = 0;
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMainMenu()
    {
        UpdateHighScore();

        GameManager.Instance.PlayerPoints = 0;
        SceneManager.LoadScene(GameKeys.SCENE_MENU);
    }

    public void UpdateHighScore()
    {
        if (PlayerPrefs.HasKey(GameKeys.PREFS_HIGHSCORE))
        {
            if (PlayerPrefs.GetInt(GameKeys.PREFS_HIGHSCORE) < GameManager.Instance.PlayerPoints)
            {
                PlayerPrefs.SetInt(GameKeys.PREFS_HIGHSCORE, GameManager.Instance.PlayerPoints);
            }
        }
        else
        {
            PlayerPrefs.SetInt(GameKeys.PREFS_HIGHSCORE, GameManager.Instance.PlayerPoints);
        }
    }
}

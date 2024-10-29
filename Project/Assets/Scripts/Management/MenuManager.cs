using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TMP_Text txHighScore;

    void Awake()
    {
        if (!PlayerPrefs.HasKey(GameKeys.PREFS_HIGHSCORE)) PlayerPrefs.SetInt(GameKeys.PREFS_HIGHSCORE, 0);
        if (!PlayerPrefs.HasKey(GameKeys.PREFS_MAXLEVEL)) PlayerPrefs.SetInt(GameKeys.PREFS_MAXLEVEL, 1);
        txHighScore.text = PlayerPrefs.GetInt(GameKeys.PREFS_HIGHSCORE).ToString("00000");
    }
}

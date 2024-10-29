using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    //Level timer
    float timer = 0;
    int minutes = 0;
    int seconds = 0;

    //Player Health
    PlayerHealth playerHealth;

    //Components
    [Header("HUD text")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI remainingPowerUpsText;

    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void Update()
    {
        LevelManager.Instance.InternalLevelTime -= Time.deltaTime;
        seconds = (int)LevelManager.Instance.InternalLevelTime % 60;
        minutes = (int)LevelManager.Instance.InternalLevelTime / 60;
    }

    void OnGUI()
    {
        timeText.text = minutes.ToString("00") + ":" +seconds.ToString("00"); //Format numbers to be, for example, 01 instead of 1
        healthText.text = playerHealth.Health.ToString("00");
        pointsText.text = GameManager.Instance.PlayerPoints.ToString("00000");
        remainingPowerUpsText.text = LevelManager.Instance.RemainingPlayerPowerUps.ToString("00");
    }
}

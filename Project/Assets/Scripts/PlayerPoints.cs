using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    [SerializeField] private int pointsPerPowerUp = 250;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            GetPowerUp();
            Destroy(collision.gameObject);
        }

    }

    /// <summary>
    /// This method gives the player points if it takes a power up and 
    /// changes the remaining power ups
    /// </summary>
    private void GetPowerUp()
    {
        //Play power up sound
        AudioManager.Instance?.PlayPowerUpSound();

        //Manage points
        GameManager.Instance.PlayerPoints += pointsPerPowerUp;
        LevelManager.Instance.CurrentPlayerPowerUps += 1;
        LevelManager.Instance.RemainingPlayerPowerUps -= 1;
    }
}

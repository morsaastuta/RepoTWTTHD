using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] private int health = 5;
    public int Health { get => health; set => health = value; }
    private bool canTakeDamage = true;

    [Header("Blink")]
    [SerializeField] private float blinkSeconds = 0.2f;
    [SerializeField] private Color blinkColor = Color.red;
    private SpriteRenderer spriteRenderer; //To put the color filter in the sprite renderer

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// This method manages the damage that the player takes
    /// </summary>
    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            //Play damage audio
            AudioManager.Instance?.PlayDamageSound();

            //reduce player's health
            health--;

            if (health <= 0)
            {
                LevelManager.Instance.GameOver();
            }

            StartCoroutine(BlinkSprite(4));
        }
    }

    /// <summary>
    /// This method repeats blinkTimes times a blink every blinkSeconds
    /// </summary>
    /// <param name="blinkTimes">Number of times that the sprite blinks</param>
    /// <returns></returns>
    private IEnumerator BlinkSprite(int blinkTimes)
    {
        canTakeDamage = false;

        //repeat blinkTimes times
        do
        {
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(blinkSeconds);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkSeconds);
            blinkTimes--;
        } while (blinkTimes > 0);
        
        canTakeDamage = true;
    }
}

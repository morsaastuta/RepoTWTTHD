using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource powerUpAudio;
    [SerializeField] private AudioSource damageAudio;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        } else
        {
            //assign the static instance to the object
            Instance = this;
            //maintain this instance to the next scene
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayDamageSound()
    {
        damageAudio.Play();
    }

    public void PlayPowerUpSound()
    {
        powerUpAudio.Play();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private int maxLevel = 0;
    private int maxScore = 0;

    private void Awake()
    {
        //Singleton, reference Instance to this object instance
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //assign the static instance to the object
            Instance = this;
            //maintain this instance to the next scene
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

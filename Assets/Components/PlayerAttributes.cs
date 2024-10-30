using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    // Persistent memory (Health)
    public float pm = 100f;
    public float apm = 0f;

    // Volatile memory (Mana)
    public float vm = 50f;
    public float avm = 0f;

    // Movement
    public float speed = 10f;
    public bool grounded = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

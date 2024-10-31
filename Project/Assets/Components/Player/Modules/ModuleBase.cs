using UnityEngine;

public class ModuleBase : MonoBehaviour
{
    public bool active = true;

    PlayerAttributes attributes;

    void Start()
    {
        attributes = GetComponentInParent<PlayerAttributes>();
    }

    void Update()
    {
        
    }
}

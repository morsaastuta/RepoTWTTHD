using UnityEngine;

public class ModuleBase : MonoBehaviour
{
    public bool active = true;

    PlayerBehaviour attributes;

    void Start()
    {
        attributes = GetComponentInParent<PlayerBehaviour>();
    }

    void Update()
    {
        
    }

    public bool DefaultConditions()
    {
        return active && attributes.entityCode.FreeVM();
    }
}

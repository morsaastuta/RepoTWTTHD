using UnityEngine;

public class ModuleBehaviour : MonoBehaviour
{
    protected EntityBehaviour user;
    protected ModuleBaseBehaviour modBase;

    protected virtual void Start()
    {
        user = GetComponentInParent<EntityBehaviour>();
        modBase = GetComponentInParent<ModuleBaseBehaviour>();
    }
}

using UnityEngine;

public class ModuleBehaviour : MonoBehaviour
{
    protected EntityBehaviour user;
    protected ModuleBaseBehaviour modBase;
    protected AudioSource sfxSource;

    protected virtual void Start()
    {
        if (GetComponentInParent<EntityBehaviour>()) user = GetComponentInParent<EntityBehaviour>();
        if (GetComponentInParent<ModuleBaseBehaviour>()) modBase = GetComponentInParent<ModuleBaseBehaviour>();
        if (GetComponent<AudioSource>()) sfxSource = GetComponent<AudioSource>();
    }
}

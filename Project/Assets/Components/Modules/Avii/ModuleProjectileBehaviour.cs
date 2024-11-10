using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModuleProjectileBehaviour : MonoBehaviour
{
    ModuleBaseBehaviour modBase;

    [Header("References")]
    [SerializeField] GameObject aim;
    [SerializeField] GameObject projectile;

    void Start()
    {
        modBase = GetComponentInParent<ModuleBaseBehaviour>();
    }

    void Update()
    {
        if (Shortcuts.Pressed(LevelManager.instance.Attack)) ActionShoot();
    }

    void ActionShoot()
    {
        if (modBase.DefaultConditions())
        {
            GameObject cast = Shortcuts.InstantiateCast(gameObject, Instantiate(projectile), true);
            cast.transform.position = transform.position;
            cast.GetComponent<ProjectileBehaviour>().SetObjective(aim.transform);
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModuleProjectileBehaviour : MonoBehaviour
{
    ModuleBaseBehaviour modBase;

    [SerializeField] GameObject aim;
    [SerializeField] GameObject projectile;

    [SerializeField] InputActionReference shoot;

    void Start()
    {
        modBase = GetComponentInParent<ModuleBaseBehaviour>();
    }

    void Update()
    {
        shoot.action.performed += ActionShoot;
    }

    void ActionShoot(InputAction.CallbackContext obj)
    {
        if (modBase.DefaultConditions())
        {
            GameObject b = Shortcuts.InstantiateCast(gameObject, Instantiate(projectile), true);

            b.transform.position = transform.position;

            b.GetComponent<ProjectileBehaviour>().SetObjective(aim.transform);
        }
    }
}

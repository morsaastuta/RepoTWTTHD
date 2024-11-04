using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModProjectile : MonoBehaviour
{
    ModuleBase modBase;

    [SerializeField] GameObject aim;
    [SerializeField] GameObject bullet;

    [SerializeField] InputActionReference shoot;

    void Start()
    {
        modBase = GetComponentInParent<ModuleBase>();
    }

    void Update()
    {
        shoot.action.performed += ActionShoot;
    }

    void ActionShoot(InputAction.CallbackContext obj)
    {
        if (modBase.DefaultConditions())
        {
            GameObject b = Shortcuts.InstantiateCast(gameObject, Instantiate(bullet), true);

            b.transform.position = transform.position;

            b.GetComponent<BulletBehaviour>().SetObjective(aim.transform);
        }
    }
}

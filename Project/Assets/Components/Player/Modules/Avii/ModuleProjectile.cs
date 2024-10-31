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
        if (modBase.active)
        {
            shoot.action.performed += ActionShoot;
        }
    }

    void ActionShoot(InputAction.CallbackContext obj)
    {
        GameObject b = Instantiate(bullet);

        b.transform.position = transform.position;

        b.GetComponent<BulletController>().SetObjective(aim.transform);
    }
}

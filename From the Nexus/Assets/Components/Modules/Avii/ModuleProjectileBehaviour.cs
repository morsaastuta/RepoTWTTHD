using UnityEngine;

public class ModuleProjectileBehaviour : ModuleBehaviour
{
    [Header("References")]
    [SerializeField] GameObject aim;
    [SerializeField] GameObject projectile;

    void Update()
    {
        if (Shortcuts.Pressed(GameManager.instance.attack)) ActionShoot();
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

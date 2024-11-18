using UnityEngine;

public class ModuleCleanseBehaviour : ModuleBehaviour
{
    [Header("References")]
    [SerializeField] GameObject cleanse;

    void Update()
    {
        if (Shortcuts.Pressed(GameManager.instance.burst)) ActionCleanse();
    }

    void ActionCleanse()
    {
        if (modBase.DefaultConditions())
        {
            GameObject cast = Shortcuts.InstantiateCast(gameObject, Instantiate(cleanse), true);
            cast.transform.position = transform.position;
        }
    }
}

using Glossary;
using UnityEngine;

public class ModuleShieldBehaviour : ModuleBehaviour
{
    int timerMax = 50;
    int timer = 0;

    [Header("References")]
    [SerializeField] Animator shield;

    void Update()
    {

        if (Shortcuts.Pressed(LevelManager.instance.react)) ActionShield();
    }

    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer--;
            if (timer <= 0)
            {
                shield.SetBool("on", false);
                GetComponentInParent<PlayerBehaviour>().entityCode.RemoveState(State.Disconnected);
            }
        }
    }

    void ActionShield()
    {
        if (modBase.DefaultConditions())
        {
            Shortcuts.NullifyMovement(GetComponentInParent<PlayerBehaviour>());
            GetComponentInParent<PlayerBehaviour>().entityCode.AddState(State.Disconnected);
            shield.SetBool("on", true);
            timer = timerMax;
        }
    }
}

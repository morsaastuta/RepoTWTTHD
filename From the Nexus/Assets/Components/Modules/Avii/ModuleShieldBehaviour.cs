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
        if (Shortcuts.Pressed(GameManager.instance.react) && !user.entityCode.HasState(State.Disconnected)) ActionShield();
    }

    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer--;
            if (timer <= 0)
            {
                shield.SetBool("on", false);
                user.entityCode.RemoveState(State.Shielded);
                user.entityCode.RemoveState(State.Disconnected);
            }
        }
    }

    void ActionShield()
    {
        if (modBase.DefaultConditions())
        {
            user.entityCode.AllocateVM(8f);
            Shortcuts.NullifyMovement(GetComponentInParent<PlayerBehaviour>());
            user.entityCode.AddState(State.Shielded);
            user.entityCode.AddState(State.Disconnected);

            JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Shield, false);

            shield.SetBool("on", true);
            timer = timerMax;
        }
    }
}

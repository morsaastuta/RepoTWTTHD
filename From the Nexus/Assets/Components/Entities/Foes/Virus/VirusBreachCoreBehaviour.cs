using Glossary;
using System.Collections.Generic;
using UnityEngine;

public class VirusBreachCoreBehaviour : FoeBehaviour
{
    [Header("Minions")]
    [SerializeField] List<VirusPolyhedralBehaviour> minions = new();

    [Header("References")]
    [SerializeField] GameObject rotator;
    [SerializeField] Animator rotatorAnimator;
    [SerializeField] Animator coreAnimator;
    [SerializeField] Animator chainsAnimator;
    [SerializeField] GameObject coreBody;
    [SerializeField] Animator bodyAnimator;
    
    float rotationSpeedOff = 50f;
    float rotationSpeedOn = 200f;

    protected override void Start()
    {
        entityCode = new VirusBreachCore();

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (!entityCode.HasState(State.Disconnected))
        {
            Detect();

            if (targetDetected)
            {
                foreach (VirusPolyhedralBehaviour minion in minions) minion.OverrideTarget(true, targetPos);

                coreBody.transform.Rotate(0, 0, -Time.deltaTime * rotationSpeedOn, Space.Self);
                rotator.transform.Rotate(0, 0, Time.deltaTime * rotationSpeedOn, Space.Self);

                rotatorAnimator.SetBool("on", true);
                coreAnimator.SetBool("on", true);
                chainsAnimator.SetBool("on", true);
                bodyAnimator.SetBool("on", true);
            }
            else
            {
                foreach (VirusPolyhedralBehaviour minion in minions) minion.OverrideTarget(false, targetPos);

                coreBody.transform.Rotate(0, 0, -Time.deltaTime * rotationSpeedOff, Space.Self);
                rotator.transform.Rotate(0, 0, Time.deltaTime * rotationSpeedOff, Space.Self);

                rotatorAnimator.SetBool("on", false);
                coreAnimator.SetBool("on", false);
                chainsAnimator.SetBool("on", false);
                bodyAnimator.SetBool("on", false);
            }
        }
    }

    public override void ReceiveDamage(float p, float v, Vector2 pos)
    {
        if (!entityCode.HasState(State.Disconnected) && flinchTimer <= 0)
        {
            // Stop all minions
            foreach (VirusPolyhedralBehaviour minion in minions) minion.OverrideTarget(false, targetPos);

            // Allocate memory
            entityCode.AllocatePM(p);
            entityCode.AllocateVM(v);

            // Flinch
            flinchTimer = flinchTimerMax;
            entityCode.AddState(State.Disconnected);
            body.linearVelocityX = 0f;
            body.linearVelocityY = 0f;
        }
    }
}

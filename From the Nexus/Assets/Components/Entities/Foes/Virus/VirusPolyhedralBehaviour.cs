using Glossary;
using UnityEngine;

public class VirusPolyhedralBehaviour : FoeBehaviour
{
    [Header("References")]
    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator fibersAnimator;
    [SerializeField] Animator castAnimator;

    protected override void Start()
    {
        entityCode = new VirusPolyhedral();

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        Approach();

        if (targetDetected)
        {
            bodyAnimator.SetBool("chase", true);
            fibersAnimator.SetBool("chase", true);
            castAnimator.SetBool("chase", true);
        }
        else
        {
            bodyAnimator.SetBool("chase", false);
            fibersAnimator.SetBool("chase", false);
            castAnimator.SetBool("chase", false);
        }
    }

    public override void ReceiveDamage(float p, float v, Vector2 pos)
    {
        if (!entityCode.HasState(State.Disconnected) && flinchTimer <= 0)
        {
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

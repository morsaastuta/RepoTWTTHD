using UnityEngine;
using Glossary;

public class EntityBehaviour : MonoBehaviour
{
    [SerializeField] bool bypassGravity = false;

    // Code
    public Entity entityCode;

    // Physics
    protected Rigidbody2D body;
    protected float directionX;
    protected float directionY;
    public bool grounded = false;
    float flinchTimerMax = 400;
    float flinchTimer = 0;

    // Projection
    protected SpriteRenderer renderer;
    protected Animator animator;

    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        renderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        // Death
        if (entityCode.apm > entityCode.pm)
        {
            // Store score
            LevelManager.instance.StoreSM(entityCode.sm);

            // If FOE
            if (entityCode.GetType().BaseType.Equals(typeof(Foe))) Destroy(gameObject);
            // If PLAYER
            else
            {
                LevelManager.instance.ClearSM();
                transform.position = LevelManager.instance.pointer.position;
                body.linearVelocity = new(0, 0);
                entityCode.ClearMemory();
            }
        }

        if (!entityCode.HasState(State.Disconnected) && entityCode.AllocatedVM()) entityCode.ClearVM(Time.deltaTime * 1f);

        if (flinchTimer > 0)
        {
            flinchTimer--;

            if (flinchTimer % 25 == 0)
            {
                if (flinchTimer % 2 == 0) renderer.color = Color.white;
                else renderer.color = new(0, 0, 0, 0);
            }

            if (flinchTimer <= flinchTimerMax/2) entityCode.RemoveState(State.Disconnected);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (body.linearVelocityY <= 0) grounded = Shortcuts.GetGroundRaycast(GetComponent<Collider2D>());

        // If the player is grounded, nullify gravity and velocity Y
        if (!bypassGravity)
        {
            if (grounded)
            {
                body.gravityScale = 0;
                body.linearVelocityY = 0;
            }
            else body.gravityScale = 1;
        }

        // Start at 0
        if (!entityCode.HasState(State.Disconnected)) body.linearVelocityX = entityCode.speed * directionX;
    }

    protected void EmergencyStop(bool enter)
    {
        if (enter)
        {
            body.linearVelocityX = 0;
            body.linearVelocityY = 0;
            body.gravityScale = 0;
        }
        else
        {
            body.gravityScale = 1;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
    }

    public void ReceiveDamage(float p, float v, Vector2 pos)
    {
        if (flinchTimer <= 0)
        {
            // Allocate memory
            entityCode.AllocatePM(p);
            entityCode.AllocateVM(v);

            // Flinch
            flinchTimer = flinchTimerMax;
            entityCode.AddState(State.Disconnected);
            if (transform.position.x > pos.x) body.linearVelocityX = 2f;
            else if (transform.position.x < pos.x) body.linearVelocityX = -2f;
            grounded = false;
            body.linearVelocityY = 4f;
        }
    }
}
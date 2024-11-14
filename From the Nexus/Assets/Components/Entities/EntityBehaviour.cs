using Glossary;
using UnityEngine;

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
    protected Vector3 lastPos = new();
    protected float flinchTimerMax = 120;
    protected float flinchTimer = 0;

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
    }

    protected virtual void FixedUpdate()
    {
        // Check ground
        if (body.linearVelocityY <= 0) grounded = Shortcuts.GetGrounded(GetComponent<Collider2D>(), lastPos);
        lastPos = transform.position;

        // If player is fliching
        if (flinchTimer > 0)
        {
            flinchTimer--;

            if (flinchTimer % 6 == 0)
            {
                if (flinchTimer % 12 == 0) renderer.color = Color.white;
                else renderer.color = new(0, 0, 0, 0);
            }

            if (flinchTimer <= flinchTimerMax / 2) entityCode.RemoveState(State.Disconnected);
        }

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
        else
        {
            if (!entityCode.HasState(State.Disconnected)) body.linearVelocityY = entityCode.speed * directionY;
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

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
    }

    protected virtual void OnTriggerStay2D(Collider2D collider)
    {
    }

    public virtual void ReceiveDamage(float p, float v, Vector2 pos)
    {
        if (!entityCode.HasState(State.Disconnected) && flinchTimer <= 0)
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

using Glossary;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    [SerializeField] bool bypassGravity = false;

    // Code
    public Entity entityCode;

    // Physics
    protected Rigidbody2D body;
    protected Collider2D bounds;
    protected float directionX;
    protected float directionY;
    public bool grounded = false;
    protected Vector3 lastPos = new();
    protected float flinchTimerMax = 80;
    protected float flinchTimer = 0;

    // Projection
    protected List<SpriteRenderer> projections = new();
    protected Animator animator;

    // Audio
    protected AudioSource sfxSource;

    protected virtual void Start()
    {
        if (GetComponent<AudioSource>()) sfxSource = GetComponent<AudioSource>();
        if (GetComponent<Rigidbody2D>()) body = GetComponent<Rigidbody2D>();
        if (GetComponent<Collider2D>()) bounds = GetComponent<Collider2D>();
        if (GetComponentInChildren<Animator>()) animator = GetComponentInChildren<Animator>();
        if (GetComponentInChildren<SpriteRenderer>()) projections.AddRange(GetComponentsInChildren<SpriteRenderer>());
    }

    protected virtual void Update()
    {
        // Death
        if (entityCode.apm > entityCode.pm)
        {
            // Store score
            LevelManager.instance.StoreSM(entityCode.sm);

            // If FOE
            if (entityCode.GetType().BaseType.Equals(typeof(Foe))) StartCoroutine(Shortcuts.DestroyAudibleObject(gameObject));
            // If PLAYER
            else LevelManager.instance.Death();
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

            if (flinchTimer % 4 == 0)
            {
                foreach (SpriteRenderer projection in projections)
                {
                    if (flinchTimer % 8 == 0) projection.color = Color.white;
                    else projection.color = new(0, 0, 0, 0);
                }
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
        else body.gravityScale = 0;

        // State effects
        if (entityCode.HasState(State.Cutscene))
        {
            directionX = 0;
            directionY = 0;
            body.gravityScale = 0;
        }
        else if (!bypassGravity) body.gravityScale = 1;

        if (!entityCode.HasState(State.Disconnected))
        {
            body.linearVelocityX = entityCode.speed * directionX;
            if (bypassGravity) body.linearVelocityY = entityCode.speed * directionY;
        }
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

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
    }

    protected virtual void OnTriggerStay2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Outbounds"))
        {
            if (Shortcuts.GetColliderLayer(bounds, "Foe")) StartCoroutine(Shortcuts.DestroyAudibleObject(gameObject));
            else if (Shortcuts.GetColliderLayer(bounds, "Player"))
            {
                transform.position = LevelManager.instance.pointer.position;
                body.linearVelocity = new Vector2(0, 0);
                ReceiveDamage(5, 0, transform.position);
            }
        }
    }

    public virtual void ReceiveDamage(float p, float v, Vector2 pos)
    {
        if (!entityCode.HasState(State.Shielded) && !entityCode.HasState(State.Cutscene) && flinchTimer <= 0)
        {
            JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Hit, false);

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

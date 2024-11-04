using UnityEngine;
using Glossary;

public class EntityBehaviour : MonoBehaviour
{
    // Code
    public Entity entityCode;

    // Physics
    protected Rigidbody2D body;
    protected float directionX;
    protected float directionY;
    public bool grounded = false;

    // Projection
    protected Animator animator;

    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        if (entityCode.avm > 0) entityCode.avm -= 0.01f;
    }

    protected virtual void FixedUpdate()
    {
        if (body.linearVelocityY <= 0) grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.05f, LayerMask.GetMask("Ground", "Platform", "Box"));

        // If the player is grounded, nullify gravity and velocity Y
        if (grounded)
        {
            body.gravityScale = 0;
            body.linearVelocityY = 0;
        }
        else body.gravityScale = 1;

        // Start at 0
        body.linearVelocityX = entityCode.speed * directionX;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Components
    [SerializeField] Rigidbody2D body;
    [SerializeField] PlayerAttributes attributes;
    [SerializeField] Animator animator;
    #endregion

    #region Input references
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    #endregion

    float directionX;
    float directionY;

    void Update()
    {
        if (attributes.canMove) directionX = move.action.ReadValue<Vector2>().x;
        else directionX = 0;

        jump.action.performed += ActionJump;
    
        // If the player is moving horizontally
        if (directionX != 0)
        {
            animator.SetBool("x", true);
            if (directionX < 0) transform.localScale = new Vector2(-1, 1);
            else if (directionX > 0) transform.localScale = new Vector2(1, 1);
        }
        else animator.SetBool("x", false);

        AnimatorSetters();
    }

    void AnimatorSetters()
    {
        // If the player is moving vertically
        if (directionY != 0) animator.SetBool("y", true);
        else animator.SetBool("y", false);

        // If the player is grounded
        if (attributes.grounded) animator.SetBool("g", true);
        else animator.SetBool("g", false);

        // Set values
        animator.SetFloat("yDir", directionY);
    }

    void FixedUpdate()
    {
        if (!attributes.platformed) attributes.grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

        // Start at 0
        body.linearVelocityX = attributes.speed * directionX;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (body.linearVelocityY < 0 && collision.collider.gameObject.layer == LayerMask.NameToLayer("Platforms")) attributes.platformed = true;


    }
}

    #region Actions
    void ActionJump(InputAction.CallbackContext obj)
    {
        if (attributes.grounded) body.linearVelocityY = attributes.jumpPower;
    }
    #endregion
}

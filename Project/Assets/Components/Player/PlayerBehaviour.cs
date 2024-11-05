using UnityEngine;
using Glossary;
using UnityEngine.InputSystem;

public class PlayerBehaviour : EntityBehaviour
{
    #region Module references
    [SerializeField] GameObject mod_projectile;
    [SerializeField] GameObject mod_shield;
    [SerializeField] GameObject mod_cleanse;

    [SerializeField] GameObject mod_chop;
    [SerializeField] GameObject mod_grapple;
    #endregion

    // Attributes
    int playerID;
    [SerializeField] GameObject hud;

    #region Input references
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    #endregion

    // Movement
    public bool canMove = true;

    protected override void Start()
    {
        base.Start();

        playerID = 1;
        entityCode = new Avii();

        GameObject newHUD = Instantiate(hud, GameObject.Find("Canvas").transform);
        newHUD.transform.localPosition = new(-Screen.width / 2 + playerID * Screen.width / 5 - 256, Screen.height / 2 - 128);
        newHUD.GetComponent<HUDController>().SetPlayer(this);
        
        foreach (Mod module in ((Player)entityCode).modules)
        {
            GameObject mod = mod_projectile;

            switch (module)
            {
                case Mod.Projectile: mod = mod_projectile; break;
                case Mod.Shield: mod = mod_shield; break;
                case Mod.Cleanse: mod = mod_cleanse; break;
                case Mod.Chop: mod = mod_chop; break;
                case Mod.Grapple: mod = mod_grapple; break;
            }

            Instantiate(mod, transform);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!entityCode.HasState(State.Disconnected)) directionX = move.action.ReadValue<Vector2>().x;
        else directionX = 0;

        directionY = body.linearVelocityY;

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

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (Shortcuts.CollidesWithLayer(collision, "Foe")) ReceiveDamage(5, 0, collision.collider.GetComponent<Transform>().position);
    }

    void AnimatorSetters()
    {
        // If the player is moving vertically
        if (directionY != 0) animator.SetBool("y", true);
        else animator.SetBool("y", false);

        // If the player is grounded
        if (grounded) animator.SetBool("g", true);
        else animator.SetBool("g", false);

        // Set values
        animator.SetFloat("yDir", directionY);
    }

    #region Actions
    void ActionJump(InputAction.CallbackContext obj)
    {
        if (grounded)
        {
            grounded = false;
            body.linearVelocityY = entityCode.jumpPower;
        }
    }
    #endregion
}

using UnityEngine;
using Glossary;
using UnityEngine.InputSystem;
using System;

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
    CanvasController canvasController;
    [SerializeField] GameObject hud;

    #region Input references
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference interact;
    #endregion

    // Input carries
    bool interactCarry;

    // Movement
    public bool canMove = true;

    protected override void Start()
    {
        base.Start();

        // Player setup
        playerID = 1;
        entityCode = new Avii();

        // Canvas/HUD setup
        canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
        GameObject newHUD = Instantiate(hud, canvasController.transform);
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

        if (Shortcuts.Pressed(interact)) interactCarry = true;

        if (!entityCode.HasState(State.Disconnected))
        {
            directionX = move.action.ReadValue<Vector2>().x;

            if (Shortcuts.Pressed(jump)) ActionJump();


            // If the player is moving horizontally
            if (directionX != 0)
            {
                animator.SetBool("x", true);
                if (directionX < 0) transform.localScale = new Vector2(-1, 1);
                else if (directionX > 0) transform.localScale = new Vector2(1, 1);
            }
            else animator.SetBool("x", false);
        }
        else directionX = 0;
        
        directionY = body.linearVelocityY;

        AnimatorSetters();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (interactCarry) {
            interactCarry = false;
            if (Shortcuts.CollidesWithLayer(collider, "Signpost"))
            {
                if (!entityCode.HasState(State.Disconnected))
                {
                    ActionInteract(collider);
                    EmergencyStop(true);
                }
                else if (canvasController.GetTextHUD().NextString())
                {
                    entityCode.RemoveState(State.Disconnected);
                    EmergencyStop(false);
                }
            }
        }
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
    void ActionJump()
    {
        if (grounded)
        {
            grounded = false;
            body.linearVelocityY = entityCode.jumpPower;
        }
    }

    void ActionInteract(Collider2D collider)
    {
        canvasController.GetTextHUD().ReceiveMessage(collider.GetComponent<SignpostBehaviour>().content);
        entityCode.AddState(State.Disconnected);
    }
    #endregion
}
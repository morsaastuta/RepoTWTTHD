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

    // Interaction
    bool interactCarry;
    int timerMax = 30;
    int timer;

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

        if (!entityCode.HasState(State.Disconnected))
        {
            // Movement input
            directionX = move.action.ReadValue<Vector2>().x;

            // Jump input
            if (Shortcuts.Pressed(jump)) ActionJump();

            // Interaction input
            if (Shortcuts.Pressed(interact))
            {
                timer = timerMax;
                interactCarry = true;
            }
            if (timer > 0)
            {
                timer--;
                if (timer <= 0) interactCarry = false;
            }

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

            timer = 0;

            if (Shortcuts.CollidesWithLayer(collider, "Signpost") && !entityCode.HasState(State.Disconnected)) ActionInteract(collider);
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

    public void EnterInteraction(bool enter)
    {
        if (enter)
        {
            entityCode.AddState(State.Disconnected);
            EmergencyStop(true);
        }
        else
        {
            entityCode.RemoveState(State.Disconnected);
            EmergencyStop(false);
        }
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
    }
    #endregion
}
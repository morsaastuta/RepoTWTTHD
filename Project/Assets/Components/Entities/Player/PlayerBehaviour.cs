using UnityEngine;
using Glossary;
using UnityEngine.InputSystem;
using System;

public class PlayerBehaviour : EntityBehaviour
{
    // Attributes
    int playerID;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject modBase;

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

        // HUD setup
        GameObject newHUD = Instantiate(hud, LevelManager.instance.transform);
        newHUD.transform.localPosition = new(-Screen.width / 2 + playerID * Screen.width / 5 - 256, Screen.height / 2 - 128);
        newHUD.GetComponent<HUDController>().SetPlayer(this);

        transform.position = LevelManager.instance.Pointer.position;
    }

    protected override void Update()
    {
        base.Update();

        if (!entityCode.HasState(State.Disconnected))
        {
            modBase.SetActive(true);

            // Movement input
            directionX = LevelManager.instance.Move.action.ReadValue<Vector2>().x;

            // Jump input
            if (Shortcuts.Pressed(LevelManager.instance.Jump)) ActionJump();

            // Interaction input
            if (Shortcuts.Pressed(LevelManager.instance.Interact))
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
        else
        {
            directionX = 0;
            modBase.SetActive(false);
        }
        
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
        if (collider.GetComponent<SignpostBehaviour>()) LevelManager.instance.MessageHUD.ReceiveMessage(collider.GetComponent<SignpostBehaviour>().content);
    }
    #endregion
}
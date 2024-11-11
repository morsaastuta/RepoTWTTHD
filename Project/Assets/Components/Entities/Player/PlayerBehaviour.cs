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

        // Start pos
        transform.position = LevelManager.instance.pointer.position;
    }

    protected override void Update()
    {
        base.Update();

        if (!entityCode.HasState(State.Disconnected))
        {
            modBase.SetActive(true);

            // Movement input
            directionX = LevelManager.instance.move.action.ReadValue<Vector2>().x;

            // Jump input
            if (Shortcuts.Pressed(LevelManager.instance.jump)) ActionJump();

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

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Outbounds"))
        {
            transform.position = LevelManager.instance.pointer.position;
            body.linearVelocity = new Vector2(0, 0);
            entityCode.AllocatePM(5);
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
    #endregion
}
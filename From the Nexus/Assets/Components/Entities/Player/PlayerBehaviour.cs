using Glossary;
using UnityEngine;

public class PlayerBehaviour : EntityBehaviour
{
    // Attributes
    int playerID;
    [SerializeField] GameObject hud;
    GameObject hudSelf;
    [SerializeField] GameObject modBase;

    // Movement
    public bool canMove = true;
    public bool right = true;

    protected override void Start()
    {
        base.Start();

        // Player setup
        playerID = 1;
        entityCode = new Avii();

        // HUD setup
        hudSelf = Instantiate(hud, LevelManager.instance.transform);
        hudSelf.transform.localPosition = new(-Screen.width / 2 + playerID * Screen.width / 5 - 256, Screen.height / 2 - 128);
        hudSelf.GetComponent<HUDController>().SetPlayer(this);

        // Start pos
        LevelManager.instance.PointTo(LevelManager.instance.pointer, false);

        ShowHUD(true);
    }

    protected override void Update()
    {
        base.Update();

        if (!entityCode.HasState(State.Cutscene) && !entityCode.HasState(State.Disconnected))
        {
            // Movement input
            directionX = GameManager.instance.move.action.ReadValue<Vector2>().x;

            // Jump input
            if (Shortcuts.Pressed(GameManager.instance.jump)) ActionJump();
        }
        
        directionY = body.linearVelocityY;

        AnimatorSetters();
    }

    void AnimatorSetters()
    {
        // If the player is moving horizontally
        if (directionX != 0)
        {
            animator.SetBool("x", true);
            if (directionX < 0)
            {
                transform.localScale = new Vector2(-1, 1);
                right = false;
            }
            else if (directionX > 0)
            {
                transform.localScale = new Vector2(1, 1);
                right = true;
            }
        }
        else animator.SetBool("x", false);

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
        LevelManager.instance.canPause = !enter;
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

    public void ShowHUD(bool on)
    {
        hudSelf.SetActive(on);
    }

    #region Actions
    void ActionJump()
    {
        if (grounded)
        {
            JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Jump, false);
            grounded = false;
            body.linearVelocityY = entityCode.jumpPower;
        }
    }
    #endregion
}
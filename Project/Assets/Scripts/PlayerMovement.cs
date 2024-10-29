using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;
    private GameObject playerFeet; //For ground check
    private Animator playerAnimator;

    //Ground check layer
    private LayerMask groundLayer;

    //Movement
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 1;

    //Inputs
    private float vAxis = 0;
    private float hAxis = 0;
    private bool jumpInput = false;

    private Vector3 jumpVector = Vector3.zero;

    private void Awake()
    {
        //Inicialization
        rb = GetComponent<Rigidbody2D>();

        //Looks for the component among all objects in the scene, not only children
        //playerSprite = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>(); 

        //Looks for the first component of said type among the children of an object
        playerSprite = GetComponentInChildren<SpriteRenderer>();

        playerFeet = GameObject.Find("PlayerFeet");
        groundLayer = LayerMask.GetMask("Ground");

        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GetInputs();
        MovePlayer();
        FlipSprite();

        CalculateJump();

        ChangeAnimations();
    }

    private void FixedUpdate() //Update for physics
    {
        Jump();
    }

    /// <summary>
    /// This method reads the vertical and horizontal Axis from Input
    /// </summary>
    private void GetInputs()
    {
        //Axis
        vAxis = Input.GetAxisRaw("Vertical");
        hAxis = Input.GetAxisRaw("Horizontal");

        //Jump
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        
    }

    /// <summary>
    /// This method changes the player position using Axis and speed
    /// </summary>
    private void MovePlayer()
    {
        transform.position += Vector3.right * hAxis * moveSpeed * Time.deltaTime; //deltaTime is time between frames

    }

    /// <summary>
    /// This method changes the player sprite to flip it
    /// </summary>
    private void FlipSprite()
    {
        if (hAxis > 0) playerSprite.flipX = false;
        else if (hAxis < 0) playerSprite.flipX = true;
    }

    /// <summary>
    /// This method checks if the Player is on the ground using 3 RayCasts
    /// </summary>
    /// <returns>Grounded state in boolean</returns>
    private bool IsGrounded()
    {
        Vector3 feetPosition = playerFeet.transform.position;
        //Show the future raycasts with red color
        //Center RayCast
        Debug.DrawRay(feetPosition, Vector3.down * 0.5f, Color.red);
        //Left RatCast
        Debug.DrawRay(new Vector3(feetPosition.x - 0.2f, feetPosition.y, 0), Vector3.down * 0.5f, Color.blue);
        //Right RatCast
        Debug.DrawRay(new Vector3(feetPosition.x + 0.2f, feetPosition.y, 0), Vector3.down * 0.5f, Color.yellow);


        //Raycast -> position from which ray goes, direction of ray, length of ray, layer it has to find 
        bool isCenterGrounded = Physics2D.Raycast(feetPosition, Vector3.down, 0.5f, groundLayer);
        bool isLeftGrounded = Physics2D.Raycast(new Vector3(feetPosition.x - 0.2f, feetPosition.y, 0), Vector3.down, 0.5f, groundLayer);
        bool isRightGrounded = Physics2D.Raycast(new Vector3(feetPosition.x + 0.2f, feetPosition.y, 0), Vector3.down, 0.5f, groundLayer);

        return isCenterGrounded || isLeftGrounded || isRightGrounded;
    }

    /// <summary>
    /// This method calculates the Vector3 for the jump
    /// </summary>
    private void CalculateJump()
    {
        if(jumpInput && IsGrounded())
        {
            jumpVector = Vector3.up * jumpForce;
        } else if (!IsGrounded())
        {
            jumpVector = Vector3.zero;
        }
    }

    /// <summary>
    /// Generate the Physics of the Jump movement
    /// </summary>
    private void Jump()
    {
        rb.AddForce(jumpVector, ForceMode2D.Impulse);
    }

    /// <summary>
    /// This method changes the animation depending on the action
    /// </summary>
    private void ChangeAnimations()
    {
        //Jumping
        if (!IsGrounded())
        {
            playerAnimator.Play("PlayerJump");
        }

        //Running
        else if (IsGrounded() && hAxis != 0) 
        {
            playerAnimator.Play("PlayerRun");
        }

        //Idle
        else if(IsGrounded() && hAxis == 0)
        {
            playerAnimator.Play("PlayerIdle");
        }
    }
}

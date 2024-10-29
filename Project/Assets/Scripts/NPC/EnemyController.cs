using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType
{
    Crab, 
    Octopus, 
    Jumper
}
public class EnemyController : MonoBehaviour
{

    //Enemy Type
    public EnemyType enemyType;

    //Components
    Transform groundDetection;
    SpriteRenderer spriteRenderer;

    //Ground check
    LayerMask layer;
    //[SerializeField] private float rayDistance = 1.5f;

    //Movement
    [SerializeField] Vector2 direction = Vector2.right;
    [SerializeField] float moveSpeed = 1;

    //Sinewave Movement
    [Header("Only Jumper Enemy movement")]
    float sinCenterY;
    [SerializeField] float sinAmplitude = 2;
    [SerializeField] float sinFrequency = 2;


    void Start()
    {
        sinCenterY = transform.position.y;
    }

    void Awake()
    {
        groundDetection = transform.GetChild(1);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        layer = LayerMask.GetMask("Ground");
        
    }

    void Update()
    {
        EnemyMove();
    }

    /// <summary>
    /// This code detects if the Enemy is touching the floor
    /// </summary>
    /// <returns></returns>
    //private bool GroundDetection()
    //{
    //    Debug.DrawRay(groundDetection.position, Vector2.down * rayDistance, Color.blue);
    //    return Physics2D.Raycast(groundDetection.position, Vector2.down, rayDistance, layer);
    //}

    /// <summary>
    /// This code detects if the Enemy is touching the ceiling
    /// </summary>
    /// <returns></returns>
    //private bool CeilingDetection()
    //{
    //    Debug.DrawRay(groundDetection.position, Vector2.up * rayDistance, Color.yellow);
    //    return Physics2D.Raycast(groundDetection.position, Vector2.up, rayDistance, layer);
    //}

    /// <summary>
    /// This code detects if the Enemy is touching any collision
    /// </summary>
    /// <param name="rayLength"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    bool GeneralDetection(float rayLength, Vector3 direction, Color color)
    {
        Debug.DrawRay(groundDetection.position, direction * rayLength, color);
        return Physics2D.Raycast(groundDetection.position, direction, rayLength, layer);
    }

    /// <summary>
    /// This method detects any collision on a wall
    /// </summary>
    /// <returns></returns>
    //private bool WallDetection()
    //{
    //    Vector2 direction = Vector2.zero;
    //    direction = (spriteRenderer.flipX) ? Vector2.right : Vector2.left;
    //    Debug.DrawRay(groundDetection.position, direction * 0.25f, Color.green);
    //    return Physics2D.Raycast(groundDetection.position, direction, 0.25f, layer);
    //}

    /// <summary>
    /// This method controls the way the Enemy moves
    /// </summary>
    void EnemyMove()
    {
        switch (enemyType) {
            case EnemyType.Crab: //If my enemy type is a crab
                CrabMove();
                break;
            case EnemyType.Octopus: //If my enemy type is an octopus
                OctopusMove();
                break;
            case EnemyType.Jumper:
                JumperMove();
                break;
        }
    }

    /// <summary>
    /// This method controls the way the crab moves
    /// </summary>
    void CrabMove()
    {
        //if ground bellow is not detected, or the wall is detected, change direction
        if (!GeneralDetection(1.5f, Vector3.down, Color.yellow) || GeneralDetection(0.5f, direction, Color.green)) 
        {
            direction = -direction; //Same as doing [direction *= -1;]
            spriteRenderer.flipX = !spriteRenderer.flipX;

            //Invert the groundDetection X point
            groundDetection.localPosition = new Vector3(-groundDetection.localPosition.x, groundDetection.localPosition.y, groundDetection.localPosition.z);
        }

        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// This method controls the way the octopus moves
    /// </summary>
    void OctopusMove()
    {
        //if ground or ceiling is detected, change direction
        if (GeneralDetection(1f, direction, Color.magenta))
        {
            direction = -direction; //Same as doing [direction *= -1;]
        }

        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// This method controls the way the jumper moves
    /// </summary>
    void JumperMove()
    {
        if (GeneralDetection(1f, direction, Color.blue))
        {
            direction = -direction; //Same as doing [direction *= -1;]
        }

        Vector3 enemyPosition = transform.position;

        float sin = Mathf.Sin(enemyPosition.x * sinFrequency) * sinAmplitude;

        enemyPosition.y = sinCenterY + sin;
        enemyPosition.x += direction.x * moveSpeed * Time.deltaTime;

        transform.position = enemyPosition;


        
    }

    //This method checks if the player is touching an enemy and gives him damage if so
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        } 
        
    }
}

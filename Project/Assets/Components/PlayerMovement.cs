using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    [SerializeField] PlayerAttributes attributes;
    [SerializeField] InputActionReference move;

    Vector2 direction;

    void Update()
    {
        direction = move.action.ReadValue<Vector2>();

        Debug.Log(direction.x + " - " + direction.y);
    }

    void FixedUpdate()
    {
        body.linearVelocity = new Vector2(attributes.speed * direction.x, direction.y);
    }
}

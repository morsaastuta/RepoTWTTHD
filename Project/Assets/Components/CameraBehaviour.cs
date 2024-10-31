using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform player;
    float lerpFactor = 0.1f;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, player.position, lerpFactor);
        transform.position = new Vector3(transform.position.x, transform.position.y+0.5f, -10);
    }
}

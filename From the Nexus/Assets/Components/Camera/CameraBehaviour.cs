using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    Transform player;
    float lerpFactor = 0.1f;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, player.position + new Vector3(0,3f,0), lerpFactor);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}

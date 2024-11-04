using UnityEngine;

public class AimBehaviour : MonoBehaviour
{
    Vector3 mousePosition;

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = mousePosition;
    }
}

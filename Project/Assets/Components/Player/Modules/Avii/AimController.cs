using UnityEngine;

public class AimController : MonoBehaviour
{
    Vector3 mousePosition;

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = mousePosition;
    }
}

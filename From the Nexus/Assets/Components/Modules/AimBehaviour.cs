using UnityEngine;
using UnityEngine.InputSystem;

public class AimBehaviour : MonoBehaviour
{
    Vector3 mousePosition;

    void Start()
    {
        mousePosition = new(1,0);
    }

    void Update()
    {
        if (!LevelManager.instance.aim.action.ReadValue<Vector2>().Equals(Vector2.zero))
        {
            mousePosition = LevelManager.instance.aim.action.ReadValue<Vector2>();
        }

        foreach (InputDevice device in GetComponentInParent<PlayerInput>().devices)
        {
            if (device is Mouse)
            {
                transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
            }
            else if (device is Gamepad)
            {
                if (GetComponentInParent<PlayerBehaviour>().right) transform.localPosition = new Vector2(mousePosition.x, mousePosition.y) * 3;
                else transform.localPosition = new Vector2(-mousePosition.x, mousePosition.y) * 3;
            }
        }
    }
}

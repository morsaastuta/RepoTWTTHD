using UnityEngine;
using UnityEngine.UI;

public class MainMenuCameraBehaviour : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] Transform cam;
    [SerializeField] Transform origin;
    [SerializeField] Transform target;

    [Header("Options")]
    [SerializeField] float translationSpeed;
    [SerializeField] bool loop;

    [Header("Alpha screen")]
    [SerializeField] Image screen;
    [SerializeField] bool asActive;
    [SerializeField] float clearingSpeed;
    [SerializeField] float startThreshold;

    void Start()
    {
        cam.position = origin.position;

        if (asActive) screen.gameObject.SetActive(true);
    }

    void Update()
    {
        if (startThreshold > 0) startThreshold -= Time.deltaTime * clearingSpeed;
        else if (asActive && screen.color.a > 0) screen.color = new Color(screen.color.r, screen.color.g, screen.color.b, screen.color.a - Time.deltaTime * clearingSpeed);

        if (loop && cam.position == Vector3.MoveTowards(cam.position, target.position, Time.deltaTime * translationSpeed)) cam.position = origin.position;

        cam.position = Vector3.MoveTowards(cam.position, target.position, Time.deltaTime * translationSpeed);
    }
}

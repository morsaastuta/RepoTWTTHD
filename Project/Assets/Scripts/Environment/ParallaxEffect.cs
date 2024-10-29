using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float effectLevel;

    private Transform cameraTransform;

    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate() //Update for camera and similar, at end of frame
    {
        Vector3 backgroundMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(backgroundMovement.x * effectLevel, backgroundMovement.y, 0);
        lastCameraPosition = cameraTransform.position;
    }
}

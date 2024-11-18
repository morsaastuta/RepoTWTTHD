using UnityEngine;

public class DetectorBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody2D parentBody;

    public bool detected;
    public Vector3 targetPos = new();

    void Update()
    {
        GetComponent<Rigidbody2D>().position = parentBody.position;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Player"))
        {
            targetPos = collider.transform.position + new Vector3(0, 1.775f, 0);
            detected = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Player")) detected = false;
    }
}

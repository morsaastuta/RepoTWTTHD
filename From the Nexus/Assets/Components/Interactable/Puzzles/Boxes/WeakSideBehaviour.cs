using UnityEngine;

public class WeakSideBehaviour : MonoBehaviour
{
    [SerializeField] ProtectedBoxBehaviour box;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Cast"))
        {
            box.durability -= collider.GetComponent<CastBehaviour>().GetCast().apm;
            box.GetComponent<SpriteRenderer>().color = new(0,1,1,1);
            box.Damage();
        }
    }
}

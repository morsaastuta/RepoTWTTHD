using UnityEngine;

public class WeakSideBehaviour : MonoBehaviour
{
    [SerializeField] ProtectedBoxBehaviour box;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Shortcuts.CollidesWithLayer(collision, "Cast"))
        {
            box.durability -= collision.collider.GetComponent<CastBehaviour>().GetCast().apm;
            box.GetComponent<SpriteRenderer>().color = new(0,1,1,1);
            box.Damage();
        }
    }
}

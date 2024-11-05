using UnityEngine;

public class LeakMemoBehaviour : FoeBehaviour
{
    protected override void Start()
    {
        base.Start();

        entityCode = new LeakMemo();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (Shortcuts.CollidesWithLayer(collision, "Player")) collision.collider.GetComponent<EntityBehaviour>().ReceiveDamage(0, 10, transform.position);
    }
}

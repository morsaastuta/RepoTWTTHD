using UnityEngine;

public class DropBehaviour : CastBehaviour
{
    int timer = 400;

    override protected void Start()
    {
        cast = new Drop();

        base.Start();
    }

    override protected void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        if (Shortcuts.GetColliderLayer(collider, "Ground") || Shortcuts.GetColliderLayer(collider, "Player") || Shortcuts.GetColliderLayer(collider, "Foe") || Shortcuts.GetColliderLayer(collider, "Box") || (GetComponent<Rigidbody2D>().linearVelocityY <= 0 && Shortcuts.GetColliderLayer(collider, "Platform"))) Destroy(gameObject);
    }

    void FixedUpdate()
    {
        timer--;
        if (timer <= 0) Destroy(gameObject);
    }
}

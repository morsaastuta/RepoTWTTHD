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

        if (Shortcuts.CollidesWithLayer(collider, "Ground") || Shortcuts.CollidesWithLayer(collider, "Player") || Shortcuts.CollidesWithLayer(collider, "Foe") || Shortcuts.CollidesWithLayer(collider, "Box") || (GetComponent<Rigidbody2D>().linearVelocityY <= 0 && Shortcuts.CollidesWithLayer(collider, "Platform"))) Destroy(gameObject);
    }

    void FixedUpdate()
    {
        timer--;
        if (timer <= 0) Destroy(gameObject);
    }
}

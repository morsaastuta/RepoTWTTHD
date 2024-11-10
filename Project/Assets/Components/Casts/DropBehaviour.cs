using UnityEngine;

public class DropBehaviour : CastBehaviour
{
    int timer = 400;

    override protected void Start()
    {
        cast = new Drop();

        base.Start();
    }

    override protected void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (Shortcuts.CollidesWithLayer(collision, "Ground") || Shortcuts.CollidesWithLayer(collision, "Player") || Shortcuts.CollidesWithLayer(collision, "Foe") || Shortcuts.CollidesWithLayer(collision, "Box") || Shortcuts.CollidesWithLayer(collision, "Platform")) Destroy(gameObject);
    }

    void FixedUpdate()
    {
        timer--;
        if (timer <= 0) Destroy(gameObject);
    }
}

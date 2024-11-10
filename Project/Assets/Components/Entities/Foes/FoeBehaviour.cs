using UnityEngine;
using Glossary;

public class FoeBehaviour : EntityBehaviour
{
    protected bool right = false;
    protected int dirMultiplier = 0;

    protected override void Update()
    {
        base.Update();

        if (right) dirMultiplier = 1;
        else dirMultiplier = -1;

        transform.localScale = new (dirMultiplier, 1, 1);
    }

    protected void Walk()
    {
        directionX = dirMultiplier;

        if (grounded)
        {
            if (Shortcuts.GetPrecipiceRaycast(GetComponent<Collider2D>(), right)) right = !right;
            else if (right && Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.right, 0.9f, LayerMask.GetMask("Ground", "Box"))) right = false;
            else if (!right && Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.left, 0.9f, LayerMask.GetMask("Ground", "Box"))) right = true;
        } 
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (Shortcuts.CollidesWithLayer(collision, "Player")) collision.collider.GetComponent<EntityBehaviour>().ReceiveDamage(5, 0, transform.position);
        if (Shortcuts.CollidesWithLayer(collision, "Foe")) right = !right;
    }

    protected void SkipCollision(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}

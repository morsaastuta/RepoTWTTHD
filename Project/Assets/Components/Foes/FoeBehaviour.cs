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
            if (!Physics2D.Raycast(transform.position, Vector2.down, 0.05f, LayerMask.GetMask("Ground", "Platform", "Box"))) right = !right;
            else if (right && Physics2D.Raycast(transform.position + new Vector3(0, 1f, 0), Vector2.right, 0.9f, LayerMask.GetMask("Ground", "Box"))) right = false;
            else if (!right && Physics2D.Raycast(transform.position + new Vector3(0, 1f, 0), Vector2.left, 0.9f, LayerMask.GetMask("Ground", "Box"))) right = true;
        } 
    }
}

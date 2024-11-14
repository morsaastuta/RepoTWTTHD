using UnityEngine;

public class FoeBehaviour : EntityBehaviour
{
    protected bool right = true;

    protected int faceX = 0;
    protected int faceY = 0;

    protected bool targetDetected = false;
    protected Vector3 targetPos = new();

    protected override void Start()
    {
        base.Start();

        targetPos = transform.position;
    }

    protected override void Update()
    {
        base.Update();

        if (right) faceX = 1;
        else faceX = -1;

        transform.localScale = new Vector3(faceX, 1, 1);
    }

    protected void Walk()
    {
        directionX = faceX;

        if (Shortcuts.GetGrounded(GetComponent<Collider2D>(), lastPos))
        {
            if (Shortcuts.GetPrecipiceRaycast(GetComponent<Collider2D>(), right)) right = !right;
            else if (right && Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.right, 0.9f, LayerMask.GetMask("Ground", "Box"))) right = false;
            else if (!right && Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.left, 0.9f, LayerMask.GetMask("Ground", "Box"))) right = true;
        }
    }

    protected void Approach()
    {
        float dirX = 0;
        float dirY = 0;

        if (CheckDetection())
        {
            if (targetPos.x < transform.position.x) dirX = -1;
            else if (targetPos.x > transform.position.x) dirX = 1;

            if (targetPos.y < transform.position.y) dirY = -1;
            else if (targetPos.y > transform.position.y) dirY = 1;
        }

        directionX = dirX;
        directionY = dirY;
    }

    protected bool CheckDetection()
    {
        DetectorBehaviour detector = GetComponentInChildren<DetectorBehaviour>();
        targetDetected = detector.detected;

        if (targetDetected) targetPos = detector.targetPos;

        return targetDetected;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (Shortcuts.CollidesWithLayer(collision, "Foe")) right = !right;
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);

        if (Shortcuts.CollidesWithLayer(collision, "Player")) collision.collider.GetComponent<EntityBehaviour>().ReceiveDamage(5, 0, transform.position);
    }

    protected override void OnTriggerStay2D(Collider2D collider)
    {
        base.OnTriggerStay2D(collider);

        if (Shortcuts.CollidesWithLayer(collider, "Player")) collider.GetComponent<EntityBehaviour>().ReceiveDamage(5, 0, transform.position);
    }

    protected void SkipCollision(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
    }
}

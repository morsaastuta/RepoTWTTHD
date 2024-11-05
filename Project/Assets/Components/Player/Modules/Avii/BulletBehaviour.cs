using UnityEngine;

public class BulletBehaviour : CastBehaviour
{
    float speed = 30f;
    int timer = 200;

    override protected void Start()
    {
        cast = new Bullet();

        base.Start();
    }

    override protected void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (Shortcuts.CollidesWithLayer(collision, "Ground") || Shortcuts.CollidesWithLayer(collision, "Foe") || Shortcuts.CollidesWithLayer(collision, "Box") || Shortcuts.CollidesWithLayer(collision, "Platform")) Destroy(gameObject);
    }

    void Update()
    {
        timer--;
        if (timer <= 0) Destroy(gameObject);
    }

    public void SetObjective(Transform objective)
    {
        // Set angle
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, AngleBetweenPoints(objective.position, transform.position)));

        // Set linear velocity
        GetComponent<Rigidbody2D>().linearVelocity = (objective.position - transform.position).normalized * speed;
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}

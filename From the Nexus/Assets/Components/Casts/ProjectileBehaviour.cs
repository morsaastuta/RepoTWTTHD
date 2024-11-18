using UnityEngine;

public class ProjectileBehaviour : CastBehaviour
{
    float speed = 30f;
    int timer = 200;

    override protected void Start()
    {
        cast = new Projectile();

        base.Start();
    }

    override protected void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        if (Shortcuts.GetColliderLayer(collider, "Ground") || Shortcuts.GetColliderLayer(collider, "Foe") || Shortcuts.GetColliderLayer(collider, "Box") || Shortcuts.GetColliderLayer(collider, "Platform")) StartCoroutine(Shortcuts.DestroyAudibleObject(gameObject));
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

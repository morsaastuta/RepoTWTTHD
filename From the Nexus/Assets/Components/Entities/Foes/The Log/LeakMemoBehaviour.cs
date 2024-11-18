using UnityEngine;

public class LeakMemoBehaviour : FoeBehaviour
{
    [Header("Module: Drop")]
    [SerializeField] GameObject drop;
    [SerializeField] Transform origin;
    [SerializeField] float xForce = 0;
    [SerializeField] float yForce = 0;
    [SerializeField] int dropTimerMax = 200;
    int dropTimer = 200;


    protected override void Start()
    {
        entityCode = new LeakMemo();

        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (dropTimer > 0) {
            dropTimer--;
            if (dropTimer <= 0)
            {
                GameObject cast = Shortcuts.InstantiateCast(gameObject, Instantiate(drop), false);
                cast.transform.position = origin.position;
                cast.GetComponent<Rigidbody2D>().linearVelocityX = xForce;
                cast.GetComponent<Rigidbody2D>().linearVelocityY = yForce;

                dropTimer = dropTimerMax;
            }
        }
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        SkipCollision(collision);

        if (Shortcuts.GetCollisionLayer(collision, "Player")) collision.collider.GetComponent<EntityBehaviour>().ReceiveDamage(5, 10, transform.position);
    }
}

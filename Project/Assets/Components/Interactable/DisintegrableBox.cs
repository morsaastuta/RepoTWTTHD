using UnityEngine;

public class DisintegrableBox : MonoBehaviour
{
    [SerializeField] float durability = 1;
    [SerializeField] int height = 1;
    [SerializeField] int width = 1;

    int whiteTimerMax = 16;
    int whiteTimer = 0;

    void Awake()
    {
        GetComponent<Animator>().SetInteger("h", height);
        GetComponent<Animator>().SetInteger("w", width);
        GetComponent<BoxCollider2D>().size = new(width,height);
    }

    void Update()
    {
        if (whiteTimer > 0)
        {
            whiteTimer--;
            if (whiteTimer <= 0) GetComponent<SpriteRenderer>().color = Color.white;
        }
        if (durability <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Shortcuts.CollidesWithLayer(collision, "Cast"))
        {
            durability -= collision.collider.GetComponent<CastBehaviour>().GetCast().apm;
            GetComponent<SpriteRenderer>().color = new(0,1,1,1);
            whiteTimer = whiteTimerMax;
        }
    }
}

using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    [SerializeField] Animator box;

    [SerializeField] float durability = 1;
    [SerializeField] int height = 1;
    [SerializeField] int width = 1;

    int whiteTimerMax = 16;
    int whiteTimer = 0;

    void Awake()
    {
        box.SetInteger("h", height);
        box.SetInteger("w", width);
        GetComponent<BoxCollider2D>().size = new(width,height);
    }

    void Update()
    {
        if (whiteTimer > 0)
        {
            whiteTimer--;
            if (whiteTimer <= 0) GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Cast"))
        {
            durability -= collider.GetComponent<CastBehaviour>().GetCast().apm;
            GetComponent<SpriteRenderer>().color = new(0,1,1,1);
            whiteTimer = whiteTimerMax;
            if (durability <= 0) Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class ProtectedBoxBehaviour : MonoBehaviour
{
    [SerializeField] Animator box;
    [SerializeField] Animator shield;
    [SerializeField] WeakSideBehaviour weakSide;

    [SerializeField] public float durability = 1;
    [SerializeField] int height = 1;
    [SerializeField] int width = 1;

    int whiteTimerMax = 16;
    int whiteTimer = 0;

    void Awake()
    {
        box.SetInteger("h", height);
        box.SetInteger("w", width);
        shield.SetInteger("h", height);
        shield.SetInteger("w", width);
        GetComponent<BoxCollider2D>().size = new(width - 0.04f, height - 0.04f);
        weakSide.GetComponent<BoxCollider2D>().size = new(.05f, height - 0.99f);
        weakSide.GetComponent<BoxCollider2D>().offset = new(0.475f + ((width - 1) * 0.5f), 0);
    }

    void Update()
    {
        if (whiteTimer > 0)
        {
            whiteTimer--;
            if (whiteTimer <= 0) GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void Damage()
    {
        whiteTimer = whiteTimerMax;
        if (durability <= 0) Destroy(gameObject);
    }
}

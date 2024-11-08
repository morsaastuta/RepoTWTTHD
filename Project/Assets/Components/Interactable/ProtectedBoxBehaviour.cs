using UnityEngine;
using UnityEngine.UIElements;

public class ProtectedBoxBehaviour : MonoBehaviour
{
    [SerializeField] WeakSideBehaviour weakSide;

    [SerializeField] public float durability = 1;
    [SerializeField] int height = 1;
    [SerializeField] int width = 1;

    int whiteTimerMax = 16;
    int whiteTimer = 0;

    void Awake()
    {
        GetComponent<Animator>().SetInteger("h", height);
        GetComponent<Animator>().SetInteger("w", width);
        GetComponent<BoxCollider2D>().size = new(width,height);
        weakSide.GetComponent<BoxCollider2D>().size = new(.05f, height-0.1f);
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

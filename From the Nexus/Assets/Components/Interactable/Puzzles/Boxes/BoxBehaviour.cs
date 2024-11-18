using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float durability = 1;
    [SerializeField] int height = 1;
    [SerializeField] int width = 1;

    [Header("References")]
    [SerializeField] Animator box;
    [SerializeField] AudioSource sfxSource;

    int whiteTimerMax = 16;
    int whiteTimer = 0;

    void Awake()
    {
        box.SetInteger("h", height);
        box.SetInteger("w", width);
        GetComponent<BoxCollider2D>().size = new(width - 0.04f, height - 0.04f);
    }

    void Update()
    {
        if (whiteTimer > 0)
        {
            whiteTimer--;
            if (durability > 0 && whiteTimer <= 0) GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Cast"))
        {
            durability -= collider.GetComponent<CastBehaviour>().GetCast().apm;
            GetComponent<SpriteRenderer>().color = new(0,1,1,1);
            whiteTimer = whiteTimerMax;

            if (durability <= 0)
            {
                JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Box, false);
                StartCoroutine(Shortcuts.DestroyAudibleObject(gameObject));
            }
            else JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Hit, false);
        }
    }
}

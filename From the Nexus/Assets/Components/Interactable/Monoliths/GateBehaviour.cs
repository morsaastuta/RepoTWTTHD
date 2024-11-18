using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    [Header("Target Scene")]
    [SerializeField] int region;
    [SerializeField] int stage;
    [SerializeField] bool mainMenu;

    [Header("References")]
    [SerializeField] Animator frontAnimator;
    [SerializeField] Animator backAnimator;
    [SerializeField] Transform pointer;
    [SerializeField] AudioSource sfxSource;

    bool on = false;
    Collider2D player;

    void Start()
    {
        backAnimator.SetBool("open", false);
        backAnimator.SetBool("front", false);
        frontAnimator.SetBool("open", true);
        frontAnimator.SetBool("front", true);
    }

    void OnEnable()
    {
        backAnimator.SetBool("open", false);
        backAnimator.SetBool("front", false);
        frontAnimator.SetBool("open", true);
        frontAnimator.SetBool("front", true);
    }

    void Update()
    {
        if (on & Shortcuts.Pressed(GameManager.instance.interact))
        {
            on = false;
            JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Close, false);
            player.transform.position = pointer.position;
            LevelManager.instance.Cutscene(true);
            frontAnimator.SetBool("open", false);
        }

        if (frontAnimator.GetCurrentAnimatorStateInfo(0).IsName("end"))
        {
            // Upload score
            LevelManager.instance.UploadSM();

            GameManager.instance.ClearPointer();

            if (mainMenu)
            {
                PlayerPrefs.SetInt(Shortcuts.KEY_PROGRESS, 2);
                Shortcuts.GoToMenu();
            }
            else
            {
                // Establish current accessible levels on startup
                if (PlayerPrefs.GetInt(Shortcuts.KEY_LEVEL) < stage) PlayerPrefs.SetInt(Shortcuts.KEY_LEVEL, stage);

                // Load specified stage
                Shortcuts.LoadStage(region, stage);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Player"))
        {
            JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Open, false);
            player = collider;
            on = true;
            backAnimator.SetBool("open", true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Player"))
        {
            if (!sfxSource.isPlaying) JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Close, false);
            player = collider;
            on = false;
            backAnimator.SetBool("open", false);
        }
    }
}

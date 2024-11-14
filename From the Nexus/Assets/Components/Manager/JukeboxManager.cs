using UnityEngine;
using UnityEngine.SceneManagement;

public class JukeboxManager : MonoBehaviour
{
    public static JukeboxManager instance;

    #region BGM TRACKS
    [Header("BGM")]
    [SerializeField] AudioSource bgm;

    [SerializeField] AudioClip menu_f;
    [SerializeField] AudioClip menu_s;

    [SerializeField] AudioClip r00s00_f;
    [SerializeField] AudioClip r00s00_s;

    [SerializeField] AudioClip r01s01;
    [SerializeField] AudioClip r01s02;
    [SerializeField] AudioClip r01s03;
    #endregion

    #region SFX FILES
    [Header("SFX")]
    [SerializeField] AudioSource sfx;

    [SerializeField] AudioClip select;
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip pause;

    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip clash;
    [SerializeField] AudioClip respawn;

    [SerializeField] AudioClip connect;
    [SerializeField] AudioClip interact;
    [SerializeField] AudioClip collect;
    [SerializeField] AudioClip destroy;
    [SerializeField] AudioClip open;
    [SerializeField] AudioClip close;

    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip die;
    [SerializeField] AudioClip drop;

    [SerializeField] AudioClip shoot;
    #endregion

    public enum SFX
    {
        // UI
        Hover, Select, Pause,

        // Player
        Jump, Damage, Respawn,

        // Interactable
        Connect, Disconnect, Interact, Collect, Destroy, Open, Close,

        // Foe
        Hit, Die, Drop,

        // Modules
        Shoot,
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += Setup;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= Setup;
    }

    void Setup(Scene scene, LoadSceneMode mode)
    {
        StopBGM();
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (LevelManager.instance == null)
        {
            if (!PlayerPrefs.HasKey(Shortcuts.KEY_BEGUN)) PlayBGM(menu_f);
            else PlayBGM(menu_s);
        }
        else
        {
            switch (LevelManager.instance.region)
            {
                case 0:
                    // 00 - THE NEXUS
                    switch (LevelManager.instance.stage)
                    {
                        case 0: if (PlayerPrefs.HasKey(Shortcuts.KEY_BEGUN)) PlayBGM(r00s00_s); break;
                    }
                    break;

                case 1:
                    // 01 - THE LOG
                    switch (LevelManager.instance.stage)
                    {
                        case 1: PlayBGM(r01s01); break;
                        case 2: PlayBGM(r01s02); break;
                        case 3: PlayBGM(r01s03); break;
                    }
                    break;
            }
        }
    }

    public void PlayBGM(AudioClip track)
    {
        bgm.clip = track;
        bgm.Play();
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void PlaySFX(SFX file)
    {
        switch (file)
        {
            // UI
            case SFX.Hover: break;
            case SFX.Select: break;
            case SFX.Pause: break;

            // Player
            case SFX.Jump: break;
            case SFX.Damage: break;
            case SFX.Respawn: break;

            // Interactable
            case SFX.Connect: break;
            case SFX.Disconnect: break;
            case SFX.Interact: break;
            case SFX.Collect: break;
            case SFX.Destroy: break;
            case SFX.Open: break;
            case SFX.Close: break;

            // Foe
            case SFX.Hit: break;
            case SFX.Die: break;
            case SFX.Drop: break;

            // Modules
            case SFX.Shoot: break;
        }
    }
}

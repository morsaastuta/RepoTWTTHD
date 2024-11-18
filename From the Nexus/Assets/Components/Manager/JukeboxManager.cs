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
    [SerializeField] AudioClip menu_l;

    [SerializeField] AudioClip r00s00_f;
    [SerializeField] AudioClip r00s00_s;

    [SerializeField] AudioClip r01s01;
    [SerializeField] AudioClip r01s02;
    [SerializeField] AudioClip r01s03;
    #endregion

    #region SFX FILES
    [Header("SFX")]
    [SerializeField] AudioSource sfxUI;

    [SerializeField] AudioClip select;
    [SerializeField] AudioClip enterHover;
    [SerializeField] AudioClip exitHover;

    [SerializeField] AudioClip pause;

    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip clash;
    [SerializeField] AudioClip kill;
    [SerializeField] AudioClip respawn;

    [SerializeField] AudioClip connect;
    [SerializeField] AudioClip disconnect;

    [SerializeField] AudioClip read;
    [SerializeField] AudioClip save;
    [SerializeField] AudioClip open;
    [SerializeField] AudioClip close;

    [SerializeField] AudioClip box;
    [SerializeField] AudioClip branch;

    [SerializeField] AudioClip chip;

    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip shield;
    [SerializeField] AudioClip burst;

    [SerializeField] AudioClip drop;
    [SerializeField] AudioClip detect;
    [SerializeField] AudioClip call;
    #endregion

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnEnable()
    {
        SceneManager.sceneLoaded += Setup;
    }

    protected virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= Setup;
    }

    public enum SFX
    {
        // UI
        EnterHover, ExitHover, Select, Pause,

        // Entity
        Jump, Hit, Clash, Kill, Respawn,

        // Interactable
        Connect, Disconnect, Read, Save, Box, Branch, Collect, Open, Close,

        // Cast
        Shoot, Shield, Burst, Drop, Detect, Call
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
            switch (PlayerPrefs.GetInt(Shortcuts.KEY_PROGRESS))
            {
                case 0: PlayBGM(menu_f); break;
                case 1: PlayBGM(menu_s); break;
                case 2: PlayBGM(menu_l); break;
            }
        }
        else
        {
            switch (LevelManager.instance.region)
            {
                case 0:
                    // 00 - THE NEXUS
                    switch (LevelManager.instance.stage)
                    {
                        case 0: if (PlayerPrefs.GetInt(Shortcuts.KEY_PROGRESS) >= 1) PlayBGM(r00s00_s); break;
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
        if (bgm.clip != track || !bgm.isPlaying)
        {
            bgm.clip = track;
            bgm.Play();
        }
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void PlaySFX(AudioSource source, SFX file, bool l)
    {
        switch (file)
        {
            // UI
            case SFX.EnterHover: source.clip = enterHover; break;
            case SFX.ExitHover: source.clip = exitHover; break;
            case SFX.Select: source.clip = select; break;
            case SFX.Pause: source.clip = pause; break;

            // Player
            case SFX.Jump: source.clip = jump; break;
            case SFX.Hit: source.clip = hit; break;
            case SFX.Clash: source.clip = clash; break;
            case SFX.Kill: source.clip = kill; break;
            case SFX.Respawn: source.clip = respawn; break;

            // Interactablesource.clip = connect;
            case SFX.Connect: source.clip = connect; break;
            case SFX.Disconnect: source.clip = disconnect; break;
            case SFX.Read: source.clip = read; break;
            case SFX.Save: source.clip = save; break;
            case SFX.Open: source.clip = open; break;
            case SFX.Close: source.clip = close; break;
            case SFX.Box: source.clip = box; break;
            case SFX.Branch: source.clip = branch; break;
            case SFX.Collect: source.clip = chip; break;

            // Abilities
            case SFX.Shoot: source.clip = shoot; break;
            case SFX.Shield: source.clip = shield; break;
            case SFX.Burst: source.clip = burst; break;
            case SFX.Drop: source.clip = drop; break;
            case SFX.Detect: source.clip = detect; break;
            case SFX.Call: source.clip = call; break;
        }

        source.loop = l;
        source.Play();
    }

    public void PlayUI(SFX sfx)
    {
        PlaySFX(sfxUI, sfx, false);
    }
}

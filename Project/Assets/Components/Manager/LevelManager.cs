using UnityEngine;
using Glossary;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool canPause = true;

    [Header("Global references")]
    [SerializeField] int region;
    [SerializeField] int scene;
    [SerializeField] PlayerBehaviour player;
    [SerializeField] Transform pointer;
    [SerializeField] GameObject foeSource;

    [Header("Local references")]
    [SerializeField] MenuController menuHUD;
    [SerializeField] MessageController messageHUD;

    #region INPUT ACTION REFERENCES
    [Header("Inputs")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference interact;
    [SerializeField] InputActionReference attack;
    [SerializeField] InputActionReference pause;
    #endregion

    #region MODULES
    [Header("Modules")]
    [SerializeField] GameObject m_projectile;
    [SerializeField] GameObject m_shield;
    [SerializeField] GameObject m_cleanse;
    #endregion

    #region PROPERTIES
    public int Region { get; }
    public int Scene { get; }
    public PlayerBehaviour Player { get; }
    public Transform Pointer { get; set; }
    public MenuController MenuHUD { get;  }
    public MessageController MessageHUD { get; }
    public GameObject M_projectile { get; }
    public GameObject M_shield { get; }
    public GameObject M_cleanse { get; }
    public InputActionReference Move { get; set; }
    public InputActionReference Jump { get; set; }
    public InputActionReference Interact { get; set; }
    public InputActionReference Attack { get; set; }
    public InputActionReference Pause { get; set; }
    #endregion

    float storedSM;

    void Awake()
    {
        instance = this;
        Pointer = pointer;
        Move = move;
        Jump = jump;
        Interact = interact;
        Attack = attack;
        Pause = pause;
    }

    void Start()
    {
        // If the user ever played the Tutorial
        if (!PlayerPrefs.HasKey(Shortcuts.KEY_BEGUN)) PlayerPrefs.SetInt(Shortcuts.KEY_BEGUN, 0);

        // Add modules from the beginning if the player already has them, and eliminate
        Transform modBase = player.GetComponentInChildren<ModuleBaseBehaviour>().transform;

        if (PlayerPrefs.GetInt(Shortcuts.KEY_MOD_PROJECTILE).Equals(1)) Instantiate(m_projectile, modBase);
        if (PlayerPrefs.GetInt(Shortcuts.KEY_MOD_SHIELD).Equals(1)) Instantiate(m_projectile, modBase);
        if (PlayerPrefs.GetInt(Shortcuts.KEY_MOD_CLEANSE).Equals(1)) Instantiate(m_projectile, modBase);
    }

    public void StoreSM(float sm)
    {
        storedSM += sm;
    }

    public void UploadSM()
    {
        PlayerPrefs.SetFloat(Shortcuts.KEY_SCORE, PlayerPrefs.GetFloat(Shortcuts.KEY_SCORE) + storedSM);
    }

    public void RestartScene()
    {
        Shortcuts.LoadScene(region, scene);
    }

    public void SetPause(bool on)
    {
        player.gameObject.SetActive(!on);
        foeSource.SetActive(!on);
    }

    public void Cutscene(bool on)
    {
        canPause = !on;
        if (on) player.entityCode.AddState(State.Disconnected);
        else player.entityCode.RemoveState(State.Disconnected);
    }
}

using UnityEngine;
using Glossary;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool canPause;

    [Header("Global references")]
    [SerializeField] public int region;
    [SerializeField] public int stage;
    [SerializeField] public PlayerBehaviour player;
    [SerializeField] public Transform pointer;
    [SerializeField] GameObject foeSource;

    [Header("Local references")]
    [SerializeField] public MenuController menuHUD;
    [SerializeField] public MessageController messageHUD;

    #region INPUT ACTION REFERENCES
    [Header("Inputs")]
    [SerializeField] public InputActionReference move;
    [SerializeField] public InputActionReference jump;
    [SerializeField] public InputActionReference interact;
    [SerializeField] public InputActionReference attack;
    [SerializeField] public InputActionReference pause;
    #endregion

    #region MODULES
    [Header("Modules")]
    [SerializeField] public GameObject m_projectile;
    [SerializeField] public GameObject m_shield;
    [SerializeField] public GameObject m_cleanse;
    #endregion

    float storedSM;
    Vector3 playerVelOnPause;
    List<Vector3> foeVelOnPause = new();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Add modules from the beginning if the player already has them, and eliminate
        if (PlayerPrefs.GetInt(Shortcuts.KEY_MOD_PROJECTILE).Equals(1)) InstantiateModule(Mod.Projectile);
        if (PlayerPrefs.GetInt(Shortcuts.KEY_MOD_SHIELD).Equals(1)) InstantiateModule(Mod.Shield);
        if (PlayerPrefs.GetInt(Shortcuts.KEY_MOD_CLEANSE).Equals(1)) InstantiateModule(Mod.Cleanse);
    }

    public void StoreSM(float sm)
    {
        storedSM += sm;
    }

    public void ClearSM()
    {
        storedSM = 0;
    }

    public void UploadSM()
    {
        PlayerPrefs.SetFloat(Shortcuts.KEY_SCORE, PlayerPrefs.GetFloat(Shortcuts.KEY_SCORE) + storedSM);
    }

    public void RestartScene()
    {
        Shortcuts.LoadScene(region, stage);
    }

    public void SetPause(bool on)
    {
        if (on)
        {
            playerVelOnPause = player.GetComponent<Rigidbody2D>().linearVelocity;

            foreach (FoeBehaviour foe in foeSource.GetComponentsInChildren<FoeBehaviour>()) foeVelOnPause.Add(foe.GetComponent<Rigidbody2D>().linearVelocity);
        }

        player.gameObject.SetActive(!on);
        foeSource.SetActive(!on);

        if (!on)
        {
            player.GetComponent<Rigidbody2D>().linearVelocity = playerVelOnPause;

            foreach (FoeBehaviour foe in foeSource.GetComponentsInChildren<FoeBehaviour>())
            {
                foe.GetComponent<Rigidbody2D>().linearVelocity = foeVelOnPause[0];
                foeVelOnPause.Remove(foeVelOnPause[0]);
            }
        }

    }

    public void Cutscene(bool on)
    {
        canPause = !on;
        if (on) player.entityCode.AddState(State.Disconnected);
        else player.entityCode.RemoveState(State.Disconnected);
    }

    public void InstantiateModule(Mod mod)
    {
        Transform modBase = player.GetComponentInChildren<ModuleBaseBehaviour>().transform;

        switch (mod)
        {
            case Mod.Projectile: Instantiate(m_projectile, modBase); break;
            case Mod.Shield: Instantiate(m_shield, modBase); break;
            case Mod.Cleanse: Instantiate(m_cleanse, modBase); break;
        }
    }
}

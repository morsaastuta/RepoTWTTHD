using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject bgStart;
    [SerializeField] GameObject bgTheNexus;
    [SerializeField] GameObject bgTheLog;

    void Awake()
    {
        Cursor.visible = true;

        if (!PlayerPrefs.HasKey(Shortcuts.KEY_PROGRESS))
        {
            PlayerPrefs.SetInt(Shortcuts.KEY_PROGRESS, 0);
            PlayerPrefs.SetFloat(Shortcuts.KEY_SCORE, 0);
            PlayerPrefs.SetInt(Shortcuts.KEY_LEVEL, 0);

            PlayerPrefs.SetInt(Shortcuts.KEY_MOD_PROJECTILE, 0);
            PlayerPrefs.SetInt(Shortcuts.KEY_MOD_SHIELD, 0);
            PlayerPrefs.SetInt(Shortcuts.KEY_MOD_CLEANSE, 0);
        }
    }

    void Start()
    {
        bgStart.SetActive(false);
        bgTheNexus.SetActive(false);
        bgTheLog.SetActive(false);

        switch (PlayerPrefs.GetInt(Shortcuts.KEY_PROGRESS))
        {
            case 0: bgStart.SetActive(true); break;
            case 1: bgTheNexus.SetActive(true); break;
            case 2: bgTheLog.SetActive(true); break;
        }
    }
}

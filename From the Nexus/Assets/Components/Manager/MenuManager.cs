using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey(Shortcuts.KEY_SCORE))
        {
            PlayerPrefs.SetFloat(Shortcuts.KEY_SCORE, 0);
            PlayerPrefs.SetInt(Shortcuts.KEY_LEVEL, 0);

            PlayerPrefs.SetInt(Shortcuts.KEY_MOD_PROJECTILE, 0);
            PlayerPrefs.SetInt(Shortcuts.KEY_MOD_SHIELD, 0);
            PlayerPrefs.SetInt(Shortcuts.KEY_MOD_CLEANSE, 0);
        }
    }
}

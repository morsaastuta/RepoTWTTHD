using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneController : MonoBehaviour
{
    [Header("Shared Memory (Score)")]
    [SerializeField] TextMeshProUGUI sm;

    [Header("Unlockable levels")]
    [SerializeField] Button b1;
    [SerializeField] Button b2;
    [SerializeField] Button b3;

    void Awake()
    {
        b1.interactable = false;
        b2.interactable = false;
        b3.interactable = false;
    }

    void OnEnable()
    {
        sm.text = PlayerPrefs.GetFloat(Shortcuts.KEY_SCORE).ToString("0.0");
        if (PlayerPrefs.GetInt(Shortcuts.KEY_LEVEL) > 0)
        {
            b1.interactable = true;
            if (PlayerPrefs.GetInt(Shortcuts.KEY_LEVEL) > 1)
            {
                b2.interactable = true;
                if (PlayerPrefs.GetInt(Shortcuts.KEY_LEVEL) > 2) b3.interactable = true;
            }
        }
    }

    void OnDisable()
    {
        b1.interactable = false;
        b2.interactable = false;
        b3.interactable = false;
    }

    public void LoadScene00(int level)
    {
        Shortcuts.LoadStage(0, level);
    }

    public void LoadScene01(int level)
    {
        Shortcuts.LoadStage(1, level);
    }
}

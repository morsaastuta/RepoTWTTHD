using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject paneMain;
    [SerializeField] GameObject paneGuide;
    [SerializeField] GameObject paneSelect;
    [SerializeField] GameObject paneCredits;
    [SerializeField] Button levelSelect;

    void Start()
    {
        if (PlayerPrefs.GetInt(Shortcuts.KEY_PROGRESS) <= 0) levelSelect.interactable = false;
    }

    public void Begin()
    {
        Shortcuts.LoadStage(0, 0);
    }

    public void LoadScene()
    {
        paneMain.SetActive(false);
        paneSelect.SetActive(true);
    }

    public void Guide(bool on)
    {
        paneGuide.SetActive(on);
        paneMain.SetActive(!on);
    }

    public void Credits(bool on)
    {
        paneCredits.SetActive(on);
        paneMain.SetActive(!on);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        paneSelect.SetActive(false);
        paneMain.SetActive(true);
    }
}

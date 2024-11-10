using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject paneMain;
    [SerializeField] GameObject paneSelect;
    [SerializeField] Button levelSelect;

    void Start()
    {
        if (!PlayerPrefs.HasKey(Shortcuts.KEY_BEGUN)) levelSelect.interactable = false;
    }

    public void Begin()
    {
        Shortcuts.LoadScene(0, 0);
    }

    public void LoadScene()
    {
        paneMain.SetActive(false);
        paneSelect.SetActive(true);
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

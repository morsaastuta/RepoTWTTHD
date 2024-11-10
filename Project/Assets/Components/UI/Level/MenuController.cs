using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject pane;

    void Update()
    {
        if (Shortcuts.Pressed(LevelManager.instance.Pause) && LevelManager.instance.canPause)
        {
            if (!pane.activeInHierarchy) Open();
            else Close();
        }
    }

    void Open()
    {
        LevelManager.instance.SetPause(true);
        pane.SetActive(true);
    }

    void Close()
    {
        pane.SetActive(false);
        LevelManager.instance.SetPause(false);
    }

    public void Restart()
    {
        LevelManager.instance.RestartScene();
    }

    public void MainMenu()
    {
        Shortcuts.GoToMenu();
    }
}

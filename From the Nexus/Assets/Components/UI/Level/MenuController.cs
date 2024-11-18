using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject pausePane;
    [SerializeField] GameObject guidePane;

    void Update()
    {
        if (Shortcuts.Pressed(GameManager.instance.pause) && LevelManager.instance.canPause)
        {
            if (!pausePane.activeInHierarchy && !guidePane.activeInHierarchy) Open();
            else if (!guidePane.activeInHierarchy) Close();
        }
    }

    void Open()
    {
        LevelManager.instance.SetPause(true);
        pausePane.SetActive(true);
    }

    void Close()
    {
        pausePane.SetActive(false);
        LevelManager.instance.SetPause(false);
    }

    public void Restart()
    {
        LevelManager.instance.RestartStage();
    }

    public void Guide(bool on)
    {
        pausePane.SetActive(!on);
        guidePane.SetActive(on);
    }

    public void MainMenu()
    {
        Shortcuts.GoToMenu();
    }
}

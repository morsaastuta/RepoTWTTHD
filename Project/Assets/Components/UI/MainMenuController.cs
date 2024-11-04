using UnityEditor.SearchService;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject buttonBegin;
    [SerializeField] GameObject butonLoad;
    [SerializeField] GameObject buttonExit;

    void Start()
    {
        title.transform.localPosition = new(0, Screen.height / 2 - 1 * Screen.height / 5);
        title.transform.localScale *= Screen.width / 1920;

        buttonBegin.transform.localPosition = new(0, Screen.height / 2 - 2 * Screen.height / 5);
        butonLoad.transform.localPosition = new(0, Screen.height / 2 - 3 * Screen.height / 5);
        buttonExit.transform.localPosition = new(0, Screen.height / 2 - 4 * Screen.height / 5);
    }

    public void Begin()
    {
        Shortcuts.LoadLevel(0, 0);
    }
}

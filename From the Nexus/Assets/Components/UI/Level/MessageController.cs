using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject pane;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] AudioSource sfxSource;
    List<string> content = new();
    public int index = 0;
    bool init = false;

    void LateUpdate()
    {
        if (Shortcuts.Pressed(GameManager.instance.interact) && !init) NextString();
        init = false;
    }

    public void ReceiveMessage(List<string> message)
    {
        init = true;
        GameObject.Find("Player").GetComponent<PlayerBehaviour>().EnterInteraction(true);
        content.Clear();
        content.AddRange(message);
        index = 0;
        pane.SetActive(true);
        NextString();
    }

    public bool NextString()
    {
        if (index >= content.Count)
        {
            if (pane.activeInHierarchy) DismissMessage();
            return true;
        }
        else
        {
            JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Read, false);
            text.SetText(content[index]);
            index++;
            return false;
        }
    }

    public void DismissMessage()
    {
        JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Read, false);
        pane.SetActive(false);
        index = 0;
        content.Clear();
        GameObject.Find("Player").GetComponent<PlayerBehaviour>().EnterInteraction(false);
    }

    public bool Active()
    {
        return index > 0;
    }
}

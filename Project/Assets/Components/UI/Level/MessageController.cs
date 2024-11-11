using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    [SerializeField] GameObject pane;
    [SerializeField] TextMeshProUGUI text;
    List<string> content = new();
    int index = 0;
    bool init = false;

    void LateUpdate()
    {
        if (Shortcuts.Pressed(LevelManager.instance.interact) && !init) NextString();
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
            DismissMessage();
            return true;
        }
        else
        {
            text.SetText(content[index]);
            index++;
            return false;
        }
    }

    public void DismissMessage()
    {
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

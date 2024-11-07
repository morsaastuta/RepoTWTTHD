using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    [SerializeField] GameObject root;
    [SerializeField] TextMeshProUGUI text;
    List<string> content = new();
    int index = 0;

    public void ReceiveMessage(List<string> message)
    {
        content.Clear();
        content.AddRange(message);
        index = 0;
        root.SetActive(true);
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
        root.SetActive(false);
        index = 0;
        content.Clear();
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    int index;

    private void Start()
    {
        List<Button> buttons = new(transform.parent.GetComponentsInChildren<Button>());
        index = buttons.IndexOf(GetComponent<Button>()) + 1;

        GetComponentInChildren<TMP_Text>().text = "Level " + index;
    }

    public void SelectLevel()
    {
        GameManager.Instance.ChangeScene("Level" + index);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorController : MonoBehaviour
{
    [SerializeField] int numOfLevels;
    [SerializeField] GameObject levelButtonPrefab;

    void Start()
    {
        for (int i = 0; i < numOfLevels; i++)
        {
            GameObject newButton = Instantiate(levelButtonPrefab);
            newButton.transform.parent = GetComponentInChildren<GridLayoutGroup>().transform;
        }

        int unlockedLevels = PlayerPrefs.GetInt(GameKeys.PREFS_MAXLEVEL);

        foreach (Button b in GetComponentsInChildren<Button>())
        {
            if (unlockedLevels <= 0) break;
            else unlockedLevels--;

            b.interactable = true;
        }
    }
}

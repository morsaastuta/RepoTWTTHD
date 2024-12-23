using UnityEngine;

public class TriggerCheckpointBehaviour : MonoBehaviour
{
    [SerializeField] Transform pointer;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Player"))
        {
            LevelManager.instance.pointer = pointer;

            // The first time the player crosses the pipe at 00_00
            if (PlayerPrefs.GetInt(Shortcuts.KEY_PROGRESS) <= 0) PlayerPrefs.SetInt(Shortcuts.KEY_PROGRESS, 1);
        }
    }
}

using UnityEngine;

public class TriggerCheckpointBehaviour : MonoBehaviour
{
    [SerializeField] Transform pointer;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            LevelManager.instance.pointer = pointer;

            // The first time the player crosses the pipe at 00_00, activate KEY_BEGUN
            if (!PlayerPrefs.HasKey(Shortcuts.KEY_BEGUN)) PlayerPrefs.SetInt(Shortcuts.KEY_BEGUN, 0);
        }
    }
}

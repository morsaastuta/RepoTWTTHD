using UnityEngine;

public class TriggerTrackBehaviour : MonoBehaviour
{
    [Header("Generic")]
    [SerializeField] bool stop;
    [SerializeField] AudioClip customTrack;

    void OnTriggerEnter2D(Collider2D collider)
    {        
        if (Shortcuts.GetColliderLayer(collider, "Player"))
        {
            if (!stop)
            {
                if (customTrack == null) JukeboxManager.instance.PlayBGM();
                else JukeboxManager.instance.PlayBGM(customTrack);
            }
            else JukeboxManager.instance.StopBGM();
        }
    }
}

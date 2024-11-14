using UnityEngine;

public class TriggerTrackBehaviour : MonoBehaviour
{
    [Header("Generic")]
    [SerializeField] bool stop;
    [SerializeField] AudioClip customTrack;

    [Header("Beginning")]
    [SerializeField] bool beginning;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (beginning && PlayerPrefs.HasKey(Shortcuts.KEY_BEGUN)) return;  
        
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            if (!stop)
            {
                if (customTrack is null) JukeboxManager.instance.PlayBGM();
                else JukeboxManager.instance.PlayBGM(customTrack);
            }
            else JukeboxManager.instance.StopBGM();
        }
    }
}

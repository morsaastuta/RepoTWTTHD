using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public void Morsa()
    {
        JukeboxManager.instance.PlayUI(JukeboxManager.SFX.Select);
        Application.OpenURL("https://morsaastuta.itch.io/");
    }

    public void Jetdarc()
    {
        JukeboxManager.instance.PlayUI(JukeboxManager.SFX.Select);
        Application.OpenURL("https://www.jetdarc.com/");
    }

    public void LRSF()
    {
        JukeboxManager.instance.PlayUI(JukeboxManager.SFX.Select);
        Application.OpenURL("https://soundcloud.com/littlerobotsoundfactory");
    }

    public void Jalastram()
    {
        JukeboxManager.instance.PlayUI(JukeboxManager.SFX.Select);
        Application.OpenURL("https://freesound.org/people/jalastram/");
    }
}

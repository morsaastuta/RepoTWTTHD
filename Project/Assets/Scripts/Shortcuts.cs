using UnityEngine;
using UnityEngine.SceneManagement;

public static class Shortcuts
{
    public static void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadLevel(int area, int level)
    {
        SceneManager.LoadScene(area.ToString("00") + "." + level.ToString("00"));
    }

    public static bool CollidesWithLayer(Collision2D collision, string layerName)
    {
        return collision.collider.gameObject.layer == LayerMask.NameToLayer(layerName);
    }

    public static GameObject InstantiateCast(GameObject source, GameObject cast, bool fromPlayer)
    {
        if (fromPlayer) cast.GetComponent<CastBehaviour>().SetSource(source.GetComponentInParent<PlayerBehaviour>().entityCode);
        else cast.GetComponent<CastBehaviour>().SetSource(source.GetComponent<FoeBehaviour>().entityCode);

        return cast;
    }
}

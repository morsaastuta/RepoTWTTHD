using UnityEngine;
using UnityEngine.InputSystem;
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

    public static bool CollidesWithLayer(Collider2D collider, string layerName)
    {
        return collider.gameObject.layer == LayerMask.NameToLayer(layerName);
    }

    public static GameObject InstantiateCast(GameObject source, GameObject cast, bool fromPlayer)
    {
        if (fromPlayer) cast.GetComponent<CastBehaviour>().SetSource(source.GetComponentInParent<EntityBehaviour>().entityCode);
        else cast.GetComponent<CastBehaviour>().SetSource(source.GetComponent<EntityBehaviour>().entityCode);

        return cast;
    }

    public static bool Pressed(InputActionReference input)
    {
        return input.action.triggered;
    }
}

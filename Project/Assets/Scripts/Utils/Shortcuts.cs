using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public static class Shortcuts
{
    #region VARIABLES
    public const string KEY_BEGUN = "begun";
    public const string KEY_SCORE = "score";
    public const string KEY_LEVEL = "level";
    public const string KEY_MOD_PROJECTILE = "m_projectile";
    public const string KEY_MOD_SHIELD = "m_shield";
    public const string KEY_MOD_CLEANSE = "m_cleanse";
    #endregion

    #region METHODS
    public static void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadScene(int region, int scene)
    {
        SceneManager.LoadScene(region.ToString("00") + "." + scene.ToString("00"));
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

    public static bool GetGroundRaycast(Collider2D collider)
    {
        return Physics2D.Raycast(collider.transform.position + new Vector3(collider.bounds.size.x / 2 - 0.04f, 0, 0), Vector2.down, 0.04f, LayerMask.GetMask("Ground", "Platform", "Box")) || Physics2D.Raycast(collider.transform.position - new Vector3(collider.bounds.size.x / 2 + 0.04f, 0, 0), Vector2.down, 0.04f, LayerMask.GetMask("Ground", "Platform", "Box"));
    }

    public static bool GetPrecipiceRaycast(Collider2D collider, bool right)
    {
        if (right) return !Physics2D.Raycast(collider.transform.position + new Vector3(collider.bounds.size.x / 2 - 0.04f, 0, 0), Vector2.down, 0.04f, LayerMask.GetMask("Ground", "Platform", "Box"));
        else return !Physics2D.Raycast(collider.transform.position - new Vector3(collider.bounds.size.x / 2 + 0.04f, 0, 0), Vector2.down, 0.04f, LayerMask.GetMask("Ground", "Platform", "Box"));
    }
    #endregion
}

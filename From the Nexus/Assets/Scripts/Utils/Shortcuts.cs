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

    public static void LoadScene(int region, int stage)
    {
        SceneManager.LoadScene(region.ToString("00") + "." + stage.ToString("00"));
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

    public static bool GetGrounded(Collider2D collider, Vector3 lastPos)
    {
        Transform entity = collider.transform;

        // Elevate player on platform detection (level assurance)
        if (Physics2D.Raycast(entity.position, entity.position - lastPos, Vector3.Distance(entity.position, lastPos), LayerMask.GetMask("Platform")))
        {
            entity.position = new Vector3(entity.position.x, entity.position.y + 0.01f);
            collider.attachedRigidbody.linearVelocityY = 0;
        }

        /*
        if (Physics2D.Raycast(entity.position + new Vector3(collider.bounds.size.x / 2 - 0.01f, 0, 0), Vector2.down, 0.01f, LayerMask.GetMask("Platform")) ||
            Physics2D.Raycast(entity.position - new Vector3(collider.bounds.size.x / 2 - 0.01f, 0, 0), Vector2.down, 0.01f, LayerMask.GetMask("Platform")))
            entity.position = new Vector3(entity.position.x, entity.position.y + 0.01f);
        */

        // Return GROUNDED
        return
            Physics2D.Raycast(entity.position + new Vector3(collider.bounds.size.x / 2 - 0.02f, 0, 0), Vector2.down, 0.03f, LayerMask.GetMask("Ground", "Platform", "Box")) ||
            Physics2D.Raycast(entity.position - new Vector3(collider.bounds.size.x / 2 - 0.02f, 0, 0), Vector2.down, 0.03f, LayerMask.GetMask("Ground", "Platform", "Box"));
    }

    public static bool GetPrecipiceRaycast(Collider2D collider, bool right)
    {
        if (right) return !Physics2D.Raycast(collider.transform.position + new Vector3(collider.bounds.size.x / 2 + 0.2f, 0, 0), Vector2.down, 0.1f, LayerMask.GetMask("Ground", "Platform", "Box"));
        else return !Physics2D.Raycast(collider.transform.position - new Vector3(collider.bounds.size.x / 2 + 0.2f, 0, 0), Vector2.down, 0.1f, LayerMask.GetMask("Ground", "Platform", "Box"));
    }

    public static void NullifyMovement(EntityBehaviour entity)
    {
        entity.GetComponent<Rigidbody2D>().linearVelocityX = 0;
    }
    #endregion
}
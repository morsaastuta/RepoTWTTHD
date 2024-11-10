using UnityEngine;
using Glossary;
using System.Collections.Generic;

public class ModuleChipBehaviour : MonoBehaviour
{
    [Header("Module")]
    [SerializeField] Mod mod;

    [Header("References")]
    [SerializeField] SpriteRenderer icon;

    #region SPRITES
    [Header("Resources")]
    [SerializeField] Sprite projectileSpr;
    [SerializeField] Sprite shieldSpr;
    [SerializeField] Sprite cleanseSpr;
    #endregion

    List<string> message = new();
    GameObject modObj;

    void Start()
    {
        switch (mod)
        {
            case Mod.Projectile:
                icon.sprite = projectileSpr;
                message.AddRange(new[]{"AVII obtained the PROJECTILE module.", "Use [Left Click] to shoot projectiles in the direction desired to destroy obstacles.", "VM usage: 3"});
                modObj = LevelManager.instance.M_projectile;
                break;
            case Mod.Shield:
                icon.sprite = shieldSpr;
                message.AddRange(new[]{"AVII obtained the SHIELD module.", "", "VM usage: 8"});
                break;
            case Mod.Cleanse:
                icon.sprite = cleanseSpr;
                message.AddRange(new[]{"AVII obtained the CLEANSE module.", "", "VM usage: 15"});
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            Instantiate(modObj, collider.GetComponentInChildren<ModuleBaseBehaviour>().transform);
            GameObject.Find("Text HUD").GetComponent<MessageController>().ReceiveMessage(message);
            Destroy(gameObject);
        }
    }
}

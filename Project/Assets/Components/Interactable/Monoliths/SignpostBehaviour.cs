using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SignpostBehaviour : MonoBehaviour
{
    [Header("Message")]
    [SerializeField] public List<string> content;

    [Header("References")]
    [SerializeField] Animator signAnimator;
    [SerializeField] Animator textAnimator;

    [Header("Resources")]
    [SerializeField] AnimationClip checkClip;

    bool on = false;
    Collider2D player;

    void Start()
    {
        signAnimator.SetBool("on", false);
        textAnimator.SetBool("on", false);
    }

    void Update()
    {
        if (on && Shortcuts.Pressed(LevelManager.instance.interact) && !LevelManager.instance.messageHUD.Active()) LevelManager.instance.messageHUD.ReceiveMessage(content);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            player = collider;
            on = true;
            signAnimator.SetBool("on", true);
            textAnimator.SetBool("on", true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            player = collider;
            on = false;
            signAnimator.SetBool("on", false);
            textAnimator.SetBool("on", false);
        }
    }

    public bool IsOn()
    {
        return textAnimator.runtimeAnimatorController.animationClips.Equals(checkClip);
    }
}

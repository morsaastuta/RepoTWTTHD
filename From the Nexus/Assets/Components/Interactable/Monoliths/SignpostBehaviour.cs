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
    [SerializeField] AudioSource sfxSource;

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
        if (on && Shortcuts.Pressed(GameManager.instance.interact) && !LevelManager.instance.messageHUD.Active()) LevelManager.instance.messageHUD.ReceiveMessage(content);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Player"))
        {
            JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Connect, false);
            player = collider;
            on = true;
            signAnimator.SetBool("on", true);
            textAnimator.SetBool("on", true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Player"))
        {
            if (!sfxSource.isPlaying) JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Disconnect, false);
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

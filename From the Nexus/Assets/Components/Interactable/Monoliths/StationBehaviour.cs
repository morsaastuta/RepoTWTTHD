using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StationBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator signAnimator;
    [SerializeField] Animator textAnimator;
    [SerializeField] Animator dataAnimator;
    [SerializeField] Transform pointer;
    [SerializeField] AudioSource sfxSource;

    [Header("Resources")]
    [SerializeField] AnimationClip checkClip;

    bool on = false;
    Collider2D player;


    void Start()
    {
        signAnimator.SetBool("on", false);
        textAnimator.SetBool("on", false);
        dataAnimator.SetBool("on", false);
    }

    void Update()
    {
        if (on && Shortcuts.Pressed(GameManager.instance.interact)) StoreData();
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
            if (LevelManager.instance.pointer.Equals(pointer)) dataAnimator.SetBool("on", true);
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
            dataAnimator.SetBool("on", false);
        }
    }

    void StoreData()
    {
        JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Save, false);
        LevelManager.instance.pointer = pointer;
        player.GetComponent<PlayerBehaviour>().entityCode.ClearMemory();
        dataAnimator.SetBool("on", true);
        LevelManager.instance.UploadSM();
    }

    public bool IsOn()
    {
        return textAnimator.runtimeAnimatorController.animationClips.Equals(checkClip);
    }
}

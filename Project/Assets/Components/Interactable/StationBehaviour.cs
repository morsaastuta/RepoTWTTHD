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

    [Header("Resources")]
    [SerializeField] AnimationClip checkClip;

    bool isTrigger = false;
    Collider2D player;


    void Start()
    {
        signAnimator.SetBool("on", false);
        textAnimator.SetBool("on", false);
        dataAnimator.SetBool("on", false);
    }

    void Update()
    {
        if (isTrigger && Shortcuts.Pressed(LevelManager.instance.Interact)) StoreData();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            player = collider;
            isTrigger = true;
            signAnimator.SetBool("on", true);
            textAnimator.SetBool("on", true);
            if (LevelManager.instance.Pointer.Equals(pointer)) dataAnimator.SetBool("on", true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            player = collider;
            isTrigger = false;
            signAnimator.SetBool("on", false);
            textAnimator.SetBool("on", false);
            dataAnimator.SetBool("on", false);
        }
    }

    void StoreData()
    {
        LevelManager.instance.Pointer = pointer;
        player.GetComponent<PlayerBehaviour>().entityCode.ClearMemory();
        dataAnimator.SetBool("on", true);
        LevelManager.instance.UploadSM();
    }

    public bool IsOn()
    {
        return textAnimator.runtimeAnimatorController.animationClips.Equals(checkClip);
    }
}

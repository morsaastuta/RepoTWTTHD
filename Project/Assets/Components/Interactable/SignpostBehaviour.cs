using System.Collections.Generic;
using UnityEngine;

public class SignpostBehaviour : MonoBehaviour
{
    [SerializeField] Animator signAnimator;
    [SerializeField] Animator textAnimator;
    [SerializeField] AnimationClip checkClip;
    [SerializeField] public List<string> content;

    void Start()
    {
        signAnimator.SetTrigger("off");
        textAnimator.SetTrigger("off");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            signAnimator.SetTrigger("on");
            textAnimator.SetTrigger("on");
        }
    }

    /*
    void OnTriggerExit2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            signAnimator.SetTrigger("off");
            textAnimator.SetTrigger("off");
        }
    }
    */

    public bool IsOn()
    {
        return textAnimator.runtimeAnimatorController.animationClips.Equals(checkClip);
    }
}

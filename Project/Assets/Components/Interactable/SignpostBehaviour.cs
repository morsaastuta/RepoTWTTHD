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
        signAnimator.SetBool("on", false);
        textAnimator.SetBool("on", false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            signAnimator.SetBool("on", true);
            textAnimator.SetBool("on", true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            signAnimator.SetBool("on", false);
            textAnimator.SetBool("on", false);
        }
    }

    public bool IsOn()
    {
        return textAnimator.runtimeAnimatorController.animationClips.Equals(checkClip);
    }
}

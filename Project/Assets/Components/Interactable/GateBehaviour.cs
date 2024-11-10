using Glossary;
using UnityEngine;
using UnityEngine.InputSystem;

public class GateBehaviour : MonoBehaviour
{
    [Header("Target Scene")]
    [SerializeField] int region;
    [SerializeField] int scene;

    [Header("References")]
    [SerializeField] Animator frontAnimator;
    [SerializeField] Animator backAnimator;
    [SerializeField] Transform pointer;

    bool isTrigger = false;
    Collider2D player;

    void Start()
    {
        backAnimator.SetBool("open", false);
        backAnimator.SetBool("front", false);
        frontAnimator.SetBool("open", true);
        frontAnimator.SetBool("front", true);
    }

    void Update()
    {
        if (isTrigger & Shortcuts.Pressed(LevelManager.instance.Interact))
        {
            player.transform.position = pointer.position;
            player.GetComponent<Rigidbody2D>().linearVelocityX = 0;
            LevelManager.instance.Cutscene(true);
            frontAnimator.SetBool("open", false);
        }

        if (frontAnimator.GetCurrentAnimatorStateInfo(0).IsName("end")) Shortcuts.LoadScene(region, scene);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            player = collider;
            isTrigger = true;
            backAnimator.SetBool("open", true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            player = collider;
            isTrigger = false;
            backAnimator.SetBool("open", false);
        }
    }
}

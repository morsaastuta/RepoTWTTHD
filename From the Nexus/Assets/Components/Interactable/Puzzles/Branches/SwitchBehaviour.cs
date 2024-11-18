using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    [Header("Branching")]
    [SerializeField] List<GameObject> branches = new();
    [SerializeField] int branchIdx = 0;

    [Header("References")]
    [SerializeField] Animator switchAnimator;
    [SerializeField] Animator branchAnimator;
    [SerializeField] Animator dataAnimator;
    [SerializeField] Animator interfaceAnimator;
    [SerializeField] AudioSource sfxSource;

    bool on = false;
    bool switching = false;

    void Start()
    {
        branchAnimator.SetInteger("branchQty", branches.Count);
        StabilizeBranchTracing();
        LoadActiveBranch();
    }

    void LoadActiveBranch()
    {
        branches[branchIdx].SetActive(true);
    }

    void StabilizeBranchTracing()
    {
        // Hide all branches
        foreach (GameObject branch in branches) branch.SetActive(false);

        // Prepare for next branch
        branchAnimator.SetInteger("branchIdx", branchIdx);
        interfaceAnimator.SetInteger("branchIdx", branchIdx);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Player"))
        {
            JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Connect, false);
            on = true;
            switchAnimator.SetBool("on", true);
            branchAnimator.SetBool("on", true);
            interfaceAnimator.SetBool("on", true);
        }
    }

    void Update()
    {
        if (on && !switching && Shortcuts.Pressed(GameManager.instance.interact)) StartCoroutine(Switch());
    }

    IEnumerator Switch()
    {
        JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Branch, false);

        // Set next branch index
        if (branchIdx < branches.Count - 1) branchIdx++;
        else branchIdx = 0;

        // Turn off current branch
        switching = true;
        StabilizeBranchTracing();
        interfaceAnimator.SetTrigger("switch");
        dataAnimator.SetTrigger("switch");

        yield return new WaitForSeconds(1.2f);

        // Turn on next branch
        LoadActiveBranch();
        switching = false;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (Shortcuts.GetColliderLayer(collider, "Player"))
        {
            if (!sfxSource.isPlaying) JukeboxManager.instance.PlaySFX(sfxSource, JukeboxManager.SFX.Disconnect, false);
            on = false;
            switchAnimator.SetBool("on", false);
            branchAnimator.SetBool("on", false);
            interfaceAnimator.SetBool("on", false);
        }
    }
}

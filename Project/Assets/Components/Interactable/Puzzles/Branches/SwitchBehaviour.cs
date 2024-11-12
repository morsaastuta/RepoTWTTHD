using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    [Header("Branching")]
    [SerializeField] List<GameObject> branches = new();
    [SerializeField] int branchIdx = 0;

    [Header("Animators")]
    [SerializeField] Animator switchAnimator;
    [SerializeField] Animator branchAnimator;
    [SerializeField] Animator dataAnimator;
    [SerializeField] Animator interfaceAnimator;

    bool on = false;
    bool switching = false;

    void Start()
    {
        Debug.Log(branches.Count);
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
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            on = true;
            switchAnimator.SetBool("on", true);
            branchAnimator.SetBool("on", true);
            interfaceAnimator.SetBool("on", true);
        }
    }

    void Update()
    {
        if (on && !switching && Shortcuts.Pressed(LevelManager.instance.interact)) StartCoroutine(Switch());
    }

    IEnumerator Switch()
    {
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
        if (Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            on = false;
            switchAnimator.SetBool("on", false);
            branchAnimator.SetBool("on", false);
            interfaceAnimator.SetBool("on", false);
        }
    }
}

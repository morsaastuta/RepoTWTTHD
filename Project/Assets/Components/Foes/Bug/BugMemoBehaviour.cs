using UnityEngine;

public class BugMemoBehaviour : FoeBehaviour
{
    protected override void Start()
    {
        base.Start();

        entityCode = new BugMemo();
    }

    protected override void Update()
    {
        base.Update();

        Walk();
    }
}

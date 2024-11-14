public class BugMemoBehaviour : FoeBehaviour
{
    protected override void Start()
    {
        entityCode = new BugMemo();

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        Walk();
    }
}

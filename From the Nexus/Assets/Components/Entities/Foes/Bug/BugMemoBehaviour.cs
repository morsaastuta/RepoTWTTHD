public class BugMemoBehaviour : FoeBehaviour
{
    protected override void Start()
    {
        base.Start();

        right = true;

        entityCode = new BugMemo();
    }

    protected override void Update()
    {
        base.Update();

        Walk();
    }
}

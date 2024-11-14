using Glossary;

public class BugMemo : Foe
{
    public BugMemo()
    {
        name = "Bug";
        sm = 1;

        // Initialize values
        type = CType.Memo;
        pm = 1f;
        vm = 5f;
        ogSpeed = 1.6f;
        ogJumpPower = 6f;
        UpdateState();
        ClearMemory();
    }
}

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
        speed = 2f;
        jumpPower = 6f;
        ClearMemory();
    }
}

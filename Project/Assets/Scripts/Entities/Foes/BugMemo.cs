using Glossary;

public class BugMemo : Foe
{
    public BugMemo()
    {
        name = "Bug";

        // Initialize values
        type = CType.Memo;
        pm = 8f;
        vm = 5f;
        speed = 2f;
        jumpPower = 6f;
        ClearMemory();
    }
}

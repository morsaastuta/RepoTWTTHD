using Glossary;

public class LeakMemo : Foe
{
    public LeakMemo()
    {
        name = "Leak";

        // Initialize values
        type = CType.Memo;
        pm = 6f;
        vm = 50f;
        sm = 1.5f;
        speed = 0f;
        jumpPower = 0f;
        ClearMemory();
    }
}

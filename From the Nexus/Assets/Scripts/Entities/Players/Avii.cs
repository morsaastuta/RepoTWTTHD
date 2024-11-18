using Glossary;

public class Avii : Player
{
    public Avii()
    {
        name = "Avii";

        // Initialize values
        type = CType.Anti;
        pm = 100f;
        vm = 50f;
        ogSpeed = 3f;
        ogJumpPower = 8f;
        UpdateState();
        ClearMemory();
    }
}

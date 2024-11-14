using Glossary;

public class BugExo : Foe
{
    public BugExo()
    {
        name = "Bug (Infected)";

        // Initialize values
        type = CType.Exo;
        pm = 8f;
        vm = 5f;
        speed = 3.5f;
        jumpPower = 9f;
        ClearMemory();
    }
}

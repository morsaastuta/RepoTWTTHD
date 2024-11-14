using Glossary;

public class LeakExo : Foe
{
    public LeakExo()
    {
        name = "Leak (Infected)";

        // Initialize values
        type = CType.Exo;
        pm = 30f;
        vm = 5f;
        speed = 0f;
        jumpPower = 0f;
        ClearMemory();
    }
}

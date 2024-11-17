using Glossary;

public class VirusBreachCore : Foe
{
    public VirusBreachCore()
    {
        name = "Breach core";

        // Initialize values
        type = CType.Exo;
        pm = 16f;
        vm = 999f;
        sm = 12f;
        ogSpeed = 0f;
        ogJumpPower = 0f;
        UpdateState();
        ClearMemory();
    }
}

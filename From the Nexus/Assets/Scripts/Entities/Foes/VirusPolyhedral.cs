using Glossary;

public class VirusPolyhedral : Foe
{
    public VirusPolyhedral()
    {
        name = "Adenovirus";

        // Initialize values
        type = CType.Exo;
        pm = 12f;
        vm = 999f;
        sm = 3f;
        ogSpeed = 2.4f;
        ogJumpPower = 0f;
        UpdateState();
        ClearMemory();
    }
}

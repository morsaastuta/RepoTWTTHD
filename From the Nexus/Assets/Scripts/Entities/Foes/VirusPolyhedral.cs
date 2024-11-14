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
        speed = 0.9f;
        jumpPower = 0f;
        ClearMemory();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using Glossary;

public static class Combat
{
    public static void Inflict(Ability cast, Entity source, Entity target)
    {
        cast.type = source.type;

        target.AllocatePM(CalculateCast(cast, target).apm);
    }

    static Ability CalculateCast(Ability cast, Entity target)
    {
        // If Type is equal, clear PM
        if (cast.type.Equals(target.type)) cast.apm = -Mathf.Abs(cast.apm);
        else
        {
            switch (cast.type)
            {
                // Anti matchups
                case CType.Anti:
                    switch (target.type)
                    {
                        // Exo is weak to Anti
                        case CType.Exo: cast.apm *= 2; break;
                    }
                    break;

                // Exo matchups
                case CType.Exo:
                    switch (target.type)
                    {
                        // Anti resists Exo
                        case CType.Anti: cast.apm *= 0.5f; break;
                        // Memo is weak to Exo
                        case CType.Memo: cast.apm *= 2; break;
                    }
                    break;
            }
        }

        return cast;
    }
}

using System;

public abstract class Ability : TypedConcept
{
    public Type source;
    public Type target;

    public float avm;
    public float apm;


    protected void SetInteractions(Type source, bool isClear)
    {
        if (!isClear)
        {
            if (source.BaseType.Equals(typeof(Player))) target = typeof(Foe);
            else target = typeof(Player);
        }
        else target = source;
    }
}

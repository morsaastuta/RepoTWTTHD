using System;
using UnityEngine;

public abstract class CastBehaviour : MonoBehaviour
{
    protected Entity source;
    public Type target;
    protected Cast cast;

    virtual protected void Start()
    {
        source.AllocateVM(cast.cost);

        if (!cast.isClear)
        {
            if (source.GetType().BaseType == typeof(Player)) target = typeof(Foe);
            else target = typeof(Player);
        }
        else target = source.GetType().BaseType;
    }

    virtual protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (target.Equals(typeof(Player)) && Shortcuts.CollidesWithLayer(collider, "Player"))
        {
            Combat.Inflict(this, source, collider.GetComponent<EntityBehaviour>());
        }
        else if (target.Equals(typeof(Foe)) && Shortcuts.CollidesWithLayer(collider, "Foe"))
        {
            Combat.Inflict(this, source, collider.GetComponent<EntityBehaviour>());
        }
    }

    public Cast GetCast()
    {
        return cast;
    }

    public Entity GetSource()
    {
        return source;
    }

    public void SetSource(Entity e)
    {
        source = e;
    }
}

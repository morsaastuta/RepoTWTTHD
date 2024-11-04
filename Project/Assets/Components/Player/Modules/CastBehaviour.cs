using UnityEngine;

public abstract class CastBehaviour : MonoBehaviour
{
    protected Entity source;
    protected Ability cast;
    protected bool isClear = false;

    virtual protected void Start()
    {
        source.AllocateVM(cast.avm);
    }

    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (cast.target.Equals(typeof(Player)) && Shortcuts.CollidesWithLayer(collision, "Player")) Combat.Inflict(cast, source, collision.collider.GetComponent<PlayerBehaviour>().entityCode);
        else if (cast.target.Equals(typeof(Foe)) && Shortcuts.CollidesWithLayer(collision, "Foe")) Combat.Inflict(cast, source, collision.collider.GetComponent<FoeBehaviour>().entityCode);
    }

    public Ability GetCast()
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

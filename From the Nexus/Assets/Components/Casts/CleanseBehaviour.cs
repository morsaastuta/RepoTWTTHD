using UnityEngine;

public class CleanseBehaviour : CastBehaviour
{
    int timer = 40;

    override protected void Start()
    {
        cast = new Cleanse();

        base.Start();
    }

    void FixedUpdate()
    {
        timer--;
        if (timer <= 0) StartCoroutine(Shortcuts.DestroyAudibleObject(gameObject));
    }
}

using System;
using Unity.VisualScripting;

public class Bullet : Ability
{
    public Bullet(Type source, bool isClear)
    {
        SetInteractions(source, isClear);

        avm = 5;
        apm = 1;
    }
}

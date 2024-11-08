using System;
using Unity.VisualScripting;

public class Projectile : Cast
{
    public Projectile()
    {
        isClear = false;
        cost = 3;
        apm = 1;
    }
}

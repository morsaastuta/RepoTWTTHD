using System;
using Unity.VisualScripting;

public class Projectile : Cast
{
    public Projectile()
    {
        name = "Projectile";
        isClear = false;
        cost = 3;
        apm = 1;
    }
}

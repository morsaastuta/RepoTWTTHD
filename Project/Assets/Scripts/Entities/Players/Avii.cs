using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Glossary;

public class Avii : Player
{
    public Avii()
    {
        name = "Avii";

        // Initialize values
        type = CType.Anti;
        pm = 100f;
        vm = 50f;
        speed = 3f;
        jumpPower = 8f;
        ClearMemory();
    }
}

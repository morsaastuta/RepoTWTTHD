using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Glossary;

public abstract class Player : Entity
{
    public List<Mod> modules = new();
}

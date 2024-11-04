using UnityEngine;
using Glossary;

public abstract class Entity : TypedConcept
{
    // Basic info
    public string name;

    // Persistent memory (Health)
    public float pm = 100f;
    public float apm = 0f;

    // Volatile memory (Mana)
    public float vm = 50f;
    public float avm = 0f;

    // Movement
    public float speed = 3f;
    public float jumpPower = 8f;

    public void ClearMemory()
    {
        apm = 0f;
        avm = 0f;
    }

    public void AllocatePM(float a)
    {
        apm += a;
    }

    public void AllocateVM(float a)
    {
        avm += a;
    }

    public bool FreePM()
    {
        return apm < pm;
    }

    public bool FreeVM()
    {
        return avm < vm;
    }
}

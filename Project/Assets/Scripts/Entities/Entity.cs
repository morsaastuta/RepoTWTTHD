using UnityEngine;
using Glossary;
using System.Collections.Generic;

public abstract class Entity : TypedConcept
{
    // Basic info
    public string name;
    public List<State> states = new();

    // Persistent memory (Health)
    public float pm = 100f;
    public float apm = 0f;

    // Volatile memory (Mana)
    public float vm = 50f;
    public float avm = 0f;

    // Shared memory (Score)
    public float sm = 0;

    // Movement
    public float speed = 3f;
    public float jumpPower = 8f;

    public void UpdateState()
    {
        // Overload: If AVM > VM, player will move slower and receive double PM
        if (avm > vm)
        {
            if (!HasState(State.Overloaded)) AddState(State.Overloaded);
        }
        else if (HasState(State.Overloaded)) RemoveState(State.Overloaded);

        // Set default values
        speed = 3f;
        jumpPower = 8f;

        // Stat application
        if (HasState(State.Overloaded)) {
            speed = 1f;
            jumpPower = 4f;
        }
    }

    public void AddState(State state)
    {
        states.Add(state);

        UpdateState();
    }

    public void RemoveState(State state)
    {
        states.RemoveAll(s => s == state);

        UpdateState();
    }

    public bool HasState(State state)
    {
        return states.Contains(state);
    }

    public void ClearMemory()
    {
        apm = 0f;
        avm = 0f;
    }

    public void AllocatePM(float a)
    {
        if (HasState(State.Overloaded)) a *= 2;

        apm += a;

        UpdateState();
    }

    public void AllocateVM(float a)
    {
        avm += a;

        UpdateState();
    }

    public void ClearPM(float a)
    {
        if (apm - a >= 0) apm -= a;
        else apm = 0;

        UpdateState();
    }

    public void ClearVM(float a)
    {
        if (avm - a >= 0) avm -= a;
        else avm = 0;

        UpdateState();
    }

    public bool AllocatedPM()
    {
        return apm > 0;
    }

    public bool AllocatedVM()
    {
        return avm > 0;
    }

    public bool CanAllocatePM()
    {
        return apm < pm;
    }

    public bool CanAllocateVM()
    {
        return avm < vm;
    }
}

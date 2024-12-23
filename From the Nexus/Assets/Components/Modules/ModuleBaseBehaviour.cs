using UnityEngine;

public class ModuleBaseBehaviour : MonoBehaviour
{
    public bool active = true;

    [SerializeField] PlayerBehaviour player;

    public bool DefaultConditions()
    {
        return active && !player.entityCode.HasState(Glossary.State.Disconnected) && player.entityCode.CanAllocateVM();
    }
}

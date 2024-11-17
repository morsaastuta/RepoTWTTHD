using UnityEngine;

public class ImmortalBoxBehaviour : MonoBehaviour
{
    [SerializeField] Animator box;

    [SerializeField] int height = 1;
    [SerializeField] int width = 1;

    void Awake()
    {
        box.SetInteger("h", height);
        box.SetInteger("w", width);
        GetComponent<BoxCollider2D>().size = new(width - 0.04f, height - 0.04f);
    }
}

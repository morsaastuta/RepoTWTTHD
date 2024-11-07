using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] MessageController textHUD;

    public MessageController GetTextHUD()
    {
        return textHUD;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Button")]
    [SerializeField] float scale = 1;

    [Header("Text")]
    [SerializeField] string content = "";
    [SerializeField] float fontSize = 28;

    [Header("Resources")]
    [SerializeField] Sprite off;
    [SerializeField] Sprite hover;
    [SerializeField] Sprite click;

    Button button;

    void Start()
    {
        transform.localScale *= scale;
        button = GetComponent<Button>();
        GetComponentInChildren<TextMeshProUGUI>().text = content;
        GetComponentInChildren<TextMeshProUGUI>().fontSize = fontSize;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button.interactable) GetComponent<Image>().sprite = click;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable) GetComponent<Image>().sprite = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button.interactable) GetComponent<Image>().sprite = off;
    }

    void OnEnable()
    {
        GetComponent<Image>().sprite = off;
    }
}

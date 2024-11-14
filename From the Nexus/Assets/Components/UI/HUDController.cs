using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    PlayerBehaviour player;
    [SerializeField] Image pmBar;
    [SerializeField] Image vmBar;
    [SerializeField] TextMeshProUGUI pmValue;
    [SerializeField] TextMeshProUGUI vmValue;

    public void SetPlayer(PlayerBehaviour p)
    {
        player = p;
    }

    void Update()
    {
        pmBar.fillAmount = player.entityCode.apm / player.entityCode.pm;
        vmBar.fillAmount = player.entityCode.avm / player.entityCode.vm;
        pmValue.text = player.entityCode.apm.ToString("0.0") + "/" + player.entityCode.pm.ToString("0.0");
        vmValue.text = player.entityCode.avm.ToString("0.0") + "/" + player.entityCode.vm.ToString("0.0");
    }
}

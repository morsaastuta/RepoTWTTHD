using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    [SerializeField] GameObject staminaHUD;
    [SerializeField] Image staminaBar;
    [SerializeField] TextMeshProUGUI staminaMeter;

    float stMax = 16.0f;
    float stQty = 0.0f;
    float stConsumptionRate = 0.8f;
    float stRecoveryRate = 0.2f;

    void Start()
    {
        // Set current stamina to maximum stamina
        stQty = stMax;
    }

    void Update()
    {
        if (HasStamina() && Input.GetKey(KeyCode.Space)) ConsumeStamina();

        // If current stamina is lower than maximum stamina, show stamina HUD recovery so the player can manage its consumption wisely. Else, hide it so it doesn't distract the player.
        if (stQty < stMax)
        {
            if (!staminaHUD.activeInHierarchy) staminaHUD.SetActive(true);

            // If current stamina is lower than maximum stamina, recover the rate amount.
            if (stQty < stMax) RecoverStamina(stRecoveryRate);

            // Set stamina bar fill amount to current stamina divided by maximum stamina (percentage).
            staminaBar.fillAmount = stQty / stMax;

            // Show current stamina by maximum stamina via the stamina meter.
            staminaMeter.SetText(stQty.ToString("0.0") + "/" + stMax.ToString("0.0"));
        }
        else if (staminaHUD.activeInHierarchy) staminaHUD.SetActive(false);
    }

    public void ConsumeStamina()
    {
        // Consume a certain amount of stamina. Set to 0 if the result is lower.
        stQty -= Time.deltaTime * stConsumptionRate;
        if (stQty < 0) stQty = 0;
    }

    public void RecoverStamina(float qty)
    {
        // Recover a certain amount of stamina. Set to maximum if the result is higher.
        stQty += Time.deltaTime * qty;
        if (stQty > stMax) stQty = stMax;
    }

    public bool HasStamina()
    {
        return stQty > 0;
    }
}

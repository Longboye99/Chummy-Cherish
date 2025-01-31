using UnityEngine;
using UnityEngine.UI;

public class PlayerStatManager : MonoBehaviour
{
    public float maxStamina;
    private float currentStamina;
    [SerializeField] float staminaDrain;
    [SerializeField] Slider staminaSlider;

    private void Start()
    {
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        currentStamina = maxStamina;
    }

    private void Update()
    {
        
            currentStamina -= staminaDrain * Time.deltaTime;
            staminaSlider.value = currentStamina;

    }

}

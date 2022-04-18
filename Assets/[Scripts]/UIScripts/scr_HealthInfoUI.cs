using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scr_HealthInfoUI : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI maxHealthText;
    HealthComponent playerHealthComponent;


    private void OnEnable()
    {
        scr_PlayerEvents.OnHealthInitialized += OnHealthInitialized; 
    }


    private void OnDisable()
    {
        scr_PlayerEvents.OnHealthInitialized -= OnHealthInitialized;

    }
    private void OnHealthInitialized(HealthComponent healthComponent)
    {
        playerHealthComponent = healthComponent;
    }

    void Update()
    {
        healthText.text = playerHealthComponent.CurrentHealth.ToString();
        maxHealthText.text = playerHealthComponent.MaxHealth.ToString();
    }
}

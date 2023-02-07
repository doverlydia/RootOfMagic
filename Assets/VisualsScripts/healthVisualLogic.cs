using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;
using UnityEngine.UI;

public class healthVisualLogic : MonoBehaviour
{
    [SerializeField] private Slider Slider;
    // Start is called before the first frame update
    void Start()
    {
        
        PlayerController.Instance.PlayerHealthChangedEvent.AddListener(OnHealthChange);
    }

    void OnHealthChange(float currentHealth, float maxHealth)
    {
        Slider.value = currentHealth / maxHealth;
    }
}

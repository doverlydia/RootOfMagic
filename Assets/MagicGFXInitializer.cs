using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Magics.Patterns;
using Magics.StatusEffects;

public class MagicGFXInitializer : MonoBehaviour
{
    [SerializeField] private Pattern magicPattern;
    [SerializeField] private GameObject fireGFX;
    [SerializeField] private GameObject iceGFX;
    [SerializeField] private GameObject bloodGFX;

    private void OnEnable()
    {
        Debug.Log(magicPattern.StatusEffect);
        switch (magicPattern.StatusEffect)
        {
            case Runes.StatusEffectType.DamageOverTime:
                fireGFX.SetActive(true);
                break;
            case Runes.StatusEffectType.Slow:
                iceGFX.SetActive(true);
                break;
            case Runes.StatusEffectType.Leech:
                bloodGFX.SetActive(true);
                break;
            default:
                break;
        }
    }
}

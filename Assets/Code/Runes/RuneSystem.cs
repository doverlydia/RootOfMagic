using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RuneSystem : MonoBehaviour
{
    [SerializeField] private SyllablesScriptableObject syllables;

    private Dictionary<string, Rune> runes = new();

    protected void Awake()
    {
        InitRunes();

#if UNITY_EDITOR
        PrintRunes();
#endif
    }

    private void InitRunes()
    {
        // Just cloning so we won't change the original syllables
        var unusedSyllables = syllables.availableSyllables.ToList();

        var rune = GenerateRuneWithRandomSyllable(unusedSyllables, StatusEffectTypes.DamageOverTime, MagicPatternTypes.Beam);
        runes.Add(rune.Syllable, rune);

        rune = GenerateRuneWithRandomSyllable(unusedSyllables, StatusEffectTypes.Slow, MagicPatternTypes.DamageField);
        runes.Add(rune.Syllable, rune);

        rune = GenerateRuneWithRandomSyllable(unusedSyllables, StatusEffectTypes.Leech, MagicPatternTypes.Companion);
        runes.Add(rune.Syllable, rune);
    }

    private Rune GenerateRuneWithRandomSyllable(List<string> unusedSyllables, StatusEffectTypes statusEffect, MagicPatternTypes magicPattern)
    {
        var syllableToUse = unusedSyllables.RemoveRandom();
        return new Rune()
        {
            Syllable = syllableToUse,
            StatusEffect = statusEffect,
            magicPattern = magicPattern
        };
    }

    private void PrintRunes()
    {
        var output = "Runes dict: \n";
        foreach (var rune in runes)
        {
            output += $"{rune.Key} : {rune.Value.Syllable}, {rune.Value.magicPattern}, {rune.Value.StatusEffect} \n";
        }
        
        Debug.Log(output);
    }

    public class Rune
    {
        public string Syllable;
        public StatusEffectTypes StatusEffect;
        public MagicPatternTypes magicPattern;
    }

    public enum StatusEffectTypes
    {
        DamageOverTime,
        Slow,
        Leech
    }

    public enum MagicPatternTypes
    {
        Beam,
        Companion,
        DamageField
    }
}
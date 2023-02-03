using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSystem : MonoBehaviour
{
    private Dictionary<string, Rune> runes; 
        
    protected void Awake()
    {
        
    }

    protected void Init()
    {
        
    }
}

public class Rune
{
    public string Syllable;
    public StatusEffect StatusEffect;
    public ProjectilePattern ProjectilePattern;
}

public enum StatusEffect
{
    DamageOverTime,
    Slow,
    Leech
}

public enum ProjectilePattern
{
    Beam,
    Companion,
    DamageField
}
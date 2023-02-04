using System;
using System.Collections;
using System.Collections.Generic;
using Runes;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] private RuneUIController[] titleRune;

    protected void Start()
    {
        SetTitleRunes();
    }

    private void SetTitleRunes()
    {
        var counter = 0;
        foreach (var rune in RunesController.Instance.GetRunes())
        {
            titleRune[counter++].SetRune(rune);
        }
    }
}

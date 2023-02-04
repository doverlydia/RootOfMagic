using Runes;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class RuneUIController : MonoBehaviour
{
    private TMP_Text runeSyllableText;
    protected void Awake()
    {
        runeSyllableText = GetComponent<TMP_Text>();
    }

    public void SetRune(Rune rune)
    {
        runeSyllableText.text = rune.Syllable;
    }
}

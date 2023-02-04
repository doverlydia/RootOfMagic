using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEditor.Presets;
using UnityEngine;

public class InputVisualLogic : MonoBehaviour
{

    [SerializeField] private List<TMP_Text> _texts;
    void Start()
    {
        PlayerInputController.Instance.NewUserInputState.AddListener(PresetShowInputOnChange);
    }

    void PresetShowInputOnChange(string inputs)
    {
        int i = 0;
        for (; i < inputs.Length; i++)
        {
            _texts[i].text = inputs[i].ToString();
        }

        for (; i < _texts.Count; i++)
        {
            _texts[i].text = "_";
        }
    }
}

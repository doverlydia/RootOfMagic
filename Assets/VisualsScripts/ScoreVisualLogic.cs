using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreVisualLogic : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    void Start()
    {
        ScoreController.Instance.ScoreChanged.AddListener(OnScoreCHanged);
    }

    void OnScoreCHanged(int newScore)
    {
        text.text = $"score: {newScore}";
    }
    


}

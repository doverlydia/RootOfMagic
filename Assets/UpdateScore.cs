using Characters.Enemy;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    int score;
    TMP_Text text;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        Enemy.EnemyDied.AddListener((x, y) =>
        {
            score += 100;
            text.text = score.ToString();
        });

        Enemy.EnemyHit.AddListener((x, y, z) =>
        {
            score += 50;
            text.text = score.ToString();
        });
    }
}

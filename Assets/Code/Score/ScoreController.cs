using System;
using Characters.Enemy;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private int scorePerSecond;
    private IntReactiveProperty score;
    private DateTime _lastScoreUpdateUtc;
    public static UnityEvent<int> ScoreChanged = new UnityEvent<int>();
    private void OnEnable()
    {
        _lastScoreUpdateUtc = DateTime.UtcNow;
        score.Subscribe(OnScoreChanged);
        Enemy.EnemyDied.AddListener(OnEnemyDied);
    }

    private void Update()
    {
        int secondsSinceLastUpdate = (DateTime.UtcNow - _lastScoreUpdateUtc).Seconds;
        if (  secondsSinceLastUpdate >= 1)
        {
            score.Value += secondsSinceLastUpdate * 10;
            _lastScoreUpdateUtc = DateTime.UtcNow;
        }
    }

    private void OnDisable()
    {
        score.Value = 0;
        Enemy.EnemyDied.RemoveListener(OnEnemyDied);
    }

    void OnScoreChanged(int newScore)
    {
        ScoreChanged.Invoke(newScore);
    }

    void OnEnemyDied(Guid id)
    {
        score.Value += 3;     
    }
}

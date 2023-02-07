using System;
using Characters.Enemy;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : SingletonMonoBehavior<ScoreController>
{
    [SerializeField] private int scorePerSecond;
    private IntReactiveProperty score = new IntReactiveProperty();
    private DateTime _lastScoreUpdateUtc;
    public  UnityEvent<int> ScoreChanged = new ();
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

    void OnEnemyDied(Guid id, Vector3 pos)
    {
        score.Value += 3;     
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Enemy;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Characters.Enemy
{
    public class EnemyController : SingletonMonoBehavior<EnemyController>
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnTickRateInSeconds;
    [SerializeField] private int _baseSpawnAmount;
    [SerializeField] private int _maxEnemyCount;
    [SerializeField] private float enemyBaseHp;
    [SerializeField] private float enemyBaseDamage;
    [SerializeField] public UnityEvent waveSurvived = new();
    [SerializeField] public float MaxVariance = 0.5f;
    private Dictionary<Guid, Enemy> _enemies = new();
    private float _timePassed;
    private int _amountDied;
    private int _wavesCompletedAmount;
    private Camera _camera;

    public Enemy GetRandomEnemy()
    {
        var random = new System.Random();
        return _enemies.Values.OrderBy(_ => random.Next()).FirstOrDefault();
    }

    public Enemy GetRandomEnemyInRadius(Vector2 position,float radius = 1)
    {
        var random = new System.Random();
        return _enemies.Values
            .Where(e => math.abs(Vector2.Distance(position, e.transform.position)) <= radius )
            .OrderBy(_ => random.Next())
            .FirstOrDefault();
    }

    public Enemy GetEnemyById(Guid id)
    {
        _enemies.TryGetValue(id,out var enemy);
        return enemy;
    }

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
        Enemy.EnemyDied.AddListener(OnEnemyDied);
        _timePassed = _spawnTickRateInSeconds;
    }

    private void OnDestroy()
    {
        Enemy.EnemyDied.RemoveListener(OnEnemyDied);
    }

    private void Update()
    {
        foreach (var enemy in _enemies.Values)
        {   
            enemy.SetMovement(getMoveDirection(enemy));
        }
    }

    private void FixedUpdate()
    {
        TrySpawn();
    }

    private void TrySpawn()
    {

        // Set the position along the chosen side of the camera view
        if (_enemies.Count >= _maxEnemyCount)
        {
            return;
        }
        _timePassed += Time.deltaTime;
        if (_timePassed < GetSpawnTickRate())
        {
            return;
        }

        for (int i = 0; i < GetSpawnAmount(); i++)
        {
            var enemy = Instantiate(_enemyPrefab, GetSpawnPos(), Quaternion.identity);
            var enemyData = enemy.GetComponent<Enemy>();
            ModifyEnemy(enemyData);
            _enemies[enemyData.Id] = enemyData;
        }
        
        _timePassed -= _spawnTickRateInSeconds;
        _wavesCompletedAmount++;
        waveSurvived.Invoke();
    }

    private Vector3 GetSpawnPos()
    {
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: // Left
                return _camera.ViewportToWorldPoint(new Vector3(0, Random.Range(0.3f, 2.5f), _camera.nearClipPlane));

            case 1: // Right
                return _camera.ViewportToWorldPoint(new Vector3(1, Random.Range(0.3f, 2.5f), _camera.nearClipPlane));

            case 2: // Bottom
                return _camera.ViewportToWorldPoint(new Vector3(Random.Range(0.3f, 2.5f), 0, _camera.nearClipPlane));

            case 3: // Top
                return _camera.ViewportToWorldPoint(new Vector3(Random.Range(0.3f, 2.5f), 1, _camera.nearClipPlane));

            default:
                return Vector3.zero;
        }
    }

    private float GetSpawnTickRate()
    {
        return _wavesCompletedAmount < 2 ? _spawnTickRateInSeconds : _spawnTickRateInSeconds / 2;
    }

    private int GetSpawnAmount()
    {
        return (_baseSpawnAmount + _amountDied / 10) * (1 + _wavesCompletedAmount / 5);
    }

    private void ModifyEnemy(Enemy enemy)
    {
        enemy.SetMaxHp(enemyBaseHp + (1 + _wavesCompletedAmount * 2 / 3 ));
        enemy.damage = enemyBaseDamage + (1 + _wavesCompletedAmount / 3) + _amountDied / 20;
        enemy.speed += Random.Range(-0.3f, 0.5f);
    }

    private Vector2 getMoveDirection(Enemy enemy)
    {
        var playerPos = _player.transform.position;
        var enemyPos = enemy.gameObject.transform.position;
        var direction = (playerPos - enemyPos).normalized;
        var distance = Vector2.Distance(enemyPos, playerPos);
        var variance = Mathf.Lerp(MaxVariance, 0, distance / MaxVariance);
        var directionVariance = Random.insideUnitCircle * variance;
        direction += new Vector3(directionVariance.x,directionVariance.y);
        return direction.normalized;
    }

    void OnEnemyDied(Guid id, Vector3 pos)
    {
        _enemies.Remove(id);
    }
}
}
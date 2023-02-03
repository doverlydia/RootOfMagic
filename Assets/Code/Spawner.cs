using System;
using System.Collections.Generic;
using Characters.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
   [SerializeField] private GameObject _player;
   [SerializeField] private GameObject _enemyPrefab;
   [SerializeField] private float _spawnTickRateInSeconds;
   [SerializeField] private int _baseSpawnAmount;
   private Dictionary<Guid, Enemy> _enemies = new ();
   private float _timePassed;
   private int _amountDied;
   private int _spawnWave;
   private Camera _camera;

   private void Awake()
   {
      _camera = Camera.main;
   }

   private void Update()
   {
      foreach (var enemy in _enemies.Values)
      {
         var enemyPos = enemy.gameObject.transform.position;
         var playerPos = _player.transform.position;
         enemy.SetMovement((playerPos - enemyPos).normalized);
      }
   }

   private void FixedUpdate()
   {
      TrySpawn();
   }

   private void TrySpawn()
   {
      int side = Random.Range(0, 4);

      Vector3 spawnPos;

      // Set the position along the chosen side of the camera view
      switch (side)
      {
         case 0: // Left
            spawnPos = _camera.ViewportToWorldPoint(new Vector3(0, Random.Range(0f, 1f), _camera.nearClipPlane));
            break;
         case 1: // Right
            spawnPos = _camera.ViewportToWorldPoint(new Vector3(1, Random.Range(0f, 1f), _camera.nearClipPlane));
            break;
         case 2: // Bottom
            spawnPos = _camera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 0, _camera.nearClipPlane));
            break;
         case 3: // Top
            spawnPos = _camera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1, _camera.nearClipPlane));
            break;
         default:
            spawnPos = Vector3.zero;
            break;
      }
      _timePassed += Time.deltaTime;
      if (_timePassed < _spawnTickRateInSeconds)
      {
         return;
      }
      var enemy = Instantiate(_enemyPrefab,spawnPos , Quaternion.identity);
      var enemyData = enemy.GetComponent<Enemy>();
      _enemies[enemyData.Id] = enemyData;
      _timePassed -= _spawnTickRateInSeconds;
      _spawnWave++;
   }

   private float GetSpawnTickRate()
   {
      return  _spawnWave < 2? _spawnTickRateInSeconds : _spawnTickRateInSeconds / 2;
   }

   private int GetSpawnAmount()
   {
      return (_baseSpawnAmount + _amountDied / 10) * (_spawnWave / 5);
   }

   private void ModifyEnemy(Enemy enemy)
   {
      
   }
}

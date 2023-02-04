using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemy
{
    public class Enemy : EffectableCharacter
    {
        public static UnityEvent<Guid, Vector3> EnemyDied = new();
        [SerializeField] public float damage;
        public UnityEvent<Guid,Vector3,float> EnemyHit;
        public float _prevHealth;
        public Guid Id { get; private set; }

        public void SetMaxHp(float value)
        {
            maxHp = CurrentHp.Value = value;
        }


        public void Awake()
        {
            Id = Guid.NewGuid();
            _prevHealth = CurrentHp.Value;
            CurrentHp.Subscribe((float currentValue) =>
            {
                EnemyHit.Invoke(Id,transform.position,_prevHealth - currentValue);
                _prevHealth = currentValue;
            });
        }

        private void Update()
        {
            if (CurrentHp.Value < 0)
            {
                EnemyDied.Invoke(Id, gameObject.transform.position);
                Destroy(gameObject);
            }
        }
    }
}


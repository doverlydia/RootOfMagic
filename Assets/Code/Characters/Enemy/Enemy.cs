using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemy
{
    public class Enemy : EffectableCharacter
    {
        public static UnityEvent<Guid, Vector3> EnemyDied = new();
        bool isDead;
        [SerializeField] public float damage;
        public static UnityEvent<Guid,Vector3,float> EnemyHit = new();
        public float _prevHealth;
        public Guid Id { get; private set; }

        public void SetMaxHp(float value)
        {
            maxHp = CurrentHp.Value = value;
        }


        public void Awake()
        {
            Id = Guid.NewGuid();
        }

        public void Start()
        {
            _prevHealth = CurrentHp.Value;
            CurrentHp.Where(currentValue=>_prevHealth - currentValue > 0 ).Subscribe((float currentValue) =>
            {
                EnemyHit.Invoke(Id,transform.position,_prevHealth - currentValue);
                _prevHealth = currentValue;
            });
        }

        private void Update()
        {
            if (CurrentHp.Value < 0 && !isDead)
            {
                isDead = true;
                EnemyDied.Invoke(Id, gameObject.transform.position);
                Destroy(gameObject);
            }
        }
    }
}


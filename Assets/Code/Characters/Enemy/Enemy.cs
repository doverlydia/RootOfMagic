using System;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemy
{
    public class Enemy : EffectableCharacter
    {
        public static UnityEvent<Guid> EnemyDied = new();
        [SerializeField] public float damage;
        public Guid Id { get; private set; }

        public void SetMaxHp(float value)
        {
            maxHp = CurrentHp = value;
        }


        public void Awake()
        {
            Id = Guid.NewGuid();

        }

        private void Update()
        {
            if (CurrentHp < 0)
            {
                EnemyDied.Invoke(Id);
                Destroy(gameObject);
            }
        }
    }
}


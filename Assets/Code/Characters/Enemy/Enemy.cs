using System;
using Magics.StatusEffects;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Enemy
{
    public class Enemy : EffectableCharacter
    {
        public static UnityEvent<Guid> EnemyDied = new UnityEvent<Guid>();
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
                foreach (var status in StatusEffects)
                {
                    StatusEffects.Remove(status);
                }
                EnemyDied.Invoke(Id);
                Destroy(gameObject);
            }
        }
    }
}


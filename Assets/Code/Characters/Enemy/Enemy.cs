using System;
using System.Collections;
using System.Collections.Generic;
using Magics.StatusEffects;
using UnityEngine;
namespace Characters.Enemy
{
    public class Enemy : EffectableCharacter
    {
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
                for (int i = 0; i < StatusEffects.Count; i++)
                {
                    StatusEffects.RemoveAt(i);
                }
                
                Destroy(gameObject);
            }
        }
    }
}


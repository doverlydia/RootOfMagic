using System;
using System.Collections;
using System.Collections.Generic;
using Magics.StatusEffects;
using UnityEngine;
namespace Characters.Enemy
{
    public class Enemy : EffectableCharacter
    {
        public Guid Id { get; private set; }


        public void Awake()
        {
            Id = Guid.NewGuid();
        }

        private void Update()
        {
            if (Hp < 0)
            {
                foreach (var status in StatusEffects)
                {
                    StatusEffects.Remove(status);
                }
                Destroy(gameObject);
            }
        }
    }
}


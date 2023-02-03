using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class Enemy : EffectableCharacter
    {
        public Guid Id { get; private set; }


        public void Awake()
        {
            Id = Guid.NewGuid();
        }
        
        
    }
}


using Characters.Enemy;
using Notification;
using Player;
using Runes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Player
{
    public class PlayerController : EffectableCharacter
    {
        public PlayerDamage PlayerDamage;

        public static PlayerController Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Update()
        {
            if (!PlayerDamage.IsDead)
            {
                SetMovement(new Vector2(Input.GetAxisRaw("Horizontal"),
                                        Input.GetAxisRaw("Vertical"))
                                        .normalized);
            }
        }

    }
}
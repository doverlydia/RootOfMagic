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
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _maxDamagePerTick;
        [SerializeField] protected float _maxCooldown = 1;
        [SerializeField] private float _cooldown;
        public UnityEvent PlayerTookDamage = new();
        public UnityEvent PlayerDead = new();
        bool isDead;
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
            if (!isDead)
            {
                SetMovement(new Vector2(Input.GetAxisRaw("Horizontal"),
                                        Input.GetAxisRaw("Vertical"))
                                        .normalized);
                _cooldown -= Time.deltaTime;

                if (CurrentHp <= 0)
                {
                    isDead = true;
                    PlayerDead.Invoke();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_cooldown > 0) return;
            if (collision.TryGetComponent(out Enemy.Enemy enemy))
            {
                CurrentHp -= enemy.damage;
                _cooldown = _maxCooldown;
                PlayerTookDamage.Invoke();
            }
        }
    }
}
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
            if (CurrentHp > 0)
            {
                SetMovement(new Vector2(Input.GetAxisRaw("Horizontal"),
                                        Input.GetAxisRaw("Vertical"))
                                        .normalized);
                if (_cooldown > 0) return;

                Collider2D[] cols = new Collider2D[] { };
                var colsCount = Physics2D.OverlapCollider(_collider, new ContactFilter2D().NoFilter(), cols);
                for (int i = colsCount; i < 0; i--)
                {
                    if (cols[i].TryGetComponent(out Enemy.Enemy enemy))
                    {
                        CurrentHp -= enemy.damage;
                        _cooldown = _maxCooldown;
                        PlayerTookDamage.Invoke();
                    }
                }
                if (CurrentHp <= 0)
                {
                    PlayerDead.Invoke();
                }
                _cooldown -= Time.deltaTime;
            }
        }
    }
}
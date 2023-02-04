using System;
using Characters.Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Player
{
    public class PlayerController : EffectableCharacter
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _maxDamagePerTick;
        [SerializeField] protected float _maxCooldown = 1;
        [SerializeField] private int _MaxHpIncreaseAmount = 0;
        [SerializeField] private float _cooldown;
        public UnityEvent PlayerDead = new();
        public UnityEvent<float,float> PlayerHealthChangedEvent = new();
        bool isDead;
        public static PlayerController Instance { get; private set; }

        public void TryHeal(int healAmount)
        {
            CurrentHp = Math.Max(CurrentHp + healAmount, maxHp);
            PlayerHealthChangedEvent.Invoke(CurrentHp,maxHp);
        }
        

        private void Awake()
        {
            CurrentHp = maxHp;
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            EnemyController.Instance.waveSurvived.AddListener(OnWaveCompleted);
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
                PlayerHealthChangedEvent.Invoke(CurrentHp,maxHp);
            }
        }

        private void OnWaveCompleted()
        {
            maxHp += _MaxHpIncreaseAmount;
            CurrentHp = maxHp;
            PlayerHealthChangedEvent.Invoke(CurrentHp,maxHp);
        }
    }
}
using System;
using Characters.Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Player
{
    public class PlayerController : EffectableCharacter
    {
        public PlayerDamage PlayerDamage = new ();
        public UnityEvent<float,float> PlayerHealthChangedEvent = new();
        public static PlayerController Instance { get; private set; }
        [SerializeField] private int _MaxHpIncreaseAmount = 0;

        public void TryHeal(float healAmount)
        {
            CurrentHp.Value = Math.Min(CurrentHp.Value + healAmount, maxHp);
            PlayerHealthChangedEvent.Invoke(CurrentHp.Value, maxHp);
        }
        

        private void Awake()
        {
            CurrentHp.Value = maxHp;
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
            if (!PlayerDamage.IsDead)
            {
                SetMovement(new Vector2(Input.GetAxisRaw("Horizontal"),
                                        Input.GetAxisRaw("Vertical"))
                                        .normalized);
            }
        }

        private void OnWaveCompleted()
        {
            var currentHealthRatio = CurrentHp.Value / maxHp; 
            maxHp += _MaxHpIncreaseAmount;
            CurrentHp.Value = currentHealthRatio * maxHp;
           
        }
    }
}
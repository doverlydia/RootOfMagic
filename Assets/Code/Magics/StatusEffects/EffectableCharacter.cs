using System.Collections.Generic;
using UnityEngine;
using UniRx;
namespace Magics.StatusEffects
{
    public abstract class EffectableCharacter : MonoBehaviour
    {
        public float SpeedModifier = 1;
        public float Hp;
        public ReactiveCollection<StatusEffect> StatusEffects = new();

        [SerializeField] protected float _speed;
        [SerializeField] protected float _maxHp;

        protected float ActualSpeed => SpeedModifier * _speed;

        private void Awake()
        {
            StatusEffects.ObserveRemove().Subscribe(OnEffectRemoved);
        }

        public void OnEffectRemoved(CollectionRemoveEvent<StatusEffect> e)
        {
            e.Value.CancellationTokenSource.Cancel();
        }
        
        public void SetMovement(Vector2 direction)
        {
            transform.Translate(ActualSpeed * Time.deltaTime * direction);
        }
    }
}

using UniRx;
using UnityEngine;
using Interfaces;
using Magics.StatusEffects;

namespace Characters
{
    public abstract class EffectableCharacter : MonoBehaviour
    {
        public float SpeedModifier = 1;
        public float CurrentHp;
        public ReactiveCollection<StatusEffect> StatusEffects = new();

        [SerializeField] protected float speed;
        [SerializeField] public float maxHp;

        protected float ActualSpeed => SpeedModifier * speed;

        private void Awake()
        {
            StatusEffects.ObserveRemove().Subscribe(OnEffectRemoved);
        }

        public void OnEffectRemoved(CollectionRemoveEvent<StatusEffect> e)
        {
            Destroy(e.Value.gameObject);
        }

        public void SetMovement(Vector2 direction)
        {
            transform.Translate(ActualSpeed * Time.deltaTime * direction);
        }
    }
}

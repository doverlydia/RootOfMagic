using UniRx;
using UnityEngine;
using Interfaces;

namespace Characters
{
    public abstract class EffectableCharacter : MonoBehaviour
    {
        public float SpeedModifier = 1;
        public float CurrentHp;
        public ReactiveCollection<IStatusEffect> StatusEffects = new();

        [SerializeField] protected float speed;
        [SerializeField] protected float maxHp;

        protected float ActualSpeed => SpeedModifier * speed;

        private void Awake()
        {
            StatusEffects.ObserveRemove().Subscribe(OnEffectRemoved);
        }

        public void OnEffectRemoved(CollectionRemoveEvent<IStatusEffect> e)
        {
            Destroy((e.Value as MonoBehaviour).gameObject);
        }

        public void SetMovement(Vector2 direction)
        {
            transform.Translate(ActualSpeed * Time.deltaTime * direction);
        }
    }
}

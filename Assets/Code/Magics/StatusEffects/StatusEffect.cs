using Characters;
using Interfaces;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Magics.StatusEffects
{
    public abstract class StatusEffect<TDerived> : StatusEffect
    where TDerived : StatusEffect
    {
        public virtual void Init(TDerived statusEffect)
        {
            DamageMultiplier = statusEffect.DamageMultiplier;
            _duration = statusEffect._duration;
            _tickRate = statusEffect._tickRate;
        }
        public virtual void ApplyEffect()
        {
            var same = _target.GetComponents<TDerived>();
            for (int i = 0; i < same.Length; i++)
            {
                var effect = same[i];
                if (effect != this)
                {
                    _target.StatusEffects.Remove(effect);

                    if (effect != null)
                        Destroy(effect);
                }
            }
            _target.StatusEffects.Add(this);
        }
        private void Update()
        {
            if (_target == null)
            {
                return;
            }
            if (_duration > 0)
            {
                _duration -= Time.deltaTime;
                if (_duration % _deltaTick <= 1)
                {
                    ApplyEffect();
                }
            }
            else
            {
                _target.StatusEffects.Remove(this);
                Destroy(this);
            }
        }
    }

    public abstract class StatusEffect : MonoBehaviour, IStatusEffect
    {
        public float DamageMultiplier;
        public float _duration;
        public float _tickRate;
        public float _deltaTick => 1 / _tickRate;
        protected EffectableCharacter _target;

        private void Start()
        {
            _target = gameObject.GetComponent<EffectableCharacter>();
        }
    }
}
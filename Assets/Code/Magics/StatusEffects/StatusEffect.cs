using Characters;
using Interfaces;
using System.Linq;
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
    }


    public abstract class StatusEffect : MonoBehaviour, IStatusEffect
    {
        public float DamageMultiplier;
        public float _duration;
        public float _tickRate;
        public float _deltaTick => 1 / _tickRate;
        protected EffectableCharacter _target;

        public virtual void ApplyEffect()
        {
            var sameEffects = _target.StatusEffects.Where(x => x.GetType() == GetType());
            if (sameEffects.Count() > 0)
            {
                for (int i = sameEffects.Count(); i < 0; i--)
                {
                    _target.StatusEffects.RemoveAt(i);
                }
            }
            Debug.Log("applying effect");
            _target.StatusEffects.Add(this);
        }

        private void Start()
        {
            _target = gameObject.GetComponent<EffectableCharacter>();
        }

        private void Update()
        {
            if (_target == null)
            {
                Debug.Log("no target");
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
}
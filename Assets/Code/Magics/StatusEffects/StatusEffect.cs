using Characters;
using Interfaces;
using System.Linq;
using UnityEngine;

namespace Magics.StatusEffects
{
    public abstract class StatusEffect : MonoBehaviour, IStatusEffect
    {
        [SerializeField] float _duration;
        [SerializeField] float _damageMultiplier;
        [SerializeField] float _tickRate;
        private float _deltaTick => 1 / _tickRate;
        protected EffectableCharacter _target;
        public virtual void Effect(EffectableCharacter target)
        {
            var sameEffects = target.StatusEffects.Where(x => x.GetType() == this.GetType()).ToList();
            if (sameEffects.Count > 0)
            {
                for (int i = sameEffects.Count; i < 0; i--)
                {
                    target.StatusEffects.RemoveAt(i);
                }
            }
            target.StatusEffects.Add(this);
        }
        public void Update()
        {
            if (_target == null) Destroy(this.gameObject);
            if (_duration > 0)
            {
                _duration -= Time.deltaTime;
                if (_duration % _deltaTick <= 1)
                {
                    Effect(_target);
                }
            }
            else
            {
                _target.StatusEffects.Remove(this);
                Destroy(this.gameObject);
            }
        }
    }
}
using Characters;
using UnityEngine;

namespace Magics.StatusEffects
{
    public class Slow : StatusEffect<Slow>
    {
        [SerializeField] float _speedModifier;
        public override void ApplyEffect()
        {
            base.ApplyEffect();
            _target.SpeedModifier = _speedModifier;
        }
        private void OnDestroy()
        {
            if (_target != null)
                _target.SpeedModifier = 1;
        }

        public override void Init(Slow statusEffect)
        {
            base.Init(statusEffect);
            _speedModifier = statusEffect._speedModifier;
            gameObject.SetActive(true);
        }
    }
}
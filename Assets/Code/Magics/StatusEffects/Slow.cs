using Characters;
using UnityEngine;

namespace Magics.StatusEffects
{
    public class Slow : StatusEffect
    {
        [SerializeField] float _speedModifier;
        EffectableCharacter _target;
        public override void Effect(EffectableCharacter target)
        {
            _target = target;
            base.Effect(target);
            target.SpeedModifier = _speedModifier;
        }
        private void OnDestroy()
        {
            if (_target != null)
                _target.SpeedModifier = 1;
        }
    }
}
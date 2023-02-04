using Characters;
using UnityEngine;
namespace Magics.StatusEffects
{
    public class DOT : StatusEffect
    {
        [SerializeField] float _damagePerTick;
        public override void Effect(EffectableCharacter target)
        {
            base.Effect(target);
            target.CurrentHp -= _damagePerTick;
        }
    }
}
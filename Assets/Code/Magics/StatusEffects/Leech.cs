using Characters;
using Characters.Player;
using UnityEngine;

namespace Magics.StatusEffects
{
    public class Leech : StatusEffect
    {
        [SerializeField] float _damagePerTick;
        public override void Effect(EffectableCharacter target)
        {
            base.Effect(target);
            PlayerController.Instance.CurrentHp += _damagePerTick;
            target.CurrentHp -= _damagePerTick;
        }
    }
}
using UnityEngine;
namespace Magics.StatusEffects
{
    public class DOT : StatusEffect<DOT>
    {
        [SerializeField] float _damagePerTick;
        public override void ApplyEffect()
        {
            base.ApplyEffect();
            _target.CurrentHp -= _damagePerTick;
        }

        public override void Init(DOT statusEffect)
        {
            base.Init(statusEffect);
            _damagePerTick = statusEffect._damagePerTick;
        }
    }
}
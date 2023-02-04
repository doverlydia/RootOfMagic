using Characters;
using Characters.Player;
using UnityEngine;

namespace Magics.StatusEffects
{
    public class Leech : StatusEffect<Leech>
    {
        [SerializeField] float _damagePerTick;
        public  override void ApplyEffect( )
        {
            base.ApplyEffect();
            PlayerController.Instance.TryHeal((int)_damagePerTick);
            _target.CurrentHp.Value -= _damagePerTick;
        }

        public override void Init(Leech statusEffect)
        {
            base.Init(statusEffect);
            _damagePerTick = statusEffect._damagePerTick;
        }
    }
}
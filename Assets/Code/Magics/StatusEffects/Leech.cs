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
            PlayerController.Instance.CurrentHp += _damagePerTick;
            _target.CurrentHp -= _damagePerTick;
        }

        public override void Init(Leech statusEffect)
        {
            base.Init(statusEffect);
            _damagePerTick = statusEffect._damagePerTick;
        }
    }
}
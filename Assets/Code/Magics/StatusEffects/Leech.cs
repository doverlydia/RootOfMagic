using Cysharp.Threading.Tasks;
using UnityEngine;
namespace Magics.StatusEffects
{
    public class Leech : StatusEffect
    {
        private float _hpRegain;

        public Leech(float durationInSeconds, float hpRegain) : base(durationInSeconds)
        {
            _hpRegain = hpRegain;
        }

        protected override UniTask Apply(EffectableCharacter target, EffectableCharacter source = null)
        {
            source.CurrentHp += _hpRegain;
            target.CurrentHp -= _hpRegain;
            Debug.Log("Leech on: " + target.gameObject.name);
            return UniTask.CompletedTask;
        }
    }
}

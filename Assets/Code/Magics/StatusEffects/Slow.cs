using Cysharp.Threading.Tasks;
using UnityEngine;
namespace Magics.StatusEffects
{
    public class Slow : StatusEffect
    {
        private float _speedModifier;

        public Slow(float durationInSeconds, float speedModifier) : base(durationInSeconds)
        {
            _speedModifier = speedModifier;
        }

        protected override async UniTask Apply(EffectableCharacter target, EffectableCharacter source = null)
        {
            target.SpeedModifier *= _speedModifier;

            Debug.Log($"slow started");

            if (Token.IsCancellationRequested)
            {
                Debug.Log($"slow cancled");
                return;
            }

            target.SpeedModifier *= _speedModifier;
            Debug.Log($"slow: {target.gameObject.name} : {target.SpeedModifier}");
            await UniTask.Delay((int)(1000 * _durationInSeconds));
            target.SpeedModifier /= _speedModifier;
        }
    }
}
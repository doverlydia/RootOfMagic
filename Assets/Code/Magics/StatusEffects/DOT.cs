using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Magics.StatusEffects
{
    public class DOT : StatusEffect
    {
        private int _ticksPerSecond;
        private float _damagePerTick;

        public DOT(float durationInSeconds, int ticksPerSecond, float damagePerTick) : base(durationInSeconds)
        {
            _ticksPerSecond = ticksPerSecond;
            _damagePerTick = damagePerTick;
        }

        protected override async UniTask Apply(EffectableCharacter target, EffectableCharacter source = null)
        {
            if (target == null) CancellationTokenSource.Cancel();
            float timeSinceStarted = 0;
            Debug.Log($"dot started");
            do
            {
                if (Token.IsCancellationRequested)
                {
                    Debug.Log($"dot cancled");
                    return;
                }

                target.CurrentHp -= _damagePerTick;
                Debug.Log($"dot: {target.gameObject.name} : {target.CurrentHp}");
                await UniTask.Delay(1000 / _ticksPerSecond);
                timeSinceStarted += (1.0f / _ticksPerSecond);
            } while (timeSinceStarted<_durationInSeconds);
            Debug.Log($"dot finished");
        }
    }
}
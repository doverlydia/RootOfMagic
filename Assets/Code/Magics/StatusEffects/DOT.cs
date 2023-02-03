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
            float timeSinceStarted = 0;
            await UniTask.RunOnThreadPool(async () =>
              {
                  do
                  {
                      System.Diagnostics.Stopwatch sw = new ();
                      timeSinceStarted += sw.ElapsedMilliseconds;
                      sw.Start();
                      await UniTask.DelayFrame(1);
                      sw.Stop();
                  } while (timeSinceStarted < _durationInSeconds);

                  CancellationTokenSource.Cancel();

              }, true, Token);

            do
            {
                if (Token.IsCancellationRequested)
                {
                    return;
                }

                target.CurrentHp -= _damagePerTick;
                Debug.Log($"{target.gameObject.name} : {target.CurrentHp}");
                await UniTask.Delay(1000 / _ticksPerSecond);
            } while (!Token.IsCancellationRequested);
        }
    }
}
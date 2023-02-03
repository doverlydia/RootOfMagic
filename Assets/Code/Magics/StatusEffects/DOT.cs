using Cysharp.Threading.Tasks;
using System.Diagnostics;

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
            UniTask.RunOnThreadPool(async () =>
              {
                  do
                  {
                      Stopwatch sw = new Stopwatch();
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

                target.Hp -= _damagePerTick;
                await UniTask.Delay(1000 / _ticksPerSecond);
            } while (!Token.IsCancellationRequested);
        }
    }
}
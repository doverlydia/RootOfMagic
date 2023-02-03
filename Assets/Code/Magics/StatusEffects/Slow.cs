using Cysharp.Threading.Tasks;
using System.Diagnostics;

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

            float timeSinceStarted = 0;
            await UniTask.RunOnThreadPool(async () =>
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
                    target.SpeedModifier /= _speedModifier;
                    CancellationTokenSource.Dispose();
                    return;
                }

            } while (!Token.IsCancellationRequested);

        }
    }
}
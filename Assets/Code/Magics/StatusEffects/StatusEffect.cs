using Cysharp.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Magics.StatusEffects
{
    public abstract class StatusEffect
    {
        public StatusEffect(float durationInSeconds)
        {
            _durationInSeconds = durationInSeconds;
        }
        protected float _durationInSeconds;
        public CancellationTokenSource CancellationTokenSource = new();
        public CancellationToken Token => CancellationTokenSource.Token;
        protected abstract UniTask Apply(EffectableCharacter target, EffectableCharacter source = null);
        public async UniTask ApplyEffect(EffectableCharacter target, EffectableCharacter source = null)
        {
            var statusesOfSameType = target.StatusEffects.Where(x => x.GetType() == this.GetType()).ToList();

            if (statusesOfSameType.Count() > 0)
            {
                for (int i = statusesOfSameType.Count - 1; i >= 0; i--)
                {
                    target.StatusEffects.RemoveAt(i);
                }
            }

            target.StatusEffects.Add(this);
            await Apply(target, source);
            target.StatusEffects.Remove(this);
        }
    }
}
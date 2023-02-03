using Characters;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Magics.StatusEffects
{
    public abstract class StatusEffect
    {
        public float DurationInSeconds;
        public CancellationTokenSource CancellationTokenSource = new();
        public CancellationToken Token => CancellationTokenSource.Token;
        public abstract UniTask Apply(EffectableCharacter traget, EffectableCharacter source = null);
    }
}
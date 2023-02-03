using Characters.Player;
using Cysharp.Threading.Tasks;
using Magics.Patterns;
using Magics.StatusEffects;
using Notification;
using Player;
using Runes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

namespace Magics
{
    public class MagicController : MonoBehaviour
    {
        List<Magic> magics = new();
        private void Start()
        {
            PlayerInputController.Instance.NewMagicCreated.AddListener(OnNewMagicCreated);
        }

        private async void OnNewMagicCreated(MagicNotification notification)
        {
            var magic = GetNewMagic(notification);
            magics.Add(magic);
            long duration = 0;
            foreach (var op in magic.Pattern.FindInRangeEffectables())
            {
                CancellationTokenSource source = new();
                CancellationToken token = source.Token;
                UniTask.RunOnThreadPool(async () =>
                {
                    do
                    {
                        Stopwatch sw = new Stopwatch();
                        duration += sw.ElapsedMilliseconds;
                        sw.Start();
                        await UniTask.DelayFrame(1);
                        sw.Stop();
                    } while (duration < magic.Pattern.Duration);

                    source.Cancel();

                }, true, token);

                do
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    await UniTask.Delay(1000 / magic.Pattern.TicksPerSecond);
                    magic.StatusEffect.ApplyEffect(op, PlayerController.Instance);
                } while (!token.IsCancellationRequested);
            }
        }

        private Magic GetNewMagic(MagicNotification notification)
        {
            var pattern = GetNewPattern(notification.PatternType);
            return new Magic(pattern, GetNewStatusEffect(notification.StatusEffectType));

        }

        private StatusEffect GetNewStatusEffect(StatusEffectType type)
        {
            switch (type)
            {
                case StatusEffectType.DamageOverTime:
                    return new DOT(3, 3, 3);
                case StatusEffectType.Slow:
                    return new Slow(3, 3);
                case StatusEffectType.Leech:
                    return new Leech(3, 3);
            }
            return null;
        }

        private Pattern GetNewPattern(PatternType type)
        {
            switch (type)
            {
                case PatternType.Beam:
                    var beam = new Beam(3, 3, 3, 3);
                    beam.SetPatternPivot(PlayerController.Instance.transform);
                    return beam;
                case PatternType.Companion:
                    var companion = new Companion(3, 3, 3, 3);
                    companion.SetPatternPivot(PlayerController.Instance.transform);
                    return companion;
                case PatternType.DamageField:
                    var garlic = new Garlic(3, 3, 3, 3);
                    garlic.SetPatternPivot(PlayerController.Instance.transform);
                    return garlic;
            }
            return null;
        }

        private void OnDrawGizmos()
        {
            foreach (var magic in magics)
            {
                Gizmos.DrawWireSphere(magic.Pattern.Pivot.position, magic.Pattern.Radius);
            }
        }
    }
}

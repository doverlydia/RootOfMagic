using Magics.Patterns;
using Magics.StatusEffects;
using Player;
using Runes;
using System.Collections.Generic;
using UnityEngine;
using Notification;
using Characters.Player;
using System;

namespace Magics
{
    public class MagicController : MonoBehaviour
    {
        private void Start()
        {
            PlayerInputController.Instance.NewMagicCreated.AddListener(OnNewMagicCreated);
        }

        private void OnNewMagicCreated(MagicNotification notification)
        {
            var magic = GetNewMagic(notification);
            foreach (var op in magic.Pattern.FindInRangeEffectables())
            {
                magic.StatusEffect.ApplyEffect(op, PlayerController.Instance);
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
                    var beam = new Beam(3, 3);
                    beam.SetPatternPivot(PlayerController.Instance.transform);
                    return beam;
                case PatternType.Companion:
                    var companion= new Companion(3, 3);
                    companion.SetPatternPivot(PlayerController.Instance.transform);
                    return companion;
                case PatternType.DamageField:
                    var garlic= new Garlic(3, 3);
                    garlic.SetPatternPivot(PlayerController.Instance.transform);
                    return garlic;
            }
            return null;
        }
    }
}

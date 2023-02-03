using Code.Runes;

namespace Code.Notification
{
    public record MagicNotification
    {
        public readonly PatternType PatternType;
        public readonly StatusEffectType StatusEffectType;

        public MagicNotification(PatternType patternType, StatusEffectType statusEffectType)
        {
            PatternType = patternType;
            StatusEffectType = statusEffectType;
        }
    }
    
}

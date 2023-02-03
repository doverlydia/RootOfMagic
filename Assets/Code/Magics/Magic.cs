using Magics.Patterns;
using Magics.StatusEffects;

namespace Magics
{
    public class Magic
    {
        public Pattern Pattern;
        public StatusEffect StatusEffect;
        public Magic(Pattern pattern, StatusEffect statusEffect)
        {
            Pattern = pattern;
            StatusEffect = statusEffect;
        }
    }
}

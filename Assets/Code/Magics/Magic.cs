using Magics.Patterns;
using Magics.StatusEffects;
using UnityEngine;

namespace Magics
{
    public class Magic : MonoBehaviour
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

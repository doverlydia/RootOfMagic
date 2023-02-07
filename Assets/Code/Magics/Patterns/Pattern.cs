using Magics.StatusEffects;
using Runes;
using UniRx;
using UnityEngine;

namespace Magics.Patterns
{
    public abstract class Pattern : MonoBehaviour
    {
        public FloatReactiveProperty Duration;

        [SerializeField] public int TicksPerSecond;
        [SerializeField] public Collider2D Aoe;
        [SerializeField] public float BaseDamage;
        public StatusEffectType StatusEffect;
        public abstract void MoveLogic(Transform pivot);
    }
}

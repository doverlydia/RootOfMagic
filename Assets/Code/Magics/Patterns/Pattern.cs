using Magics.StatusEffects;
using System.Collections.Generic;
using UnityEngine;

namespace Magics.Patterns
{
    public abstract class Pattern
    {
        public float Duration;
        public int TicksPerSecond;
        public Transform Pivot { get; private set; }
        public float Radius { get; private set; }
        
        public float Angle { get; private set; }
        public Pattern(float angle, float radius, float duration, int ticksPerSecond)
        {
            Angle = angle;
            Radius = radius;
            Duration = duration;
            TicksPerSecond = ticksPerSecond;
        }

        public List<EffectableCharacter> FindInRangeEffectables()
        {
            var cols = Physics2D.OverlapCircleAll(Pivot.position, Radius);
            List<EffectableCharacter> results = new();
            foreach (Collider2D collider in cols)
            {
                if (!collider.TryGetComponent(out EffectableCharacter ec)) continue;

                var characterToCollider = (collider.transform.position - Pivot.position).normalized;
                var dot = Vector3.Dot(characterToCollider, Vector2.up);
                if (!(dot >= Mathf.Cos(Angle / 2))) continue;

                results.Add(ec);
            }
            return results;
        }

        public void SetPatternPivot(Transform pivot)
        {
            this.Pivot = pivot;
        }
    }
}

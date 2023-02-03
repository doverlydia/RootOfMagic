using Magics.StatusEffects;
using System.Collections.Generic;
using UnityEngine;

namespace Magics.Patterns
{
    public abstract class Pattern
    {
        protected Transform _pivot;
        protected float _angle;
        protected float _radius;
        public Pattern(float angle, float radius)
        {
            _angle = angle;
            _radius = radius;
        }

        public List<EffectableCharacter> FindInRangeEffectables()
        {
            var cols = Physics2D.OverlapCircleAll(_pivot.position, _radius);
            List<EffectableCharacter> results = new();
            foreach (Collider2D collider in cols)
            {
                if (!collider.TryGetComponent(out EffectableCharacter ec)) continue;

                var characterToCollider = (collider.transform.position - _pivot.position).normalized;
                var dot = Vector3.Dot(characterToCollider, Vector2.up);
                if (!(dot >= Mathf.Cos(_angle / 2))) continue;

                results.Add(ec);
            }
            return results;
        }

        public void SetPatternPivot(Transform pivot)
        {
            _pivot = pivot;
        }
    }
}

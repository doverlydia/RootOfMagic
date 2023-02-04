using UnityEngine;

namespace Magics.Patterns
{
    public class Garlic : Pattern
    {
        [SerializeField] protected Transform _pivot;
        public override void MoveLogic(Transform pivot)
        {
            transform.position = pivot.position;
        }
        public override void Update()
        {
            MoveLogic(_pivot);
        }
    }
}
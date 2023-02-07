using Characters.Enemy;
using UnityEngine;

namespace Magics.Patterns
{

    public class Companion : Pattern
    {
        private Transform target;
        [SerializeField] float speed;
        [SerializeField] private float radius;
        public override void MoveLogic(Transform pivot)
        {
            transform.SetParent(null);
            target = EnemyController.Instance.GetRandomEnemyInRadius(transform.position, radius)?.gameObject.transform;
            if (target == null)
            {
                target = transform;
            }
        }
        public void Update()
        {
            if (target == null || target == transform)
            {
                MoveLogic(transform);
            }

            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
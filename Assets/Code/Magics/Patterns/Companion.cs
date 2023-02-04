using Characters.Enemy;
using UnityEngine;

namespace Magics.Patterns
{

    public class Companion : Pattern
    {
        private Transform target;
        [SerializeField] float speed;
        public override void MoveLogic(Transform pivot)
        {
            transform.SetParent(null);
            target = EnemyController.Instance.GetRandomEnemy()?.gameObject.transform;
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
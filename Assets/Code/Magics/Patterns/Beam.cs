using Characters.Player;
using UnityEngine;

namespace Magics.Patterns
{
    public class Beam : Pattern
    {
        public Vector3 targetAngle = new Vector3(0f, 345f, 0f);
        private Vector3 _currentAngle;
        [SerializeField] float speed = 5;
        public override async void MoveLogic(Transform pivot)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            Vector3 direction = move.normalized;

            if (move != Vector3.zero)
            {
                transform.up = direction;
            }
        }

        public void Update()
        {
            MoveLogic(gameObject.transform);
        }
    }
}
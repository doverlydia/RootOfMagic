using UnityEngine;

namespace Magics.Patterns
{
    public class Beam : Pattern
    {
        public Vector3 targetAngle = new Vector3(0f, 345f, 0f);
        private Vector3 _currentAngle;
        [SerializeField] float speed = 5;
        public override void MoveLogic(Transform pivot)
        {
            _currentAngle = new Vector3(
              Mathf.LerpAngle(_currentAngle.x, targetAngle.x, speed * Time.deltaTime),
              Mathf.LerpAngle(_currentAngle.y, targetAngle.y, speed * Time.deltaTime),
              Mathf.LerpAngle(_currentAngle.z, targetAngle.z, speed * Time.deltaTime));

            transform.eulerAngles = _currentAngle;
            if (Vector3.Distance(_currentAngle, targetAngle) <= 1f)
            {
                targetAngle *= -1;
            }
        }

        public void Update()
        {
            MoveLogic(gameObject.transform);
        }
    }
}
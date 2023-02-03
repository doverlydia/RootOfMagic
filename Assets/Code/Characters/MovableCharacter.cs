using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class MovableCharacter : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10f;

        private Rigidbody2D _rb;

        private Vector2 _velocity;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        protected void SetVelocity(Vector2 direction)
        {
            _velocity = _moveSpeed * direction;
            _rb.velocity = _velocity;
        }
    }
}
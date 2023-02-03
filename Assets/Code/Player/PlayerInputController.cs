using System;
using Code.Runes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private RunesController _runesController;
        [SerializeField] private Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;

        private void Start()
        {
        }

        void OnMovement(InputValue inputValue)
        {
            _moveDirection = inputValue.Get<Vector2>();
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _moveDirection * (speed * Time.fixedTime));
        }
    }
}
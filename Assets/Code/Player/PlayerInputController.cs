using System;
using Code.Runes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Code.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [FormerlySerializedAs("speed")] [SerializeField] private float _speed;
        [SerializeField] private RunesController _runesController;
        [SerializeField] private Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;

        private void Start()
        {
            RebindRuneControls();
        }

        void OnMovement(InputValue inputValue)
        {
            _moveDirection = inputValue.Get<Vector2>();
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _moveDirection * (_speed * Time.fixedTime));
        }

        private void RebindRuneControls()
        {
            
        }
    }
}
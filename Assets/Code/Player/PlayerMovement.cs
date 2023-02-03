using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _runSpeed = 20.0f;

        private Rigidbody2D _rb;

        private Vector2 _normalizedInputs;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            MovePlayer(new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical"))
                                    .normalized);
        }

        private void MovePlayer(Vector2 inputs)
        {
            _normalizedInputs = _runSpeed * inputs;
            _rb.velocity = _normalizedInputs;
        }
    }
}
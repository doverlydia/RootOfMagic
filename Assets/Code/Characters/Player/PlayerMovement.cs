using UnityEngine;

namespace Characters.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10f;

        protected void SetMovement(Vector2 direction)
        {
            transform.Translate(_moveSpeed * Time.deltaTime * direction);
        }
        private void Update()
        {
            SetMovement(new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical"))
                                    .normalized);
        }
    }
}
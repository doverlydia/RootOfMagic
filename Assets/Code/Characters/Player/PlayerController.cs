using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : EffectableCharacter
    {
        protected void SetMovement(Vector2 direction)
        {
            transform.Translate(_actualSpeed * Time.deltaTime * direction);
        }

        private void Update()
        {
            SetMovement(new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical"))
                                    .normalized);
        }
    }
}
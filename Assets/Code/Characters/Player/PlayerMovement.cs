using UnityEngine;

namespace Characters.Player
{
    public class PlayerMovement : MovableCharacter
    {
        private void Update()
        {
            SetVelocity(new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical"))
                                    .normalized);
        }
    }
}
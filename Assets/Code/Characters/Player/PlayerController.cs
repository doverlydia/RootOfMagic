using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : EffectableCharacter
    {
        

        private void Update()
        {
            SetMovement(new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical"))
                                    .normalized);
        }
    }
}
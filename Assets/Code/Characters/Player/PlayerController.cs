using Magics.StatusEffects;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : EffectableCharacter
    {
        public static PlayerController Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Update()
        {
            SetMovement(new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical"))
                                    .normalized);
        }
    }
}
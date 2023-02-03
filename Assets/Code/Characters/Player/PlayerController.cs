using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : CharacterController
    {
        protected void SetMovement(Vector2 direction)
        {
            transform.Translate(_speed * Time.deltaTime * direction);
        }

        private void Update()
        {
            SetMovement(new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical"))
                                    .normalized);
        }
    }
}
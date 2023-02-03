using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerView : CharacterView
    {
        protected void SetMovement(Vector2 direction)
        {
            transform.Translate(_characterData.Speed.Value * Time.deltaTime * direction);
        }

        private void Update()
        {
            SetMovement(new Vector2(Input.GetAxisRaw("Horizontal"),
                                    Input.GetAxisRaw("Vertical"))
                                    .normalized);
        }
    }
}
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : MonoBehaviour, IEffectable
    {
        [SerializeField] private float _moveSpeed = 10f;

        public float Health { get; set; }
        public float Speed { get; set; }

        public void AddEffect()
        {
            Debug.Log("effect added");
        }

        public void RemoveEffect()
        {
            Debug.Log("effect removed");
        }

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
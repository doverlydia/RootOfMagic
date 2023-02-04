using System;
using UnityEngine;
using Characters.Player;

namespace Magics.Patterns
{
    public class Garlic : Pattern
    {
        private Transform _pivot;

        private void Awake()
        {
            _pivot = PlayerController.Instance.transform;
        }

        public override void MoveLogic(Transform pivot)
        {
            transform.position = pivot.position;
        }
        public void Update()
        {
            MoveLogic(_pivot);
        }
    }
}
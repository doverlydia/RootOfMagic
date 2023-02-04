using Magics.StatusEffects;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Magics.Patterns
{
    public abstract class Pattern : MonoBehaviour
    {
        public FloatReactiveProperty Duration;

        [SerializeField] protected int _ticksPerSecond;
        [SerializeField] protected Collider2D _aoe;
        [SerializeField] protected float _baseDamage;
        public abstract void MoveLogic(Transform pivot);
        public virtual void Update()
        {

            Debug.Log("i happen mi perent");
        }
    }
}

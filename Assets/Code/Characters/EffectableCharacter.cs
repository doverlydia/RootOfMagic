using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Characters
{
    public abstract class EffectableCharacter : MonoBehaviour
    {
        public float SpeedModifier = 1;
        public float Hp;

        [SerializeField] protected float _speed;
        [SerializeField] protected float _maxHp;

        protected float _actualSpeed => SpeedModifier * _speed;
    }
}

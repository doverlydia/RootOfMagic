using Cysharp.Threading.Tasks;
using DataClasses;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
namespace Characters
{
    public abstract class CharacterController : IEffectable
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _hp;

        public ReactiveCollection<StatusEffectData> CurrentStatusEffects { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

 
        public UniTask ApplyEffect()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveEffect(StatusEffectData data)
        {
        }
    }
}

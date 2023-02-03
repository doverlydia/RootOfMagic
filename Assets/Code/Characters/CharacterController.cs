using Cysharp.Threading.Tasks;
using DataClasses;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
namespace Characters
{
    public abstract class CharacterController : MonoBehaviour, IEffectable
    {
        [SerializeField] protected float _speed;
        [SerializeField] protected float _hp;

        public ReactiveCollection<StatusEffectData> CurrentStatusEffects { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

 
        public UniTask ApplyEffect()
        {
            throw new System.NotImplementedException();
        }

        public UniTask ApplyEffect(StatusEffectData data)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveEffect(StatusEffectData data)
        {
        }

    }
}

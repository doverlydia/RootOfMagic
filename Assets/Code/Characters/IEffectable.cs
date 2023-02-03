using Cysharp.Threading.Tasks;
using DataClasses;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
namespace Characters
{
    public interface IEffectable
    {
        [field: SerializeField] public ReactiveCollection<StatusEffectData> CurrentStatusEffects { get; set; }
        public abstract UniTask ApplyEffect(StatusEffectData data);
        
        public void RemoveEffect(StatusEffectData data);
    }
}

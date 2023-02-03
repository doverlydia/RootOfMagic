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
        public async UniTask DoEffect(StatusEffectData data)
        {
            CurrentStatusEffects.Add(data);
            UniTask.RunOnThreadPool(async () =>
            {
                await UniTask.Delay((int)data.Duration * 1000);
                RemoveEffect(data);
                CurrentStatusEffects.Remove(data);
            });
            await ApplyEffect(data);
        }

        public void RemoveEffect(StatusEffectData data);
    }
}

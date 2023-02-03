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
        [field: SerializeField] public ReactiveCollection<StatusEffectData> CurrentStatusEffects { get; set; }


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

        public UniTask ApplyEffect(StatusEffectData data)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveEffect(StatusEffectData data)
        {
            throw new System.NotImplementedException();
        }

    }
}

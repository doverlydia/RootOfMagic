using Characters;
using Cysharp.Threading.Tasks;
using System.Diagnostics;

namespace Magics.StatusEffects
{
    public class Leech : StatusEffect
    {
        private float _hpRegain;
        public override async UniTask Apply(EffectableCharacter target, EffectableCharacter source = null)
        {
            source.Hp += _hpRegain;
            target.Hp -= _hpRegain;
        }
    }
}

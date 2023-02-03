using Cysharp.Threading.Tasks;

public abstract class SlowStatusEffect : StatusEffectBase
{
    public override async UniTask ApplyEffect(ICharacter characterToEffect)
    {
        await base.ApplyEffect(characterToEffect);

        // Here we're gonna
        // For duration
        // await UniTask.Delay(ticksPerSeconds);
        // Do effect (slow/damage/whatever)
        
        OnFinishedApplyAffect(characterToEffect);
    }
}

using Cysharp.Threading.Tasks;

public abstract class StatusEffectBase
{
    protected abstract float Duration { get; set; }
    protected abstract float TicksPerSecond { get; set; }
    protected virtual float DamagePerTick { get; set; } = 0;
    
    public virtual async UniTask ApplyEffect(ICharacter characterToEffect)
    {
        characterToEffect.AddEffect(this);
    }

    protected void OnFinishedApplyAffect(ICharacter characterToEffect)
    {
        RemoveEffect(characterToEffect);
    }

    private void RemoveEffect(ICharacter characterToEffect)
    {
        characterToEffect.RemoveEffect(this);   
    }
}

public interface ICharacter
{
    public float Health { get; set; }
    public float SpeedMultiplier { get; set; }
    public void AddEffect(StatusEffectBase effect);
    public void RemoveEffect(StatusEffectBase effect);
}

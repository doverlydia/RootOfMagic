using UnityEngine;

namespace Magics.Patterns
{
    public class Beam : Pattern
    {
        public Beam(float angle, float radius, float duration, int ticksPerSecond) : base(angle, radius, duration, ticksPerSecond)
        {
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public interface IEffectable
    {
        public float Health { get; set; }
        public float Speed { get; set; }
        public void AddEffect();
        public void RemoveEffect();
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Player
{
    public class PlayerDamage : MonoBehaviour
    {
        public float MaxCooldown = 1;
        public float Cooldown;
        public bool IsDead;
        public UnityEvent PlayerTookDamage = new();
        public UnityEvent PlayerDead = new();
        // Update is called once per frame
        void Update()
        {
            Cooldown -= Time.deltaTime;

            if (PlayerController.Instance.CurrentHp <= 0)
            {
                IsDead = true;
                PlayerDead.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Cooldown > 0) return;
            if (collision.TryGetComponent(out Enemy.Enemy enemy))
            {
                PlayerController.Instance.CurrentHp -= enemy.damage;
                Cooldown = MaxCooldown;
                PlayerTookDamage.Invoke();
            }
        }
    }
}
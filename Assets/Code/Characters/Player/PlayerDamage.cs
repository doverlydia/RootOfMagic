using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Characters.Player
{
    public class PlayerDamage : MonoBehaviour
    {
        public float MaxCooldown = 1;
        public float Cooldown;
        public bool IsDead;
        public UnityEvent PlayerDead = new();
        public UnityEvent PlayerDamaged = new();
        // Update is called once per frame
        void Update()
        {
            Cooldown -= Time.deltaTime;

            if (!IsDead && PlayerController.Instance.CurrentHp.Value <= 0)
            {
                IsDead = true;
                PlayerDead.Invoke();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            var player = PlayerController.Instance;
            if (Cooldown > 0) return;
            if (!IsDead && collision.TryGetComponent(out Enemy.Enemy enemy))
            {
                PlayerController.Instance.CurrentHp.Value -= enemy.damage;
                PlayerDamaged.Invoke();
                Cooldown = MaxCooldown;
                player.PlayerHealthChangedEvent.Invoke(player.CurrentHp.Value, player.maxHp);
            }
        }

        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
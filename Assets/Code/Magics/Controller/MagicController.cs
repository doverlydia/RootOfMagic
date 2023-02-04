using Notification;
using Player;
using Runes;
using UnityEngine;

namespace Magics.Controller
{
    public class MagicController : MonoBehaviour
    {
        public static MagicController Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            PlayerInputController.Instance.NewMagicCreated.AddListener(OnNewMagicCreated);
        }
        public void OnNewMagicCreated(MagicNotification notification)
        {
            switch (notification.PatternType)
            {
                case PatternType.Beam:
                    break;
                case PatternType.Companion:
                    break;
                case PatternType.DamageField:
                    break;
            }

            switch (notification.StatusEffectType)
            {
                case StatusEffectType.DamageOverTime:
                    break;
                case StatusEffectType.Slow:
                    break;
                case StatusEffectType.Leech:
                    break;
            }
        }
    }
}
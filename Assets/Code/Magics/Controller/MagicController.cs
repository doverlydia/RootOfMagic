using Notification;
using Player;
using Runes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Magics.StatusEffects;
using Magics.Patterns;

namespace Magics.Controller
{
    [System.Serializable]
    public class PatternPrefab
    {
        public PatternType type;
        public GameObject prefab;
    }

    [System.Serializable]
    public class StatusEffectPrefab
    {
        public StatusEffectType type;
        public GameObject prefab;
    }
    public class MagicController : MonoBehaviour
    {

        public List<PatternPrefab> Patterns;
        public List<StatusEffectPrefab> Effects;
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
            CreateMagic(notification);
        }

        private Magic CreateMagic(MagicNotification notification)
        {
            var pattern = Patterns.FirstOrDefault(x => x.type == notification.PatternType).prefab;
            var effect = Effects.FirstOrDefault(x => x.type == notification.StatusEffectType).prefab;

            var patternObj = Instantiate(pattern);
            var effectObj = Instantiate(effect, pattern.transform);

            var magicObj = patternObj.AddComponent<Magic>();
            magicObj.StatusEffect = effectObj.GetComponent<StatusEffect>();
            magicObj.Pattern = patternObj.GetComponent<Pattern>();

            return magicObj;
        }
    }
}
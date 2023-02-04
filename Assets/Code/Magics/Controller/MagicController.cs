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
    public class MagicController : SingletonMonoBehavior<MagicController>
    {
        public List<PatternPrefab> Patterns;
        public List<StatusEffectPrefab> Effects;

        private void Start()
        {
            PlayerInputController.Instance.NewMagicCreated.AddListener(OnNewMagicCreated);
        }

        public void OnNewMagicCreated(MagicNotification notification)
        {
            CreateMagic(notification);
        }

        private void CreateMagic(MagicNotification notification)
        {
            var pattern = Patterns.FirstOrDefault(x => x.type == notification.PatternType)?.prefab;
            var effect = Effects.FirstOrDefault(x => x.type == notification.StatusEffectType)?.prefab;

            var patternObj = Instantiate(pattern);
            var effectObj = Instantiate(effect, patternObj.transform);

            var magicObj = patternObj.AddComponent<Magic>();
            magicObj.statusEffect = effectObj.GetComponent<StatusEffect>();
            magicObj.Pattern = patternObj.GetComponent<Pattern>();
            patternObj.SetActive(true);
        }
    }
}
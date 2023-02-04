using Extensions;
using Notification;
using Runes;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerInputController : SingletonMonoBehavior<PlayerInputController>
    {
        
        private TimeSpan _magicCooldownDuration;
        public float _cooldownInSeconds;
        private ReactiveCollection<string> _inputSequence = new();

        public UnityEvent<string> NewUserInputState = new();
        public UnityEvent<Rune> OnRuneCreated = new();

        // new Megic created event
        public UnityEvent<MagicNotification> NewMagicCreated = new();
        private DateTime _lastmagicCreationTImeUtc;

        private Task _magicCoolDown = Task.CompletedTask;

        private RunesController _runesController => RunesController.Instance;
        
        protected override void Awake()
        {
            base.Awake();
            _inputSequence.ObserveCountChanged().Subscribe(OnInputSequenceChanged);
            _inputSequence.ObserveAdd().Subscribe(OnInputSequenceGrew);
            _magicCooldownDuration = TimeSpan.FromSeconds(_cooldownInSeconds);
        }

        private async Task MagicCoolDownAction()
        {
            await UniTask
                .Delay(_magicCooldownDuration)
                .ContinueWith(() =>
                {
                    _inputSequence.Clear();
                });
        }

        private void OnInputSequenceChanged(int count)
        {
            var sequenceString = string.Join("", _inputSequence.ToList());
            NewUserInputState.Invoke(sequenceString);
            print($"changed: {sequenceString}");
        }

        private void OnInputSequenceGrew(CollectionAddEvent<string> addEvent)
        {
            if (_inputSequence.Count == 1)
            {
                return;
            }


            for (int i = 0; i < _inputSequence.Count - 1; i += 2)
            {
                string currentLetter = _inputSequence[i];
                string nextLetter = _inputSequence[i + 1];
                var syllable = currentLetter + nextLetter;
                if (!_runesController.GetRunes().Any(r => String.Equals(r.Syllable, syllable, StringComparison.CurrentCultureIgnoreCase)))
                {
                    _inputSequence.Clear();
                    return;
                }
            }
            if (_inputSequence.Count == 4)
            {
                var sequenceString = string.Join("", _inputSequence.ToList());
                var rune1Syllable = sequenceString.Substring(0, 2);
                var rune2Syllable = sequenceString.Substring(2, 2);
                var rune1 = _runesController.GetRuneBySyllable(rune1Syllable);
                var rune2 = _runesController.GetRuneBySyllable(rune2Syllable);
                NewUserInputState?.Invoke(sequenceString);
                NewMagicCreated?.Invoke(new MagicNotification(rune1.patternType, rune2.statusEffectType));
                _lastmagicCreationTImeUtc = DateTime.UtcNow;
                _magicCoolDown = MagicCoolDownAction();
            }
        }


        private void Update()
        {
            if (_magicCoolDown.Status == TaskStatus.Running)
            {
                return;
            }
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (_inputSequence.Count == 4)
                {
                    return;
                }

                if (!Input.GetKeyDown(keyCode) || keyCode.IsArrowKey() || keyCode.IsUtilityKey())
                {
                    continue;
                }

                if (_runesController.RunesFirstLetter.Value.Contains(keyCode) && _inputSequence.Count % 2 == 0
                    || _runesController.RunesSecondLetter.Value.Contains(keyCode) && _inputSequence.Count % 2 == 1)
                {
                    _inputSequence.Add(keyCode.ToString());
                    continue;
                }

                _inputSequence.Clear();
                break;

            }

        }




    }
}
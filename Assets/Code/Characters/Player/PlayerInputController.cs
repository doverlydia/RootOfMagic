using Extensions;
using Notification;
using Runes;
using System;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private RunesController _runesController;
        private ReactiveCollection<string> _inputSequence = new();

        public UnityEvent<string> NewUserInputState = new();
        // new Megic created event
        public UnityEvent<MagicNotification> NewMagicCreated = new();
        public static PlayerInputController Instance { get; private set; }

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
            _inputSequence.ObserveCountChanged().Subscribe(OnInputSequenceChanged);
            _inputSequence.ObserveAdd().Subscribe(OnInputSequenceGrew);
        }

        private void OnInputSequenceChanged(int count)
        {
            var sequenceString = string.Join("", _inputSequence.ToList());
            NewUserInputState.Invoke(sequenceString);
            print($"changed: {string.Join("", _inputSequence)}");
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
                var rune1 = _runesController.GetRunes().First(r => string.Equals(r.Syllable, rune1Syllable, StringComparison.CurrentCultureIgnoreCase));
                var rune2 = _runesController.GetRunes().First(r => string.Equals(r.Syllable, rune2Syllable, StringComparison.CurrentCultureIgnoreCase));
                NewMagicCreated.Invoke(new MagicNotification(rune1.patternType, rune2.statusEffectType));
                _inputSequence.Clear();
            }
        }


        private void Update()
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {

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
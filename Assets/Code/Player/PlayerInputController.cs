using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Code.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private RunesController _runesController;
        private InputAction  _runeLetterPressed;
        private List<string> _inputSequence = new List<string>();
        private HashSet<KeyCode> _acceptableInputs = new HashSet<KeyCode>();

        private void Awake()
        {
            
        }

        private void Start()
        {
            RebindRuneControls();
            _acceptableInputs = _runesController.GetLetters()
                .Select(c=>(KeyCode)Enum.Parse(typeof(KeyCode),$"{c}"))
                .Append(KeyCode.UpArrow)
                .Append(KeyCode.DownArrow)
                .Append(KeyCode.LeftArrow)
                .Append(KeyCode.RightArrow)
                .ToHashSet();
            
        }

        private void Update()
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode) && !_acceptableInputs.Contains(keyCode))
                    {
                        _inputSequence.Clear();
                    }
                }

            print($"input queue: {string.Join(", ", _inputSequence)}");
        }

        private void RebindRuneControls()
        {
            var map = new InputActionMap("Gameplay");
            _runeLetterPressed = map.AddAction("RuneLetterPressed");
            foreach (var letter in _runesController.GetLetters())
            {
                _runeLetterPressed.AddBinding($"<Keyboard>/{letter.ToString().ToLower()}");
            }
            _runeLetterPressed.Enable();
            _runeLetterPressed.started += OnRuneLetterPressed;

        }

        void OnRuneLetterPressed(InputAction.CallbackContext callbackContext)
        {
            _inputSequence.Add(callbackContext.control.displayName);
        }

       


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Runes
{
    public class RunesController : MonoBehaviour
    {
        [SerializeField] private SyllablesScriptableObject syllables;

        [SerializeField] private List<(StatusEffectType statusEffect, PatternType pattern)> _runeCombinations =
            new()
            {
                (StatusEffectType.DamageOverTime, PatternType.Beam),
                (StatusEffectType.Slow, PatternType.DamageField),
                (StatusEffectType.Leech, PatternType.Companion)
            };

        private Dictionary<string, Rune> _runes = new();
        public Lazy<HashSet<KeyCode>> RunesFirstLetter { get; private set; }
        public Lazy<HashSet<KeyCode>> RunesSecondLetter { get; private set; }

        public IEnumerable<Rune> GetRunes()
        {
            return _runes.Values;
        }

        public Rune GetRuneBySyllable(string syllable)
        {
            return _runes[syllable.ToUpper()];
        } 

        protected void Awake()
        {
            InitRunes();
            RunesFirstLetter = new Lazy<HashSet<KeyCode>>(() =>
            {
                return _runes.Keys.Select(syllable =>(KeyCode)Enum.Parse(typeof(KeyCode),  syllable.Substring(0,1))).ToHashSet();
            });
            RunesSecondLetter = new Lazy<HashSet<KeyCode>>(() =>
            {
                return _runes.Keys.Select(syllable => (KeyCode)Enum.Parse(typeof(KeyCode),  syllable.Substring(1,1))).ToHashSet();;
            });

#if UNITY_EDITOR
            PrintRunes();
#endif
        }

        private void InitRunes()
        {
           
            var selectedSyllables = SelectSyllables(syllables.availableSyllables, _runeCombinations.Count);
            for (int i = 0; i < _runeCombinations.Count; i++)
            {
                var magic = _runeCombinations[i];
                var syllable = selectedSyllables[i];
                _runes[syllable] = new Rune
                {
                    Syllable = syllable,
                    statusEffectType = magic.statusEffect,
                    patternType = magic.pattern
                };
                
            }

            
        }

        private List<string> SelectSyllables(IEnumerable<string> syllablesOptions, int amount)
        {
            var random = new Random();
            HashSet<char> usedLetters = new HashSet<char>();
            List<string> selectedSyllables = new List<string>();
            for (int i = 0; i < amount; i++)
            {
                var selectedSyllable = syllablesOptions
                    .Where(s => !s.Any(c => usedLetters.Contains(c)))
                    .OrderBy(_ => random.NextDouble())
                    .First();
                
                selectedSyllables.Add(selectedSyllable);
                foreach (var c in selectedSyllable)
                {
                    usedLetters.Add(c);
                }

            }

            return selectedSyllables;

        }

        private void PrintRunes()
        {
            var output = "Runes dict: \n";
            foreach (var rune in _runes)
            {
                output +=
                    $"{rune.Key} : {rune.Value.Syllable}, {rune.Value.patternType}, {rune.Value.statusEffectType} \n";
            }

            Debug.Log(output);
        }
    }
}
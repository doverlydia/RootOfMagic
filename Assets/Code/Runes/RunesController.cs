using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Code.Runes
{
    public class RunesController : MonoBehaviour
    {
        [SerializeField] private SyllablesScriptableObject syllables;

        private Dictionary<string, Rune> _runes = new();
        private HashSet<char> _usedLetters;

        public IEnumerable<Rune> GetRunes()
        {
            return _runes.Values;
        }

        public HashSet<char> GetLetters()
        {
            if (_usedLetters == null)
            {
                _usedLetters = _runes.Keys.SelectMany(syllable => syllable.ToCharArray()).ToHashSet();
            }
            return _usedLetters;
        }

        protected void Awake()
        {
            InitRunes();

#if UNITY_EDITOR
            PrintRunes();
#endif
        }

        private void InitRunes()
        {
            var magics = new List<(StatusEffect statusEffect, ProjectilePattern pattern)>()
            {
                new (StatusEffect.DamageOverTime, ProjectilePattern.Beam),
                new (StatusEffect.Slow, ProjectilePattern.DamageField),
                new (StatusEffect.Leech, ProjectilePattern.Companion),
                    
            };
            var selectedSyllables = SelectSyllables(syllables.availableSyllables, magics.Count);
            for (int i = 0; i < magics.Count; i++)
            {
                var magic = magics[i];
                var syllable = selectedSyllables[i];
                _runes[syllable] = new Rune
                {
                    Syllable = syllable,
                    StatusEffect = magic.statusEffect,
                    ProjectilePattern = magic.pattern
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
                    $"{rune.Key} : {rune.Value.Syllable}, {rune.Value.ProjectilePattern}, {rune.Value.StatusEffect} \n";
            }

            Debug.Log(output);
        }
    }
}
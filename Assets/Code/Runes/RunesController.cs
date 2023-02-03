using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Runes
{
    public class RunesController : MonoBehaviour
    {
        [SerializeField] private SyllablesScriptableObject syllables;

        private Dictionary<string, Rune> _runes = new();

        public IEnumerable<Rune> GetRunes()
        {
            return _runes.Values;
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
            // Just cloning so we won't change the original syllables
            var unusedSyllables = syllables.availableSyllables.ToList();

            var rune = GenerateRuneWithRandomSyllable(unusedSyllables, StatusEffect.DamageOverTime,
                ProjectilePattern.Beam);
            _runes.Add(rune.Syllable, rune);

            rune = GenerateRuneWithRandomSyllable(unusedSyllables, StatusEffect.Slow, ProjectilePattern.DamageField);
            _runes.Add(rune.Syllable, rune);

            rune = GenerateRuneWithRandomSyllable(unusedSyllables, StatusEffect.Leech, ProjectilePattern.Companion);
            _runes.Add(rune.Syllable, rune);
        }

        private Rune GenerateRuneWithRandomSyllable(List<string> unusedSyllables, StatusEffect statusEffect,
            ProjectilePattern projectilePattern)
        {
            var syllableToUse = unusedSyllables.RemoveRandom();
            return new Rune()
            {
                Syllable = syllableToUse,
                StatusEffect = statusEffect,
                ProjectilePattern = projectilePattern
            };
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
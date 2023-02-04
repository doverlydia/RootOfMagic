using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Characters.Enemy;
using Magics.Patterns;
using Magics.StatusEffects;
using UnityEngine;

namespace Magics
{
    public class Magic : MonoBehaviour
    {
        public Pattern Pattern;
        public StatusEffect statusEffect;
        public DateTime _lastTickTime;
        public DateTime _startingTime;
        public TimeSpan tickInterval;
        public Magic(Pattern pattern, StatusEffect statusEffect)
        {
            Pattern = pattern;
            this.statusEffect = statusEffect;
        }

        private void Awake()
        {
            _startingTime = DateTime.UtcNow;
            _lastTickTime = DateTime.Now;
            tickInterval = TimeSpan.FromSeconds(1 / (float)Pattern.TicksPerSecond) ;
            Pattern.enabled = true;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if ((DateTime.Now - _startingTime).Seconds >= Pattern.Duration.Value)
            {
                Destroy(gameObject);
            }

            if ((DateTime.Now - _lastTickTime) < tickInterval)
            {
                return;
            }
            _lastTickTime = DateTime.Now;

            foreach(var enemy in GetEnemiesInPatternArea())
            {
                ApplyDamage(enemy);
                ApplyStatusEffect(enemy);
            }
        }

        IEnumerable<Enemy> GetEnemiesInPatternArea()
        {
            var cols = new List<Collider2D>();
            var amount = Physics2D.OverlapCollider(Pattern.Aoe,new ContactFilter2D().NoFilter() ,cols);
            List<Enemy> results = new();
            foreach (Collider2D collider in cols)
            {
                if (!collider.TryGetComponent(out Enemy ec))
                {
                    continue;
                }
                results.Add(ec);
            }
            return results;
        }

        void ApplyDamage(Enemy enemy)
        {
            enemy.CurrentHp -= Pattern.BaseDamage * statusEffect.DamageMultiplier;
        }

        void ApplyStatusEffect(Enemy enemy)
        {
             
            switch (statusEffect)
            {
                case DOT dot:
                {
                    var createdStatusEffect = gameObject.AddComponent(statusEffect.GetType()) as DOT;
                    createdStatusEffect.Init(dot);
                    createdStatusEffect.enabled = true;
                    break;
                }
                case Slow slow:
                {
                    var createdStatusEffect = gameObject.AddComponent(statusEffect.GetType()) as Slow;
                    createdStatusEffect.Init(slow);
                    createdStatusEffect.enabled = true;
                    break;
                }
                case Leech leech:
                {
                    var createdStatusEffect = gameObject.AddComponent(statusEffect.GetType()) as Leech;
                    createdStatusEffect.Init(leech);
                    createdStatusEffect.enabled = true;
                    break;
                }


            }
        }
    }
}

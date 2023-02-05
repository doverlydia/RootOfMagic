using Characters.Enemy;
using Cysharp.Threading.Tasks;
using Magics.StatusEffects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHub : MonoBehaviour
{
    public GameObject slow;
    public GameObject damageOverTime;
    public TMP_Text damageText;
    public Enemy enemy;

    private void Awake()
    {
        Enemy.EnemyHit.AddListener(async (guid, vector, damage) =>
        {
            if (enemy.Id != guid) return;
            damageText.text = damage.ToString();
            await UniTask.Delay(700);
            damageText.text = "";
        });

        StatusEffect.CharacterEffected.AddListener(async (target, effect) =>
        {
            if (target != enemy) return;
            if (effect is not Slow or DOT) return;
            if (effect is Slow slow)
            {
                this.slow.SetActive(true);
                await UniTask.Delay((int)slow._duration * 1000);
                
                if (this.slow != null)
                {
                    this.slow.SetActive(false);
                }
            }
            else if (effect is DOT dot)
            {
                damageOverTime.SetActive(true);
                await UniTask.Delay((int)dot._duration * 1000);
                
                if (this.damageOverTime != null)
                {
                    damageOverTime.SetActive(false);
                }
            }
        });
    }
}

using Characters.Enemy;
using System;
using UnityEngine;

public class EnemyEffectHandler : MonoBehaviour
{
    [SerializeField] private float enemyHitEffectDuration = 0.5f;
    [SerializeField] private Color enemyHitEffectColor = Color.red;
    [SerializeField] private GameObject enemyDeathParticles;


    private void Start()
    {
        Enemy.EnemyHit.AddListener(OnEnemyHit);
        Enemy.EnemyDied.AddListener(OnEnemyDie);
    }

    private void OnEnemyHit(Guid enemyID, Vector3 pos, float amount)
    {
        //Bleh, nevermind

        /*
       Transform enemyTrans = EnemyController.Instance.GetEnemyById(enemyID).transform;
        enemyTrans.DOKill();
        DOTWeenCustom.SquashNStretch(enemyTrans, new Vector2(1.2f, 0.8f), enemyHitEffectDuration, true);
        SpriteRenderer enemySR = enemyTrans.GetComponentInChildren<SpriteRenderer>();
        enemySR.DOKill();
        enemySR.DOColor(enemyHitEffectColor, enemyHitEffectDuration / 2f).SetLoops(2, LoopType.Yoyo);
        */
    }

    private void OnEnemyDie(Guid enemyID, Vector3 pos)
    {
        Instantiate(enemyDeathParticles, pos, Quaternion.identity);
    }

    private void OnDestroy()
    {
        Enemy.EnemyHit.RemoveListener(OnEnemyHit);
        Enemy.EnemyDied.RemoveListener(OnEnemyDie);
    }
}

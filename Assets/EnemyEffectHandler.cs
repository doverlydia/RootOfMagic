using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Characters.Enemy;
using DG.Tweening;

public class EnemyEffectHandler : MonoBehaviour
{
    [SerializeField] private float enemyHitEffectDuration = 0.5f;
    [SerializeField] private Color enemyHitEffectColor = Color.red;


    private void Start()
    {
        Enemy.EnemyHit.AddListener(OnEnemyHit);
    }

    private void OnEnemyHit(Guid enemyID, Vector3 pos, float amount)
    {
       Transform enemyTrans = EnemyController.Instance.GetEnemyById(enemyID).transform;
        DOTWeenCustom.SquashNStretch(enemyTrans, new Vector2(1.2f, 0.8f), enemyHitEffectDuration, true);
        enemyTrans.GetComponentInChildren<SpriteRenderer>()
            .DOColor(enemyHitEffectColor, enemyHitEffectDuration / 2f).SetLoops(2, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        Enemy.EnemyHit.RemoveListener(OnEnemyHit);
    }
}

using Characters.Enemy;
using Characters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgSound;
    [SerializeField] AudioSource enemyDead;
    [SerializeField] AudioSource playerDamage;
    [SerializeField] AudioSource playerDead;
    [SerializeField] AudioSource goodSyllable;
    [SerializeField] AudioSource badSyllable;
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SceneManager.sceneLoaded += OnGameLoaded;

    }
    private void OnGameLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 1)
        {
            return;
        }

        Enemy.EnemyDied.RemoveListener((x, y) => enemyDead.Play());
        PlayerController.Instance.PlayerDamage.PlayerDead.RemoveListener(() => {
            playerDead.Play();
            Debug.Log("player is dead");
        });
        PlayerController.Instance.PlayerDamage.PlayerDamaged.RemoveListener(() => playerDamage.Play());

        Enemy.EnemyDied.AddListener((x, y) => enemyDead.Play());
        PlayerController.Instance.PlayerDamage.PlayerDead.AddListener(() => playerDead.Play());
        PlayerController.Instance.PlayerDamage.PlayerDamaged.AddListener(() => playerDamage.Play());
    }

}

using Characters.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEngine.SceneManagement.Scene;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgSound;
    [SerializeField] AudioSource playerDamage;
    [SerializeField] AudioSource playerDead;
    [SerializeField] Toggle soundToggle;
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
        soundToggle.onValueChanged.AddListener(IsSound);

    }
    private void OnGameLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 1)
        {
            return;
        }

        IsSound(soundToggle.isOn);
    }

    private void StopSound()
    {
        bgSound.Stop();
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            return;
        }
        PlayerController.Instance.PlayerDamage.PlayerDead.RemoveListener(() =>
            playerDead.Play());
        PlayerController.Instance.PlayerDamage.PlayerDamaged.RemoveListener(() => playerDamage.Play());
    }

    private void PlaySound()
    {
        bgSound.Play();
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            return;
        }
        PlayerController.Instance.PlayerDamage.PlayerDead.AddListener(() => playerDead.Play());
        PlayerController.Instance.PlayerDamage.PlayerDamaged.AddListener(() => playerDamage.Play());
    }

    public void IsSound(bool isSound)
    {
        if (isSound)
        {
            StopSound();
            PlaySound();
        }
        else
        {
            StopSound();
        }
    }

}

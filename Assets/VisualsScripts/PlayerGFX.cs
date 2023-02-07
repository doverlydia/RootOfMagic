using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Characters.Player;
using Notification;
using Player;

public class PlayerGFX : MonoBehaviour
{
    [SerializeField] private Texture[] playerFaceSprites;
    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private float hurtFaceDuration = 2f;
    [SerializeField] private float happyFaceDuration = 1.5f;
    [SerializeField] private Color hurtColor;
    [SerializeField] private float hurtEffectDuration;
    [ColorUsage(true, true)]
    [SerializeField] private Color glowColor;
    Tween playerFaceTimerTween;
    private float lastHealthAmount;
    bool initializedHealth;
   // private Sequence squashSeqTest;

    private void Start()
    {
        //squashSeqTest = DOTWeenCustom.SquashNStretch(transform, new Vector2(1.2f, 0.8f), hurtEffectDuration, true).SetAutoKill(false);
        PlayerController.Instance.PlayerHealthChangedEvent.AddListener(UpdateLocalPlayerHealth);
        PlayerInputController.Instance.NewMagicCreated.AddListener(OnDoMagicPlayerGFX);
        //lastHealthAmount = PlayerController.Instance.CurrentHp.Value;
    }

    private void UpdateLocalPlayerHealth(float newHealth, float newMaxHp)
    {
        float healthRatio = newHealth / newMaxHp;
        if (!initializedHealth) lastHealthAmount = healthRatio;
        print("last, new: " + lastHealthAmount + ", " + healthRatio + " - " + (lastHealthAmount > healthRatio));
        if(lastHealthAmount > healthRatio || !initializedHealth)
        {
            initializedHealth = true;
            //Player was hurt
            PlayerHurtGFX();
        }

        lastHealthAmount = healthRatio;
    }

    private void PlayerHurtGFX()
    {
        PlayerDoFace(PlayerFace.Angry, hurtFaceDuration);

        transform.DOKill();
        SR.DOKill();
        DOTWeenCustom.SquashNStretch(transform, new Vector2(1.2f, 0.8f), hurtEffectDuration, true);
        SR.DOColor(hurtColor, hurtEffectDuration / 2f).SetLoops(2, LoopType.Yoyo);
    }

    private void OnDoMagicPlayerGFX(MagicNotification notification)
    {
        //if(notification.PatternType == Runes.PatternType.Beam)
       // {
            PlayerDoFace(PlayerFace.Smile, happyFaceDuration);
        //}

        SR.material.DOColor(glowColor, "_EmissionColor", happyFaceDuration / 2f).SetLoops(2, LoopType.Yoyo);
    }

    private Texture FaceToTexture(PlayerFace face)
    {
        return playerFaceSprites[(int)face];
    }

    private void Update()
    {
        /* //Test the faces
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayerDoFace(PlayerFace.Default, 3f);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlayerDoFace(PlayerFace.Smile, 3f);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            PlayerDoFace(PlayerFace.Angry, 3f);
        */
        /*
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            squashSeqTest.Restart();
        }*/
    }

    public void PlayerDoFace(PlayerFace face, float duration)
    {
        playerFaceTimerTween?.Kill();
        SetFace(FaceToTexture(face));

        float timer = 0;
        playerFaceTimerTween = DOTween.To(() => timer, x => timer = x, 1, duration);

        playerFaceTimerTween.OnComplete(() =>
        {
            SetFace(FaceToTexture(PlayerFace.Default));
            playerFaceTimerTween = null;
        });

    }

    private void SetFace(Texture faceSprite)
    {
        SR.material.SetTexture("_OverlaySprite", faceSprite);
    }
}

public enum PlayerFace
{
    Default,
    Smile,
    Angry
}

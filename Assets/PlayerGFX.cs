using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerGFX : MonoBehaviour
{
    [SerializeField] private Texture[] playerFaceSprites;
    [SerializeField] private SpriteRenderer SR;
    Tween playerFaceTimerTween;

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

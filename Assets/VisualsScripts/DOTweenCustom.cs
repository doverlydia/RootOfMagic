using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class DOTWeenCustom
{
    //Full control Squash & Stretch (can control how long it takes to squash and how long to return to normal)

    public static Sequence SquashNStretch(Transform target, Vector2 scaleValues, float goDuration, float returnDuration, bool accountForPosition)
    {
        Vector2 initialScale = target.localScale;

        Sequence seq = null;
        if (accountForPosition)
        {
            float multiplier = (scaleValues.y > 1) ? 1 : -1;
            float squashOffset = (1 - scaleValues.y) / 2;

            seq = DOTween.Sequence();
            seq
                .Append(target.DOScale(new Vector3(scaleValues.x * initialScale.x, scaleValues.y * initialScale.y), goDuration))
                .Join(target.DOBlendableLocalMoveBy(new Vector2(0, (squashOffset * multiplier)), goDuration))
                .Append(target.DOScale(new Vector3(initialScale.x, initialScale.y), returnDuration))
                .Join(target.DOBlendableLocalMoveBy(new Vector2(0, squashOffset * -multiplier), returnDuration));
        }
        else
        {
            seq = DOTween.Sequence();
            seq
                .Append(target.DOScale(new Vector3(scaleValues.x * initialScale.x, scaleValues.y * initialScale.y), goDuration))
                .Append(target.DOScale(new Vector3(initialScale.x, initialScale.y), returnDuration));
        }

        seq.OnKill(() => { target.localScale = initialScale; target.localPosition = Vector3.zero; });
        return seq;
    }

    //Simplified Squash & Stretch (duration for squash and return are equal)

    public static Sequence SquashNStretch(Transform target, Vector2 scaleValues, float duration, bool accountForPosition)
    {
        return SquashNStretch(target, scaleValues, duration / 2, duration / 2, accountForPosition);
    }

    //Same as simplified Squash & Stretch but can set Ease

    public static Sequence SquashNStretch(Transform target, Vector2 scaleValues, float duration, Ease ease, bool accountForPosition)
    {
        Sequence seq = SquashNStretch(target, scaleValues, duration, accountForPosition);
        seq.SetEase(ease);

        return seq;
    }

}

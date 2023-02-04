using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CompanionFlipWithMovement : MonoBehaviour
{
    Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        transform.ObserveEveryValueChanged(x => x.position).Subscribe(OnCompanionMoved);
    }

    private void OnCompanionMoved(Vector3 pos)
    {
        //do stuff when player moves
        if(pos.x - lastPos.x > 0 && transform.localScale.x != 1f)
            SetXScale(1f);
        else 
        if (pos.x - lastPos.x < 0 && transform.localScale.x != -1f)
            SetXScale(-1f);

        lastPos = transform.position;
    }

    private void SetXScale(float scale)
    {
        Vector3 temp = transform.localScale;
        temp.x = scale;
        transform.localScale = temp;
    }
}

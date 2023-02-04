using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Threading.Tasks;
public class CompanionFlipWithMovement : MonoBehaviour
{
    [SerializeField] private float minimumDistanceToConsider = 0.0005f;
    Vector3 lastPos;
    [SerializeField] float MaxColadown = 0.5f;
    float CurrentCooldaon = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        transform.ObserveEveryValueChanged(x => x.position).Subscribe(OnCompanionMoved);
    }

    private async void OnCompanionMoved(Vector3 pos)
    {
        //More perfomative Mathf.abs
        //do stuff when player moves
        if (CurrentCooldaon > 0) return;
        if (pos.x - lastPos.x > 0 && transform.localScale.x != 1f)
            SetXScale(1f);
        else
        if (pos.x - lastPos.x < 0 && transform.localScale.x != -1f)
            SetXScale(-1f);

        lastPos = transform.position;
    }
    private void Update()
    {
        CurrentCooldaon -= Time.deltaTime;
    }
    private void SetXScale(float scale)
    {
        CurrentCooldaon = MaxColadown;
        Vector3 temp = transform.localScale;
        temp.x = scale;
        transform.localScale = temp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Threading.Tasks;
public class PlayerControlAnimation : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.ObserveEveryValueChanged(x => x.position).Subscribe(OnPlayerMoved);
    }

    private async void OnPlayerMoved(Vector3 pos)
    {
        //do stuff when player moves
        anim.SetBool("isMoving", true);
        await Task.Delay(20);
        if (transform.position==pos)
        {
            // do stuff when player dont move
            anim.SetBool("isMoving", false);
        }
    }
}

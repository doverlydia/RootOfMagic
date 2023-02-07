using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Threading.Tasks;
public class PlayerControlAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SR;
    private Animator anim;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lastPos = transform.position;
        transform.ObserveEveryValueChanged(x => x.position).Subscribe(OnPlayerMoved);
    }

    private async void OnPlayerMoved(Vector3 pos)
    {
        //do stuff when player moves
        anim.SetBool("isMoving", true);
        HandleFlip(pos);
        await Task.Delay(20);
        if (transform.position==pos)
        {
            // do stuff when player dont move
            anim.SetBool("isMoving", false);
        }
    }

    private void HandleFlip(Vector3 pos)
    {
        //do stuff when player moves
        if (pos.x - lastPos.x > 0 && !SR.flipX)
            SR.flipX = true;
        else
        if (pos.x - lastPos.x < 0 && SR.flipX)
            SR.flipX = false;

        lastPos = transform.position;
    }
}

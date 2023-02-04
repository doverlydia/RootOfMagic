using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public async void Play()
    {
        await UniTask.Delay(1000);
        SceneManager.LoadScene(1);
    }
}

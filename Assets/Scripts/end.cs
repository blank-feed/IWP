using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{
    public int bgm_num;

    void Start()
    {
        MusicPlayer.instance.PlayBGM(bgm_num);
    }
}

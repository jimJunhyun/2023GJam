using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SoundReturn : MonoBehaviour
{
    private int bpm =0;
    private AudioClip _audio;
    
    public void Init(int a, AudioClip b)
    {
        DontDestroyOnLoad(this.gameObject);
        bpm = a;
        _audio = b;
    }

    public int BPM()
    {
        return bpm;
    }

    public AudioClip Audio()
    {
        return _audio;
    }
}

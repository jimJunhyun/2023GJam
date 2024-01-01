using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleRhythmObject : MonoBehaviour,IRhythm
{
    private int t = 0;
    public void BeatUpdate()
    {
        Debug.Log(t++);
    }

    public void BeatUpdateDivideFour()
    {
        
    }
}

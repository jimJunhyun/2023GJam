using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeatSystem : MonoBehaviour
{
    [SerializeField] float BPM = 100f;
    [SerializeField] int Matronyum = 4;

    private int _matCount = 1;
    private float _currentTime = 0f;
    
    
    private List<IRhythm> FindRhythms()
    {
        return FindObjectsOfType<MonoBehaviour>().OfType<IRhythm>().ToList();
    }

    private void Update()
    {
        float BPS = (BPM / 60f); // 1.6666 한 마디
        float beat = 1.0f / (BPS); // 한 박자
        //Debug.Log(beat);
        _currentTime += Time.deltaTime;
        
        if (_currentTime >= beat)
        {

            GetComponent<AudioSource>().Play();
            RhythmUpdating();
        }
    }

    void RhythmUpdating()
    {
        //Debug.Log(_currentTime);
        _currentTime = 0;
        _matCount++;
        if (_matCount >= Matronyum)
        {
            _matCount = 1;
            var objects = FindRhythms();

            foreach (var Rhythm in objects)
            {
                Rhythm.BeatUpdate();
            }
        }
    }
    
}

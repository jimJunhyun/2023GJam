using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BeatSystem : Singleton<BeatSystem>
{
    [SerializeField] float BPM = 100f;
    [SerializeField] int Matronyum = 4;

    [SerializeField] private float Delay = 0f;
    
    private bool isRecord = true;

    private int _matCount = 1;
    private float _currentTime = 0f;


    private List<IRhythm> FindRhythms()
    {
        return FindObjectsOfType<MonoBehaviour>().OfType<IRhythm>().ToList();
    }

    public float BeatValue()
    {
        float BPS = (BPM / 60f); // 1.6666 한 마디
        float beat = 1.0f / (BPS); // 한 박자
        return beat;
    }
    
    public float BeatSecond()
    {
        float BPS = (BPM / 60f); // 1.6666 한 마디
        float beat = 1.0f / (BPS); // 한 박자
        return beat * Matronyum;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
        {
            BeatUISystem.Instance.HitNode();
            //GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            BeatUISystem.Instance.InstanciateRecordNode();
            //GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            BeatUISystem.Instance.RemoveNode();
            BeatUISystem.Instance.HitNode();
        }
        
        //Debug.Log(beat);
        _currentTime += Time.deltaTime;
        
        if (_currentTime >= BeatValue())
        {
            GetComponent<AudioSource>().Play();
            _currentTime = 0;
            StartCoroutine(Beating());

        }
    }

    IEnumerator Beating()
    {
        yield return new WaitForSeconds(Delay);
        //BeatUISystem.Instance.Invoke();
        RhythmUpdating();
    }

    void RhythmUpdating()
    {
        //Debug.Log(_currentTime);
        if (_matCount >= Matronyum)
        {
            
            //BeatUISystem.Instance.ResetHitBoard();
            BeatUISystem.Instance.InstanciateNode();
            
            _matCount = 1;
            var objects = FindRhythms();

            foreach (var Rhythm in objects)
            {
                Rhythm.BeatUpdate();
            }
        }
        else
        {
            _matCount++; 
            
            var objects = FindRhythms();

            foreach (var Rhythm in objects)
            {
                Rhythm.BeatUpdateDivideFour();
            }
        }
    }
    
}

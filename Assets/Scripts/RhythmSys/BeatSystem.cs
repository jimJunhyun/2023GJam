using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BeatSystem : Singleton<BeatSystem>
{
    [Header("Audio")] 
    public AudioClip _matronyum;
    public AudioClip _Hit;
    [SerializeField] float BPM = 100f;
    [SerializeField] int Matronyum = 4;

    [SerializeField] private float Delay = 0f;
    
    private bool isRecord = true;

    private int _matCount = 1;
    private float _currentTime = 0f;
    private float _currentBeatValue = 0f;

    private int _addBeatCount = 0;
    private int _removeBeatCount = 0;
    
    private Inventory _inven;
    
    private void Awake()
    {
        _currentBeatValue = BeatValue();
        ;
        _inven = GameManager.Instance.player.Inven;
    }

    public float ReturnBPM
    {
        get
        {
            return BPM;
        }
    }

    private List<IRhythm> FindRhythms()
    {
        return FindObjectsOfType<MonoBehaviour>().OfType<IRhythm>().ToList();
    }

    public float BeatValue()
    {
        float BPS;
//        ItemSO SO = GameManager.instance.player.Inven.ReturnItemRule();


        BPS = BPM / 60f;  //SO == null ? BPM / 60f : SO.SetBPM == 0 ? (BPM + SO.AddBPM) / 60f  : (SO.SetBPM + SO.AddBPM) / 60f;
            
        
        
        //= (BPM ) / 60f; // 1.6666 한 마디
        float beat = 1.0f / (BPS); // 한 박자
        return beat;
    }
    
    public float BeatSecond()
    {
        return BeatValue() * Matronyum;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
        {
            GameManager.Instance.player.Inven.HitInvoking();
            BeatUISystem.Instance.HitNode();
            GetComponent<AudioSource>().PlayOneShot(_Hit);
        }

        if (Input.GetKeyDown(KeyCode.I)) // 1 + 아이템 + 플레이어
        {
            int a = 1;
            if (_inven.ReturnItemRule() != null)
            {
                a += _inven.ReturnItemRule().AddBeat;
            }

            if (a > _addBeatCount || (BeatUISystem.Instance.HitCount <4 && BeatUISystem.Instance.RecordCount < 4))
            {
                _addBeatCount++;
                BeatUISystem.Instance.InstanciateRecordNode();
            }

            //GetComponent<AudioSource>().PlayOneShot(_Hit);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            int a = 1;
            if (_inven.ReturnItemRule() != null)
            {
                a += _inven.ReturnItemRule().RemoveBeat;
            }

            if (a > _removeBeatCount)
            {
                _removeBeatCount++;
                GameManager.Instance.player.Inven.HitInvoking();
            }
            
            GameManager.Instance.player.Inven.HitInvoking();
            GetComponent<AudioSource>().PlayOneShot(_Hit);
            BeatUISystem.Instance.RemoveNode();
            BeatUISystem.Instance.HitNode();
        }
        
        //Debug.Log(beat);
        _currentTime += Time.deltaTime;
        
        if (_currentTime >= _currentBeatValue)
        {

            GetComponent<AudioSource>().PlayOneShot(_matronyum);
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
            
            GameManager.Instance.player.PlayerUI.InvaligateBPM((int)ReturnBPM);
            
            _addBeatCount = 0;
            _removeBeatCount = 0;
            _currentBeatValue = BeatValue();
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

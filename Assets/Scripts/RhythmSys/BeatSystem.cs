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
    public AudioClip _j;
    public AudioClip _k;
    public AudioClip _i;
    public AudioClip _o;
    [SerializeField] float BPM = 100f;
    [SerializeField] int Matronyum = 4;

    [SerializeField] private float Delay = 0f;
    
    private bool isRecord = true;

    private int _matCount = 1;
    private float _currentTime = 0f;
    private float _currentBeatValue = 0f;

    private int _addBeatCount = 0;
    private int _removeBeatCount = 0;

    
    
    private void Awake()
    {
        _currentBeatValue = BeatValue();


    }

    public float ReturnBPM
    {
        get
        {
            return BPM;
        }
        set
        {
            BPM = value;
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

        //Debug.Log(beat);
        _currentTime += Time.deltaTime;
        
        if (_currentTime >= _currentBeatValue)
        {

            if (_matronyum)
            {
                //SoundManager.Instance.PlaySFX(_matronyum);

                _currentTime = 0;
                StartCoroutine(Beating());
            }
        }
        if (GameManager.Instance)
        if (GameManager.Instance.player.IsInteractive)
            return;

        #region 인풋

        if (GameManager.Instance && GameManager.Instance.player)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
            
                GameManager.Instance.player.Inven.HitInvoking();
                BeatUISystem.Instance.HitNode(_j, SoundSetting.KickDrumSFX);
            
            }
            if(Input.GetKeyDown(KeyCode.K))
            {
                GameManager.Instance.player.Inven.HitInvoking();
                BeatUISystem.Instance.HitNode(_k, SoundSetting.SnareDrumSFX);
            }

            if (Input.GetKeyDown(KeyCode.I)) // 1 + 아이템 + 플레이어
            {
                int a = 1;
                if (GameManager.Instance.player.Inven.ReturnItemRule() != null)
                {
                    a += GameManager.Instance.player.Inven.ReturnItemRule().AddBeat;
                }

                if (a > _addBeatCount || (BeatUISystem.Instance.HitCount <4 && BeatUISystem.Instance.RecordCount < 4))
                {
                    _addBeatCount++;
                    BeatUISystem.Instance.InstanciateRecordNode();
                    SoundManager.Instance.PlaySFX(_i, SoundSetting.KickDrumSFX); //ㅁ?ㄹ
                }

                //GetComponent<AudioSource>().PlayOneShot(_Hit);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                int a = 1;
                if (GameManager.Instance.player.Inven.ReturnItemRule() != null)
                {
                    a += GameManager.Instance.player.Inven.ReturnItemRule().RemoveBeat;
                }

                if (a > _removeBeatCount)
                {
                    _removeBeatCount++;
                    GameManager.Instance.player.Inven.HitInvoking();
            
                    BeatUISystem.Instance.RemoveNode();
                    BeatUISystem.Instance.HitNode(_o, SoundSetting.KickDrumSFX);
                }
            

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                SoundManager.Instance.PlaySFX(_j, SoundSetting.KickDrumSFX);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                SoundManager.Instance.PlaySFX(_k, SoundSetting.SnareDrumSFX);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                SoundManager.Instance.PlaySFX(_i, SoundSetting.KickDrumSFX); //어짜피 지운다네요 암거나 넣음
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                SoundManager.Instance.PlaySFX(_o, SoundSetting.KickDrumSFX); //222
            }
        }
        
       

        #endregion
        

        
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
            _addBeatCount = 0;
            _removeBeatCount = 0;
            _currentBeatValue = BeatValue();
            
            if (GameManager.Instance && GameManager.Instance.player)
            {
                GameManager.Instance.player.PlayerUI.InvaligateBPM((int)ReturnBPM);
                BeatUISystem.Instance.InstanciateNode();
            }
            
            //BeatUISystem.Instance.ResetHitBoard();

            
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

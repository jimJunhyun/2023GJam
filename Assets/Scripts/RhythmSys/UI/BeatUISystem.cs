using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum Detection
{
    Perfect,
    Good,
    Bad,
    none
}

public class BeatUISystem : Singleton<BeatUISystem>
{
    
     public BeatLineUI _beatLineImg;
    public HitUI _beatImg;

    private List<HitUI> _hit = new();
    private List<HitUI> _record = new();

    [Header("UI")] public Transform BeatPos;
    
    [Header("Hit")]
    public RectTransform _startHitBeat;
    public RectTransform _endHitBeat;
    private BeatLineUI _hitBeatLine;

    [Header("Record")] public RectTransform _startRecordBeat;
    public RectTransform _endRecordBeat;
    private BeatLineUI _recordBeatLine;

    [Header("CurrentNode")] [SerializeField]private HitUI _nowNode;

    [Header("Curse")] [SerializeField] private CurseUI _curse;
    public CurseUI Curse => _curse;
    
    [Header("Effect")]
    public PoolAble _hitEffect;

    private int hitCount = 0;
    private int _perfectCount = 0;
    private bool PerfectMD = false;
    private int _goodCount = 0;
    private bool GoodMD = false;

    public int HitCount => _hit.Count;
    public int RecordCount => _record.Count;


    public bool IsPerfect
    {
        get
        {
            return PerfectMD;
        }
    }

    public void InstanciateNode()
    {
        
        //GameManager.Instance.player.Inven.AddCurse(CurseList.MadiSeal);
        PerfectMD = false;
        GoodMD = false;
        
        if (_perfectCount == _hit.Count)
        {
            Debug.Log("Perfect!!!!");
            PerfectMD = true;
        }

        if (_goodCount == _hit.Count)
        {
            GoodMD = true;
        }

        _perfectCount = 0;
        _goodCount = 0;
        hitCount = 0;
        
        _hitBeatLine = Instantiate(_beatLineImg,BeatPos);
        
        _hitBeatLine.Init(_startHitBeat, _endHitBeat);
        
        _recordBeatLine = Instantiate(_beatLineImg,BeatPos);
        
        _recordBeatLine.Init(_startRecordBeat, _endRecordBeat);

        if (GameManager.Instance.player && GameManager.Instance.player.Inven.FindCurse(CurseList.MadiSeal))
        {
            _curse.SetCurse(Random.Range(0,4));
        }
        else
        {
            _curse.SetCurse();
        }
        
        InvaligateNode();
    }

    public void ShootEffect(PoolAble _mono)
    {
        EffectObject ef = PoolManager.Instance.Pop(_mono.name) as EffectObject;
        Debug.LogWarning($"Effect : {GameManager.Instance.player.GetInstanceID()}");
        ef.Init(GameManager.Instance.player.transform,new Vector3(0,-1,0));
    }

    public void Invoke()
    {
        //if(_hitBeat != null)
        //    _hitBeat.Invoke();
    }

    public void InstanciateRecordNode()
    {
        if (_recordBeatLine == null)
            return;
        HitUI img = Instantiate(_beatImg, BeatPos);
        img.RectPS.localPosition = _recordBeatLine.RectPS.localPosition;
        _record.Add(img);
        img.Init(_recordBeatLine.Position, false);
        //Debug.Log(_recordBeatLine.Position);
        img.ImageUI.color = new Color(0, 1, 0, 0.3f);
    }

    public void RemoveNode()
    {
        if (_recordBeatLine == null)
            return;
        HitUI img = Instantiate(_beatImg, BeatPos);
        _record.Add(img);
        img.RectPS.localPosition = _recordBeatLine.RectPS.localPosition;
        img.Init(_recordBeatLine._position, true);
        img.ImageUI.color = new Color(0, 0, 1, 0.3f);
    }

    public void HitNode(AudioClip _audio, SoundSetting soundSet)
    {
        if (_hitBeatLine == null)
            return;

        if (_nowNode == null)
        {
            Debug.Log("노드 없음");
            return;
        }

        if (_nowNode.isHit == true)
            return;

        float a = _hitBeatLine.Position;
        float b = _nowNode.Position;
        
        

        
        
        // 비팅 시스템 만들기
        if (Mathf.Abs(a - b) < 0.03f)
        {
            _perfectCount++;
            Debug.Log($"쵝오, {a} | {b} | {Mathf.Abs(a - b)}");

            if (GameManager.Instance.player)
            {
                GameManager.Instance.player.Inven.NodeInvoking();

                if (GameManager.Instance.player.Inven.FindCurse(CurseList.PerfectSeal))
                {
                    ShootEffect(_hitEffect);
                    GameManager.Instance.detecter.ShowDetectionUI(_nowNode.transform.position, Detection.Good);
                    GameManager.Instance.player.PlayerAttack.DoAttack(Detection.Good);
                }
                else
                {
                    ShootEffect(_hitEffect);
                    GameManager.Instance.detecter.ShowDetectionUI(_nowNode.transform.position, Detection.Perfect);
                    GameManager.Instance.player.PlayerAttack.DoAttack(Detection.Perfect);
                }
            }

            switch (soundSet)
            {
                case SoundSetting.KickDrumSFX:
                    SoundManager.Instance.PlaySFX(_audio, SoundSetting.KickDrumSFX);
                    break;
                case SoundSetting.SnareDrumSFX:
                    SoundManager.Instance.PlaySFX(_audio,SoundSetting.SnareDrumSFX);
                    break;
                default:
                    break;
            }

            //SoundManager.Instance.PlaySFX(_audio);
            
            hitCount++;
        }
        else if (Mathf.Abs(a - b) < 0.04f)
        {
            
            
            _goodCount++;
            
            if (GameManager.Instance.player)
            {
                ShootEffect(_hitEffect);
                GameManager.Instance.player.Inven.NodeInvoking();
                GameManager.Instance.player.PlayerAttack.DoAttack(Detection.Good);
                GameManager.Instance.detecter.ShowDetectionUI(_nowNode.transform.position, Detection.Good);
                switch (soundSet)
                {
                    case SoundSetting.KickDrumSFX:
                        SoundManager.Instance.PlaySFX(_audio, SoundSetting.KickDrumSFX);
                        break;
                    case SoundSetting.SnareDrumSFX:
                        SoundManager.Instance.PlaySFX(_audio, SoundSetting.SnareDrumSFX);
                        break;
                    default:
                        break;
                }
            }
            Debug.Log($"굳, {a} | {b} | {Mathf.Abs(a - b)}");

            
            hitCount++;
        }
        else if (Mathf.Abs(a-b) < 0.05f)
        {

            
            
            Debug.Log($"밷, {a} | {b} | {Mathf.Abs(a - b)}");
            if (GameManager.Instance.player)
            {
                ShootEffect(_hitEffect);
                GameManager.Instance.player.Inven.NodeInvoking();
                GameManager.Instance.player.PlayerAttack.DoAttack(Detection.Bad);
                GameManager.Instance.detecter.ShowDetectionUI(_nowNode.transform.position, Detection.Bad);
            }
            hitCount++;
            
            //SoundManager.Instance.PlaySFX(_audio);
            switch (soundSet)
            {
                case SoundSetting.KickDrumSFX:
                    SoundManager.Instance.PlaySFX(_audio, SoundSetting.KickDrumSFX);
                    break;
                case SoundSetting.SnareDrumSFX:
                    SoundManager.Instance.PlaySFX(_audio, SoundSetting.SnareDrumSFX);
                    break;
                default:
                    break;
            }
        }   
        else
        {
            Debug.Log("노드 멀음");
            return;
        }
        
        _nowNode.isHit = true;
        _nowNode.ImageUI.color = new Color(1, 0, 0, 0.3f);
        _nowNode = null;
    }
    
    public void Update()
    {
        if (_hitBeatLine == null)
            return;
        
        int idx = 0;
        
        for (int i= 0; i < _hit.Count;i++)
        {
            if (idx > i && _hit[i].isHit)
            {
                _hit[i].ImageUI.color = new Color(1, 0, 0, 0.3f);
            }

            if (_nowNode == null)
                _nowNode = _hit[i];
            
            if (_nowNode != _hit[i] && Mathf.Abs(_hit[i].Position - _hitBeatLine.Position) < Mathf.Abs(_nowNode.Position - _hitBeatLine.Position))
            {
                _nowNode = _hit[i];
                idx = i;
            }
        }
    }

    public void InvaligateNode() // 이게 소환 하는거
    {
        List<HitUI> _remove = new();

        foreach (HitUI _re in _record)
        {
            if (_re.isRemove)
            {

                
                if (_hit.Count > 4)
                {
                    HitUI _node = _hit[0];
                    
                    bool _isDelete = false;
                    
                    for (int j = 0; j < _hit.Count; j++)
                    {
                        float a = Mathf.Abs(_hit[j].Position - _re.Position);
                        float b = Mathf.Abs(_node.Position - _re.Position);
                        if (a < b || _hit[j] == _node)
                        {
                            if (a < 0.06f)
                            {
                                _isDelete = true;
                                _node = _hit[j];
                            }
                        }
                    }
    
                    if (_isDelete)
                    {
                        _hit.Remove(_node);
                        
                        DestroyImmediate(_node.gameObject);
                    }
                    else
                    {
                        Debug.Log("인접노드 없음");
    
                    }
                }
               
                Debug.LogWarning(_re);
                _remove.Add(_re);
            }
        }

        for (int i = 0; i < _remove.Count; i++)
        {
            if (_record.Contains(_remove[i]))
            {
                _record.Remove(_remove[i]);
                DestroyImmediate(_remove[i].gameObject);
                Debug.LogWarning("destory");
            }
        }
        
        if (_record.Count > 0)
        {
            foreach (var hiting in _record)
            {
                _hit.Add(hiting);
            }
            _record.Clear();
        }
        
        _hit.Sort((a, b) =>
        {
            if (a.Position > b.Position)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        });
        foreach (var VARIABLE in _hit)
        {
            VARIABLE.RectPS.localPosition =
                Vector3.Lerp(_startHitBeat.localPosition, _endHitBeat.localPosition, VARIABLE.Position);
            VARIABLE.ImageUI.color = Color.red;
            VARIABLE.isHit = false;
        }
    }

    // 안씀
    public void ResetHitBoard()
    {
        foreach (var VARIABLE in _hit)
        {
            DestroyImmediate(VARIABLE.gameObject);
        }
        foreach (var VARIABLE in _record)
        {
            DestroyImmediate(VARIABLE.gameObject);
        }
        //_hit.Clear();
    }


    public bool IsRuleCollect()
    {
        return hitCount == _hit.Count;
    }

}

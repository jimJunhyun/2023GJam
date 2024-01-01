using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BeatUISystem : Singleton<BeatUISystem>
{
    
    public BeatUI _beatLineImg;
    public RectTransform _beatImg;

    private List<RectTransform> _hit = new();

    [Header("UI")] public Transform BeatPos;
    public RectTransform _startBeat;
    public RectTransform _endBeat;

    private BeatUI _beat;

    public void InstanciateNode()
    {
        _beat = Instantiate(_beatLineImg,BeatPos);
        
        _beat.Init(_startBeat, _endBeat);
    }

    public void Invoke()
    {
        if(_beat != null)
        _beat.Invoke();
    }

    public void InstanciateHitNode()
    {
        if (_beat == null)
            return;
        RectTransform img = Instantiate(_beatImg, BeatPos);
        img.localPosition = _beat.GetComponent<RectTransform>().localPosition;
        _hit.Add(img);
    }

    public void ResetHitBoard()
    {
        foreach (var VARIABLE in _hit)
        {
            DestroyImmediate(VARIABLE.gameObject);
        }
        _hit.Clear();
    }

}

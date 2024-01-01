using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatUI : MonoBehaviour
{
    private RectTransform _startPos;
    private RectTransform _endPos;

    private RectTransform _rect;

    private float _bps;
    private float _currentTime;
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public void Init(RectTransform start, RectTransform end)
    {
        _startPos = start;
        _endPos = end;
        _currentTime = 0;
        _bps = BeatSystem.Instance.BeatSecond();
        _rect.localPosition = _startPos.localPosition;

    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
//        Debug.Log(_currentTime/_bps);
        
        _rect.localPosition = Vector3.Lerp(_startPos.localPosition, _endPos.localPosition, _currentTime/_bps);

        if (_currentTime / _bps > 1.0f)
        {
            DestroyImmediate(gameObject);
        }
    }

    public void Invoke()
    {
        Debug.Log(_currentTime/_bps);
    }
}

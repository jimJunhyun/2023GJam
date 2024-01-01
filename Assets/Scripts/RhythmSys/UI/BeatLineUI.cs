using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLineUI : MonoBehaviour
{
    private RectTransform _startPos;
    private RectTransform _endPos;

    private float _bps;
    private float _currentTime;
    private RectTransform _rectTransform;
    public RectTransform RectPS
    {
        get
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }

            return _rectTransform;
        }
    }

    public float _position = 0;
    public float Position
    {
        get
        {
            return _position;
        }
    }

    public void Init(RectTransform start, RectTransform end)
    {
        _startPos = start;
        _endPos = end;
        _currentTime = 0;
        _bps = BeatSystem.Instance.BeatSecond();
        RectPS.localPosition = _startPos.localPosition;

    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
//        Debug.Log(_currentTime/_bps);
        
        RectPS.localPosition = Vector3.Lerp(_startPos.localPosition, _endPos.localPosition, _currentTime/_bps);
        _position = _currentTime / _bps;
//        Debug.LogWarning(_position);
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

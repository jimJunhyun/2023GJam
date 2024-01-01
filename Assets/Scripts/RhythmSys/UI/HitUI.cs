using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitUI : MonoBehaviour
{
    [SerializeField] private bool _constNode;
    [SerializeField] private float _position;
    [SerializeField] private bool _isHit;
    [SerializeField] private bool _isRemove;

    public bool isHit
    {

        get
        {
            return _isHit;
        }
        set
        {
            _isHit = value;
        }
    }

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
    
    private Image _img;
    public Image ImageUI
    {
        get
        {
            if (_img == null)
            {
                _img = GetComponent<Image>();
            }

            return _img;
        }
    }

    public float Position
    {
        get
        {
            return _position;
        }
    }

    public bool isRemove
    {
        get
        {
            return _isRemove;
        }
    }
    
    public void Init(float ps, bool removeNode = false, bool constNode =false)
    {
        _position = ps;
        _isRemove = removeNode;
        _constNode = constNode;
    }
}

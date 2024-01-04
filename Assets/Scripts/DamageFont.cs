using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageFont : PoolAble
{
    [SerializeField] private TextMeshPro _text;
    public override void Reset()
    {
        _text.text = "";
        _text.color = Color.white;
    }

    public void Init(Transform tls, int value, Color color)
    {
        transform.position = tls.position + new Vector3(0, 0.7f, 0);

        transform.DOMoveY(0.5f, 0.5f);
        _text.DOColor(new Color(1, 1, 1, 0), 0.4f);
        
        Debug.LogWarning("데미지 출력중");
        _text.text = $"{value}";
        _text.color = color;
    }
}

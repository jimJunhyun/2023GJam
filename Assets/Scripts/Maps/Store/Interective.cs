using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Interective : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [TextArea][SerializeField] public string _beforeText;
    [TextArea][SerializeField] public string _afterText;
    public UnityEvent _action;

    private void Awake()
    {
        _text.text = _beforeText;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.Instance.player.gameObject)
        {
            _text.text = _afterText;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameManager.Instance.player.gameObject)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _action?.Invoke();
                _text.text = _beforeText;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.Instance.player.gameObject)
        {
            _text.text = _beforeText;
        }
    }
}

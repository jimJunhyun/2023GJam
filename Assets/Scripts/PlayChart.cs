using System;
using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Chart
{
    public string Names;
    public int BPM;
    public string SongParent;

    public AudioClip _clips;
    public Sprite _spi;

    public Color color;
}

public class PlayChart : MonoBehaviour
{
    public TransitionSettings GO;
    public TransitionSettings Return;
    
    private int idx = 0;
    public List<Chart> _ch = new();

    [Header("Comp")] public Image _img;
    public Image BG;
    public Image downBG;
    public TextMeshProUGUI _songNames;
    public TextMeshProUGUI _bpm;

    private bool _isSelected = false;

    private void Start()
    {
        invaligateChart(_ch[0]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && _isSelected ==false)
        {
            _isSelected = true;
            GameObject obj = new GameObject();
            obj.AddComponent<SoundReturn>().Init(_ch[idx].BPM, _ch[idx]._clips);
            
            TransitionManager.Instance.Transition("Game",GO,0);
        }

        if (Input.GetKeyDown(KeyCode.Escape)&& _isSelected ==false)
        {
            _isSelected = true;
            TransitionManager.Instance.Transition("StartScene",Return,0);
        }
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (idx > 0)
            {
                idx--;
            }

            invaligateChart(_ch[idx]);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (idx < _ch.Count-1)
            {
                idx++;
            }
            
            invaligateChart(_ch[idx]);
        }

    }

    void invaligateChart(Chart t)
    {
        _img.sprite = t._spi;
        downBG.color = t.color;
        BG.color = t.color;
        _songNames.text = t.Names;
        _bpm.text = $"{t.BPM} BPM";
        
        BeatSystem.Instance.ReplayBPM(t.BPM);
        
        SoundManager.Instance.PlayBGM(t._clips);  
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EasyTransition;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[System.Serializable]
public class TutorialCls
{
    [SerializeField] [TextArea] public string text;
    [SerializeField] public bool _event = false;
    [SerializeField] public float _seeingTime = 1f;
    
    public Func<bool> _act = null;
}


public class Tutorial : MonoBehaviour
{
    [SerializeField] private TransitionSettings _setted;
    [SerializeField] private Image _quertBar;
    
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _subText;

    [SerializeField] public List<TutorialCls> text = new();


    public List<GameObject> _spawnPosition = new();
    public List<GameObject> _enemyList = new();
    
    public EnemySpawner spawner;
    
    private bool _isfirst = true;
    private void Awake()
    {

        text[0]._act = FirstEnter;
        text[5]._act = SecondEnter;
        text[8]._act = ThirdEnter;
        text[11]._act = FourEnter;
        text[14]._act = FiveEnter;
        text[15]._act = SixEnter;
        text[16]._act = FirstEnter;
        BeatSystem.Instance._jConnect = false;
        BeatSystem.Instance._kConnect = false;
        BeatSystem.Instance._iConnect = false;
        BeatSystem.Instance._oConnet = false;
    }

    private void Start()
    {
        
        StartCoroutine(tutorialIE(0));
    }

    public bool FirstEnter()
    {
        _quertBar.color = Color.red;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _quertBar.color= Color.green;
            return true;
        }

        _subText.text = "Enter 누르기";
        return false;
    }
    
    public bool SecondEnter()
    {
        if (_isfirst)
        {
            _quertBar.color = Color.red;
            _isfirst = false;
            BeatSystem.Instance._jConnect = true;
            BeatSystem.Instance._kConnect = true;
            BeatSystem.Instance._iConnect = true;
        }
        
        
        _subText.text = $"I를 눌러 비트 추가하기( {BeatUISystem.Instance.RecordCount}/4 )";
        if (BeatUISystem.Instance.HitCount >= 4)
        {
            _quertBar.color= Color.green;
            BeatSystem.Instance.StopBeat();
            BeatSystem.Instance._iConnect =false;
            _subText.text = $"I를 눌러 비트 추가하기( 완료 )";
            if (BeatSystem.Instance._reslStop == true)
            {
                _isfirst = true;
                return true;
            }
        }
        return false;
    }

    public bool ThirdEnter()
    {
        if (_isfirst)
        {
            _quertBar.color = Color.red;
            _isfirst = false;
            BeatSystem.Instance._jConnect = true;
            BeatSystem.Instance._kConnect = true;
        }
        _subText.text = $"J or K를 눌러 모든 비트를 연주하기";
        
        if (BeatUISystem.Instance.IsRuleCollect())
        {
            _quertBar.color= Color.green;
            _isfirst = true;
            return true;
        }
        

        return false;
    }

    public bool FourEnter()
    {
        if (_isfirst)
        {
            _quertBar.color = Color.red;
            _isfirst = false;
            BeatSystem.Instance._jConnect = true;
            BeatSystem.Instance._kConnect = true;
            BeatSystem.Instance._iConnect = true;
        }
        _subText.text = $"I를 눌러 비트 추가하기( {BeatUISystem.Instance.HitCount}/7 )";
        
        if (BeatUISystem.Instance.HitCount >= 7)
        {
            _quertBar.color= Color.green;
            _subText.text = $"I를 눌러 비트 추가하기( 완료 )";
            BeatSystem.Instance.StopBeat();
            BeatSystem.Instance._iConnect = false;
            if (BeatSystem.Instance._reslStop == true)
            {
                _isfirst = true;
                return true;
            }
        }

        return false;

    }

    public bool FiveEnter()
    {
        if (_isfirst)
        {
            _quertBar.color = Color.red;
            _isfirst = false;
            BeatSystem.Instance._jConnect = true;
            BeatSystem.Instance._kConnect = true;
            BeatSystem.Instance._iConnect = false;
            BeatSystem.Instance._oConnet = true;
        }        
        _subText.text = $"O를 눌러 비트 제거하기( {BeatUISystem.Instance.HitCount -4}개 지우기 )";

        if (BeatUISystem.Instance.HitCount <= 4)
        {
            _quertBar.color= Color.green;
            _subText.text = $"O를 눌러 비트 제거하기( 완료 )";

            _isfirst = true;
            return true;
            
        }

        return false;
    }

    public bool SixEnter()
    {
        int value = 0;
        if (_isfirst)
        {
            _quertBar.color = Color.red;
            _isfirst = false;
            BeatSystem.Instance._jConnect = true;
            BeatSystem.Instance._kConnect = true;
            BeatSystem.Instance._iConnect = true;
            BeatSystem.Instance._oConnet = true;

            List<int> ls = new List<int>();
            ls.Add(0);
            ls.Add(0);
            ls.Add(0);
            ls.Add(0);
            for (int i = 0; i < _spawnPosition.Count; i++)
            {
                _enemyList.Add(spawner.SpawnRand(_spawnPosition[i].transform,ref ls));
                _enemyList[i].GetComponent<AI>().StartAI();
            }
            
        }

        for (int i = 0; i < _enemyList.Count; i++)
        {
            if (_enemyList[i].gameObject.activeSelf == false)
            {
                value++;
            }
        }
        
        _subText.text = $"모든 적 정화하기( {value}/{_enemyList.Count} )";

        if (value == _enemyList.Count)
        {
            _quertBar.color= Color.green;
            return true;
        }

        return false;
    }
    
    
    
    IEnumerator tutorialIE(int i)
    {
        if (i == 0)
        {
            yield return new WaitUntil(() => GameManager.Instance.curRoom != null);
            GameManager.Instance.curRoom.ResetClearState();
        }
        
        BeatSystem.Instance.StartBeat();

        if (i >= text.Count)
        {
            TransitionManager.Instance.Transition("StartScene", _setted, 3f);
            StopAllCoroutines();
            yield break;
        }
        
        _text.text = text[i].text;
        _image.DOColor(new Color(1, 1, 1, 1), 0.3f);
        _text.DOColor(new Color(1, 1, 1, 1), 0.3f);

        
        yield return new WaitForSeconds(text[i]._seeingTime);
        
        
        if (text[i]._event)
        {
            while (true)
            {
                bool t = text[i]._act.Invoke();
                if (t)
                {

                    StartCoroutine(tutorialIE(i + 1));

                    break;
                }
                
                yield return null;
            }
        }
        else
        {
            _image.DOColor(new Color(1, 1, 1, 0), 0.3f);
            _text.DOColor(new Color(1, 1, 1, 0), 0.3f).OnComplete(() =>
            {                    
                if (i < text.Count - 1)
                    StartCoroutine(tutorialIE((i + 1)));
            });
        }
        

    }
    


}

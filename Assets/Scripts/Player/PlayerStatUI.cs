using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private Sprite _noHP;
    [SerializeField] private Sprite _halfHP;
    [SerializeField] private Sprite _maxHP;
    [SerializeField]private List<Image> _hpList = new();

    [Header("ATK")] [SerializeField] private TextMeshProUGUI _atk;
    [Header("SPEED")] [SerializeField] private TextMeshProUGUI _spd;
    [Header("Range")] [SerializeField] private TextMeshProUGUI _range;
    [Header("BPM")] [SerializeField] private TextMeshProUGUI _bpm;
    
    public void InvaligateHP(int nowHP, int MaxHP)
    {
        int hpCount = Mathf.FloorToInt((float)nowHP/2); // 4
        int MaxHPCount= Mathf.FloorToInt((float)MaxHP/2); // 10
        int i = 0;

        for (int j = 0; j < _hpList.Count; j++)
        {
            _hpList[j].color = new Color(1, 1, 1, 1);
            _hpList[j].sprite = _noHP; // 싹다 NoHP로 밀기
        }
        

        for (i = 0; i < hpCount; i++) // 4~5 라면 2캄나 체워주기
        {
            _hpList[i].color = new Color(1, 1, 1, 1);
            _hpList[i].sprite = _maxHP;
        }
        if (nowHP % 2 == 1) // 5일시 반칸 넣기
        {
            _hpList[i].color = new Color(1, 1, 1, 1);
            _hpList[i].sprite = _halfHP;
            i++;
        }
        for (int j  = MaxHPCount + MaxHP%2; j < _hpList.Count; j++) // 최대체력 이후부터 다 삭제
        {
            
            _hpList[j].color = new Color(1, 1, 1, 0);
        }
    }

    
    public void InvaligateAttack(int value)
    {
        _atk.text = $"{value}";
    }
    
    public void InvaligateSpeed(float value)
    {
        _spd.text = $"{value}";
    }
    public void InvaligateRange(float value)
    {
        _range.text = $"{value}";
    }
    
    public void InvaligateBPM(int value)
    {
        _bpm.text = $"{value} BPM";
    }
    
}

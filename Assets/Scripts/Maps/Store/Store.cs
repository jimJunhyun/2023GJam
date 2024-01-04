using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] ItemSOList _so;

    [Header("Camera")] public CinemachineVirtualCamera _cams;

    [Header("ItemSlot")] 
    [SerializeField ] private ItemSO _itemOne;
    [SerializeField] SpriteRenderer One;
    private ItemSO _itemTwo;
    [SerializeField] SpriteRenderer Two;
    private ItemSO _itemThree;
    [SerializeField] SpriteRenderer Three;
    private ItemSO _itemFour;
    [SerializeField] SpriteRenderer Four;

    [Header("Info")] public int _itemIdx = -1;

    [Header("SelectObject")] public GameObject _select;
    public TextMeshPro _tmp;
    [SerializeField] private bool _isLoad = false;

    public bool _isReal = false;

    [Header("UI")] public Canvas _cans;
    [SerializeField] private Image _spi;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemExpensive;
    [SerializeField] private TextMeshProUGUI _ATKStat;
    [SerializeField] private TextMeshProUGUI _rangeStat;
    [SerializeField] private TextMeshProUGUI _speedStat;
    [SerializeField] private TextMeshProUGUI _hpStat;
    [SerializeField] private TextMeshProUGUI _Explain;


    [Header("Effect")] [SerializeField] private EffectObject _bombEffect;

    [Header("Effect")] [SerializeField] private Interective _inter;
    private void Awake()
    {
        _itemIdx = -1;
        _select.SetActive(false);
        _cans.gameObject.SetActive(false);
        //StoreInit();
    }

    bool first =false;
    
    public void StoreInit()
    {
        if (first ==false)
        {
            first = true;
            // 뭐 점원이 안ㅋ녕ㅋ하ㅋ세ㅋ요 라도 하는거?
            
            // 상점 셋팅
            ItemSetting();
        }
    }

    public void ItemSetting()
    {
        List<int> idx = new();

        for (int i = 0; i < 4; i++)
        {
            int t = UnityEngine.Random.Range(0, _so._list.Count);

            if (idx.Contains(t))
            {
                i--;
            }
            else
            {
                idx.Add(t);
            }
        }

        _itemOne = _so._list[idx[0]];
        _itemTwo = _so._list[idx[1]];
        _itemThree = _so._list[idx[2]];
        _itemFour = _so._list[idx[3]];
        
        // 스프라이트 까지 설정
        One.sprite = _itemOne._sprite;
        Two.sprite = _itemTwo._sprite;
        Three.sprite = _itemThree._sprite;
        Four.sprite = _itemFour._sprite;

        _isLoad = true;

    }

    public void Interect()
    {
        StoreInit();
        GameManager.Instance.player.IsInteractive = true;
        _itemIdx = 0;
        _cans.gameObject.SetActive(true);
        ShopInvaligate();
    }

    public void BoombShop(SpriteRenderer spi)
    {
        One.color = Color.clear;
        Two.color = Color.clear;
        Three.color = Color.clear;
        Four.color = Color.clear;
        
        spi.color = Color.white;

        ShootEffect(_bombEffect, One.gameObject);
        ShootEffect(_bombEffect, Two.gameObject);
        ShootEffect(_bombEffect, Three.gameObject);
        ShootEffect(_bombEffect, Four.gameObject);
        

        _inter._action = null;

        _inter._afterText = "더이상\n팔지\n않습니다";
    }
    
    public void ShootEffect(PoolAble _mono, GameObject obj)
    {
        EffectObject ef = PoolManager.Instance.Pop(_mono.name) as EffectObject;
        Debug.LogWarning($"Effect : {GameManager.Instance.player.GetInstanceID()}");
        ef.Init(obj.transform.position);
    }

    private void Update()
    {
        if (_itemIdx <= -1 || _isLoad==false)
        {
            _select.SetActive(false);
            _cans.gameObject.SetActive(false);
            _cams.Priority = 0;
            return;
        }

        _cams.Priority = 20;
        
        _select.SetActive(true);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _tmp.text = $"이것을\n구매\n하시겠습니까";
            
            _isReal = false;
            
            _itemIdx--;
            if (_itemIdx <= 0)
            {
                _itemIdx = 0;
            }
            ShopInvaligate();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _tmp.text = $"이것을\n구매\n하시겠습니까";
            
            _isReal = false;
            
            _itemIdx++;
            if (_itemIdx >= 3)
            {
                _itemIdx = 3;
            }
            ShopInvaligate();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_isReal)
            {

                switch (_itemIdx)
                {
                    case 0:
                    {
                        BoombShop(One);
                        GameManager.Instance.player.GetItem(_itemOne, _itemOne.ExpensiveMoney);
                    }
                        break;
                    case 1:
                    {
                        BoombShop(Two);
                        GameManager.Instance.player.GetItem(_itemTwo, _itemTwo.ExpensiveMoney);
                    }
                        break;
                    case 2:
                    {
                        BoombShop(Three);
                        GameManager.Instance.player.GetItem(_itemThree, _itemThree.ExpensiveMoney);
                    }
                        break;
                    case 3:
                    {
                        BoombShop(Four);
                        GameManager.Instance.player.GetItem(_itemFour, _itemFour.ExpensiveMoney);
                        break;
                    }

                }
                GameManager.Instance.player.IsInteractive = false;
                                
                _itemIdx = -1;

            }
            else
            {
                bool b = false;
                switch (_itemIdx)
                {
                    case 0:
                    {
                        if (_itemOne.ExpensiveMoney < GameManager.Instance.player.Gold)
                        {
                            b = true;
                        }
                    }
                        break;
                    case 1:
                    {
                        if (_itemTwo.ExpensiveMoney < GameManager.Instance.player.Gold)
                        {
                            b = true;
                        }
                    }
                        break;
                    case 2:
                    {
                        if (_itemThree.ExpensiveMoney < GameManager.Instance.player.Gold)
                        {
                            b = true;
                        }
                    }
                        break;
                    case 3:
                    {
                        if (_itemFour.ExpensiveMoney < GameManager.Instance.player.Gold)
                        {
                            b = true;
                        }
                    }
                        break;

                }

                if (b == true)
                {
                    
                    _tmp.text = $"정말로\n구매\n하시겠습니까";   
                    _isReal = true;
                }
                else
                {
                    _tmp.text = $"골드가\n부족\n합니다"; 
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.player.IsInteractive = false;
                                
            _itemIdx = -1;
        }

    }

    public void ShopInvaligate()
    {
        Vector3 vec;
        switch (_itemIdx)
        {
            case 0:
            {
                vec = One.transform.position + new Vector3(0,1.5f,0);
                SetItem(_itemOne);   
            }
                break;
            case 1:
            {
                vec = Two.transform.position+ new Vector3(0,1.5f,0);
                SetItem(_itemTwo); 
            }
                break;
            case 2:
            {
                vec = Three.transform.position + new Vector3(0, 1.5f, 0);
                SetItem(_itemThree);
            }
                break;
            case 3:
            {
                vec = Four.transform.position+ new Vector3(0,1.5f,0);
                SetItem(_itemFour);
            }
                break;
            default:
                vec = One.transform.position+ new Vector3(0,1.5f,0);
                break;
        }


        _select.transform.position = vec;
    }

    void SetItem(ItemSO _so)
    {
        _itemName.text = _so.ItemName;
        _spi.sprite = _so._sprite;
        _itemExpensive.text = $"{_so.ExpensiveMoney}";
        _ATKStat.text = $"{_so.AddStat.AddATK}";
        _rangeStat.text = $"{_so.AddStat.AddRange}";;
        _speedStat.text = $"{_so.AddStat.AddSpeed}";;
        _hpStat.text = $"{_so.AddStat.AddHP}";;
        _Explain.text = $"{_so.ItemDescription}";;
    }
}

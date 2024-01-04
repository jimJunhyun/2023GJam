using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/Item/6_SixOneOpenGold")]
public class Six_OneOpenGold : ItemSO
{
    public override void AttackInvoke(LifeObject _obj, Detection dec)
    {
        Stat _player = GameManager.Instance.player.PlayerStat;
        switch (dec)   
        {
            case Detection.Perfect:
            {
                _obj.Damage(_player.ATK *1.5f, dec);

            }
                break;
            case Detection.Good:
                _obj.Damage(_player.ATK, dec);
                break;
            case Detection.Bad:
                _obj.Damage(_player.ATK * 0.5f, dec);
                break;
                                                                    
        }
        
        GameManager.Instance.player.GetGold(1);

    }
}

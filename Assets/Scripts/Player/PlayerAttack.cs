using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{


	
	//public float attack = 1;
	//
	//public float attackRange = 5;

	internal readonly int AttackHash = Animator.StringToHash("Attack");

    public void AttackInvoke(LifeObject _obj, Detection dec)
    {
        Stat _player = GameManager.Instance.player.PlayerStat;
        switch (dec)
        {
            case Detection.Perfect:
                _obj.Damage(_player.ATK * 1.5f, dec);
                break;
            case Detection.Good:
                _obj.Damage(_player.ATK, dec);
                break;
            case Detection.Bad:
                _obj.Damage(_player.ATK * 0.5f, dec);
                break;

        }
        Debug.LogError($"{dec} !!!!!! 판정!!!");
    }

    public void DoAttack(Detection _dec)
	{
		GameManager.Instance.player.playerCtrl.anim.SetTrigger(AttackHash);
		Collider[] cols = Physics.OverlapSphere(GameManager.Instance.player.transform.position, GameManager.Instance.player.PlayerStat.AttackRange, 1 << 9 | 1 << 10);
		//Debug.LogError($"{cols.Length} 적 갯수 || {GameManager.Instance.player.PlayerStat.AttackRange} || {GameManager.Instance.player.transform.position}");
		for (int i = 0; i < cols.Length; i++)
		{
			LifeObject obj = cols[i].GetComponent<LifeObject>();
			if (obj)
			{
				if(GameManager.Instance.player.Inven.ReturnItemRule() != null)
				{

					GameManager.Instance.player.Inven.ReturnItemRule().AttackInvoke(obj, _dec);
				}
				else
				{
					AttackInvoke(obj, _dec);
				}
			}
		}
		if (cols.Length > 0)
		{
			switch (_dec)
			{
				case Detection.Perfect:
					CameraManager.Instance.ShakeCamFor(3, 2, 0.1f);
					//GameManager.Instance.SlowDownFor(0.05f, 0.1f);
					break;
				case Detection.Good:
					CameraManager.Instance.ShakeCamFor(2, 1, 0.05f);
					//GameManager.Instance.SlowDownFor(0.1f, 0.05f);
					break;
				case Detection.Bad:
					CameraManager.Instance.ShakeCamFor(1, 1, 0.03f);
					//GameManager.Instance.SlowDownFor(0.2f, 0.05f);
					break;
				default:
					break;
			}
			
		}

	}


    public void JustDamage(int dmg)
	{
		Collider[] cols = Physics.OverlapSphere(transform.position, GameManager.Instance.player.PlayerStat.AttackRange, (1 << 9) | (1 << 10));
		for (int i = 0; i < cols.Length; i++)
		{
			LifeObject obj = cols[i].GetComponent<LifeObject>();
			if (obj)
			{
				obj.Damage(dmg, Detection.none);
			}
		}
		
	}
}

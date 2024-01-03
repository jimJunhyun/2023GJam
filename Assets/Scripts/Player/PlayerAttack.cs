using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	public PoolAble _hitEffect;
	
	//public float attack = 1;
	//
	//public float attackRange = 5;

	internal readonly int AttackHash = Animator.StringToHash("Attack");

	public void DoAttack(Detection _dec)
	{
		GameManager.Instance.player.playerCtrl.anim.SetTrigger(AttackHash);
		Collider[] cols = Physics.OverlapSphere(transform.position, GameManager.Instance.player.PlayerStat.AttackRange, (1 << 9) | (1 << 10));
		for (int i = 0; i < cols.Length; i++)
		{
			LifeObject obj = cols[i].GetComponent<LifeObject>();
			if (obj)
			{
				GameManager.Instance.player.Inven.ReturnItemRule().AttackInvoke(obj, _dec);
				switch (_dec)
				{
					case Detection.Perfect:
						
						break;
					case Detection.Good:
						break;
					case Detection.Bad:
						break;
					case Detection.none:
						break;
				}
				
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

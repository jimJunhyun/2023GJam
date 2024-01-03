using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	//public float attack = 1;
	//
	//public float attackRange = 5;
	
	
	
	public void DoAttack(Detection _dec)
	{
		
		Collider[] cols = Physics.OverlapSphere(transform.position, GameManager.Instance.player.PlayerStat.AttackRange, (1 << 9) | (1 << 10));
		for (int i = 0; i < cols.Length; i++)
		{
			LifeObject obj = cols[i].GetComponent<LifeObject>();
			if (obj)
			{
				GameManager.Instance.player.Inven.ReturnItemRule().AttackInvoke(obj, _dec);
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

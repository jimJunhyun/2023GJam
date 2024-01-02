using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	public float attack = 1;

	public float attackRange = 5;

	public void DoAttack()
	{
		Collider[] cols = Physics.OverlapSphere(transform.position, attackRange, (1 << 9) | (1 << 10));
		for (int i = 0; i < cols.Length; i++)
		{
			LifeObject obj = cols[i].GetComponent<LifeObject>();
			if (obj)
			{
				obj.Damage(attack);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlot : MonoBehaviour
{
    AI myEnemy;
    LifeObject enemyLife;

	public void SetEnemy(GameObject enemy)
	{
		myEnemy = enemy.GetComponent<AI>();
		enemyLife = GetComponent<LifeObject>();

	}

	public void StartEnemy()
	{
		myEnemy.examining = true;
	}

	public void StopEnemy()
	{
		myEnemy.examining = false;
	}

	public void ResetEnemy()
	{
		myEnemy.transform.position=  transform.position;
		if (!enemyLife.dead)
		{
			enemyLife.hp = enemyLife.maxHp;
		}
	}
}

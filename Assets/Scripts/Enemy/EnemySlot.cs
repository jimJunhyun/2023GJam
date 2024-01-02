using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySlot : MonoBehaviour
{
    AI myEnemy;
    LifeObject enemyLife;

	public void SetEnemy(GameObject enemy)
	{
		myEnemy = enemy.GetComponent<AI>();
		enemyLife = enemy. GetComponent<LifeObject>();
		if(NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 20f, -1))
		{
			enemy.transform.position = hit.position;
		}
	}

	public void StartEnemy()
	{
		if (!enemyLife.dead)
		{

		myEnemy.StartAI();
		}
	}

	public void StopEnemy()
	{
		if (!enemyLife.dead)
		{
			myEnemy.StopAI();
			myEnemy.transform.position = transform.position;
		}
		
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

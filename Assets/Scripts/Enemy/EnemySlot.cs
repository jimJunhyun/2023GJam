using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySlot : MonoBehaviour
{
	public int spawnRange = 4;
	public List<AI> myEnemies = new List<AI>();
	public List<LifeObject> myEnemyLifes = new List<LifeObject>();

	public void SetEnemy(GameObject enemy)
	{
		if(NavMesh.SamplePosition(transform.position, out NavMeshHit hit, spawnRange, -1))
		{
			enemy.transform.position = hit.position;
			myEnemyLifes.Add(enemy.GetComponent<LifeObject>());
			myEnemies.Add(enemy.GetComponent<AI>());
		}
	}

	public void StartEnemy()
	{
		
		for (int i = 0; i < myEnemies.Count; i++)
		{
			if (!myEnemyLifes[i].dead)
			{
				myEnemies[i].StartAI();

			}
		}
	}

	public void StopEnemy()
	{
		
		for (int i = 0; i < myEnemies.Count; i++)
		{
			if (!myEnemyLifes[i].dead)
			{
				myEnemies[i].StopAI();
				myEnemies[i].transform.position = transform.position;
			}
			
		}
		
	}

	public void ResetEnemy()
	{
		for (int i = 0; i < myEnemies.Count; i++)
		{
			myEnemies[i].transform.position = transform.position;
			if (!myEnemyLifes[i].dead)
			{
				myEnemyLifes[i].ResetCompletely();
			}
		}
		
	}
}

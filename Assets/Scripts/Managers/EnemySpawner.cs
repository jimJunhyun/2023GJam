using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
	public EnemyList list;
	public Vector4 spawnMax;

	public int GetLength
	{
		get =>list.enemies.Count;
	}

	private void Awake()
	{
		 instance = this;
	}

	public void Spawn(int idx, Transform trm)
	{
		if(idx < list.enemies.Count)
		{
			Instantiate(list.enemies[idx], trm.position, trm.rotation, trm);
		}
	}

	public void Spawn(string name, Transform trm)
	{
		GameObject obj  = list.enemies.Find(item => item.name == name);
		if (obj)
		{
			Instantiate(obj, trm.position, trm.rotation, trm);
		}
	}

	public GameObject SpawnRand(Transform trm, ref List<int> spawnInfo)
	{
		int idx = -1;
		bool invalid = true;
		int repCount = 0;
		while (invalid)
		{ 
			++repCount;

			idx = Random.Range(0, GetLength);
			for (int i = 0; i < spawnInfo.Count; i++)
			{
				if (spawnInfo[i] < 1)
				{
					if(i == 2)
					{
						if(Random.Range(0, 2) > 0)
						{
							i = 4;
						}
					}
					idx = i;
					invalid = false;
				}
			}
			switch (idx)
			{
				case 0:
					if(spawnInfo[idx] < spawnMax.x)
					{
						invalid = false;
					}
					break;
				case 1:
					if (spawnInfo[idx] < spawnMax.y)
					{
						invalid = false;
					}
					break;
				case 2:
				case 4:
					if (spawnInfo[2] < spawnMax.z)
					{
						invalid = false;
					}
					break;
				case 3:
					if (spawnInfo[idx] < spawnMax.w)
					{
						invalid = false;
					}
					break;
				default:
					break;
			}
			if(repCount > GameManager.MAXREPCOUNT)
				break;
		}
		spawnInfo[idx] += 1;
		GameObject obj = Instantiate(list.enemies[idx], trm.position, list.enemies[idx].transform.rotation, trm);
		obj.transform.localScale = new Vector3(obj.transform.localScale.x / obj.transform.lossyScale.x, obj.transform.localScale.y / obj.transform.lossyScale.y, obj.transform.localScale.z / obj.transform.lossyScale.z);
		return obj;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
	public EnemyList list;

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

	public GameObject SpawnRand(Transform trm)
	{
		int idx = Random.Range(0, GetLength);
		GameObject obj = Instantiate(list.enemies[idx], trm.position, list.enemies[idx].transform.rotation, trm);
		obj.transform.localScale = new Vector3(obj.transform.localScale.x / obj.transform.lossyScale.x, obj.transform.localScale.y / obj.transform.lossyScale.y, obj.transform.localScale.z / obj.transform.lossyScale.z);
		return obj;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum Direction
{
	Up, 
	Down,
	Left,
	Right,
}

public enum RoomType
{
	Start,
	Normal,
	Shop,
	Heal,
	Curse,
	Boss,

	Question,
}

[CreateAssetMenu()]
public class MapAtom : ScriptableObject
{
    public MapObjs obj;

	public bool isQuestion = false;
	public RoomType type;

    public MapAtom up;
    public MapAtom down;
    public MapAtom left;
    public MapAtom right;

	List<ConnectStat> conStat = null;

	public Vector3 upPt;
	public Vector3 downPt;
	public Vector3 leftPt;
	public Vector3 rightPt;


	public Vector3 rootOffSet;

	public bool isRandomizable;

	public bool cleared;

	GameObject self;
	List<EnemySlot> slots;

	bool inited = false;

	public void Init(Vector3 pos)
	{
		inited = true;
		SetStructureRandom();
		InstantiateSelf(pos);
		SetEnemyRandom();
		SetPoints();
	}

	public void SetStructureRandom()
	{
		if (isRandomizable)
		{
			conStat = new List<ConnectStat>();
			if(up != null)
			{
				conStat.Add(ConnectStat.Up);
			}
			if (down != null)
			{
				conStat.Add(ConnectStat.Down);
			}
			if (left != null)
			{
				conStat.Add(ConnectStat.Left);
			}
			if (right != null)
			{
				conStat.Add(ConnectStat.Right);
			}
			
			List<MapObjs> objs = GameManager.instance.mapList.GetMapOfCondition(conStat);
			obj = objs[Random.Range(0, objs.Count)];
		}
	}
	
	public void InstantiateSelf(Vector3 pos, Transform parent = null)
	{
		self = GameObject.Instantiate(obj.gameObject, pos, obj.gameObject.transform.rotation);
		self.transform.SetParent(parent);
	}


    public void SetEnemyRandom()
	{
		slots = new List<EnemySlot>();
		for (int i = 0; i < self.transform.childCount; i++)
		{
			Transform trm = self.transform.GetChild(i);
			if (trm.name.Contains("MobPoint"))
			{
				EnemySlot slot = trm.GetComponent<EnemySlot>();
				if (slot)
				{
					slot.SetEnemy(EnemySpawner.instance.SpawnRand(trm));
					slots.Add(slot);
				}
			}
		}
	}

	public void TriggerEnemy()
	{
		for (int i = 0; i < slots.Count; i++)
		{
			slots[i].StartEnemy();
		}
	}

	public void ResetEnemy()
	{
		for (int i = 0; i < slots.Count; i++)
		{
			slots[i].StopEnemy();
			slots[i].ResetEnemy();
		}
	}

	public void SetClearState()
	{
		cleared = true;
	}

	public void SetPoints()
	{
		upPt = self.transform.position + Vector3.forward * MapGenerator.MAPY * 0.4f;
		downPt = self.transform.position + Vector3.back * MapGenerator.MAPY * 0.4f;
		leftPt = self.transform.position + Vector3.left * MapGenerator.MAPX * 0.4f;
		rightPt = self.transform.position + Vector3.right * MapGenerator.MAPX * 0.4f;
	}

	public void MoveTo(Direction dir)
	{
		if (!cleared)
			ResetEnemy();
		switch (dir)
		{
			case Direction.Up:
				if(up == null)
					break;
				up.OnTransition();
				GameManager.instance.MovePlayerTo(up.downPt);
				break;
			case Direction.Down:
				if (down == null)
					break;
				down.OnTransition();
				GameManager.instance.MovePlayerTo(down.upPt);
				break;
			case Direction.Left:
				if (left == null)
					break;
				left.OnTransition();
				GameManager.instance.MovePlayerTo(left.rightPt);
				break;
			case Direction.Right:
				if (right == null)
					break;
				right.OnTransition();
				GameManager.instance.MovePlayerTo(right.leftPt);
				break;
		}
	}

	public void OnTransition()
	{
		GameManager.instance.curRoom = this;
		isQuestion = false;
		if (!cleared)
		{
			TriggerEnemy();
		}
		GameManager.instance.RefreshMap();
	}
}

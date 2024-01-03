using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;

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
	Blessing,

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

	public MoveStation upPtExit;
	public MoveStation downPtExit;
	public MoveStation leftPtExit;
	public MoveStation rightPtExit;


	public Vector3 rootOffSet;

	public bool isRandomizable;

	public bool cleared;

	GameObject self;
	List<EnemySlot> slots;

	[Header("MapGimik")] public MapGimikSO _mapGimik;

	private MapPP mPP;
	public virtual void Init(Vector3 pos, MapGenerator info)
	{
		SetStructureRandom(info);
		InstantiateSelf(pos);
		SetPoints();
		cleared = false;
		GameManager.Instance.StartCoroutine(DelSetEnemy());
	}

	IEnumerator DelSetEnemy()
	{
		NavMeshHit hit;
		int rep = 0;
		yield return null;
		while (true)
		{
			yield return null;
			NavMesh.SamplePosition(self.transform.position, out hit, 1000f, -1);
			++rep;
			if (hit.hit)
			{
				Debug.Log("GONE WELL WITH : " + self.name + " in " + name);
				SetEnemyRandom();
				yield break;
			}
			if(rep > GameManager.MAXREPCOUNT)
			{
				Debug.Log("SOMETHING WRONG WITH : " + self.name + " in " + name);
				yield break;
			}
		}
	}

	public virtual void SetStructureRandom(MapGenerator info)
	{
		if(type == RoomType.Boss)
		{
			obj = GameManager.Instance.mapList.bossMap;
		}
		else if (type == RoomType.Question)
		{
			bool invalid = true;
			
			int repCount = 0;
			while (invalid)
			{
				++repCount;

				RoomType t = (RoomType)Random.Range(((int)RoomType.Shop), ((int)RoomType.Blessing) + 1);
				if (info.shopCnt < info.shopMinMax.x)
				{
					t = RoomType.Shop;
					info.shopCnt += 1;
					invalid = false;
				}
				else if(info.healCnt < info.healMinMax.x)
				{
					t = RoomType.Heal;
					info.healCnt += 1;
					invalid = false;
				}
				else if (info.curseCnt < info.curseMinMax.x)
				{
					t = RoomType.Curse;
					info.curseCnt += 1;
					invalid = false;
				}
				else if (info.blessCnt < info.blessMinMax.x)
				{
					t = RoomType.Blessing;
					info.blessCnt += 1;
					invalid = false;
				}
				type = t;
				switch (t)
				{
					case RoomType.Shop:
						if (info.shopCnt + 1 < info.shopMinMax.y)
						{
							obj = GameManager.Instance.mapList.shopMap;
							info.shopCnt += 1;
							invalid = false;
						}
						break;
					case RoomType.Heal:
						if (info.healCnt + 1 < info.healMinMax.y)
						{
							obj = GameManager.Instance.mapList.healMap;
							info.healCnt += 1;
							invalid = false;
						}
						break;
					case RoomType.Curse:
						if (info.curseCnt + 1 < info.curseMinMax.y)
						{
							obj = GameManager.Instance.mapList.curseMap;
							info.curseCnt += 1;
							invalid = false;
						}
						break;
					case RoomType.Blessing:
						if (info.blessCnt + 1 < info.blessMinMax.y)
						{
							obj = GameManager.Instance.mapList.blessMap;
							info.blessCnt += 1;
							invalid = false;
						}
						break;
				}
				if(repCount > GameManager.MAXREPCOUNT)
				{
					type = RoomType.Normal;
					invalid = false;
				}
			}
		}
		else if(type == RoomType.Start)
		{
			obj = GameManager.Instance.mapList.startMap;
		}

		if(type == RoomType.Normal)
		{
			obj = GameManager.Instance.mapList.randomMaps[Random.Range(0, GameManager.Instance.mapList.randomMaps.Count)];
		}
		obj.type = type;

		switch (type)
		{
			case RoomType.Heal:
				_mapGimik = GameManager.Instance.mapList.healGimmick;
				break;
			case RoomType.Curse:
				_mapGimik = GameManager.Instance.mapList.curseGimmick;
				break;
			case RoomType.Blessing:
				_mapGimik = GameManager.Instance.mapList.blessGimmick;
				break;
			default:
				_mapGimik = null;
				break;
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
		int mobCnt = Random.Range(4, 21);
		int spPointAmt = Random.Range(4, 6);
		HashSet<int> selecteds = new HashSet<int>();
		List<int> spawnInfo = new List<int>(4) { 0, 0, 0, 0 };
		//Debug.Log("cnt : " + spPointAmt + " , Mobs : " + mobCnt);
		while(selecteds.Count < spPointAmt)
		{
			int idx = Random.Range(0, 5);
			selecteds.Add(idx);
			//Debug.Log("SLOTNO : " + idx);
		}
		foreach (int item in selecteds)
		{
			//Debug.Log($"FINDING :MobPoint_0{item + 1} under {self.transform.name}");
			Transform trm = self.transform.Find($"MobPoint_0{item+ 1}");
			if (trm)
			{
				//Debug.Log("FOUND?");
				EnemySlot slot = trm.GetComponent<EnemySlot>();
				if (slot)
				{
					//Debug.Log("FOUND!");
					slot.SetEnemy(EnemySpawner.instance.SpawnRand(trm, ref spawnInfo));
					slots.Add(slot);
				}
			}
			
		}
		
		while (GameManager.ArraySum(spawnInfo) < mobCnt)
		{
			int r = Random.Range(0, slots.Count);
			bool invalid = true;
			int repCount = 0;
			while (invalid)
			{
				++repCount;
				r = Random.Range(0, slots.Count);
				if (slots[r].myEnemies.Count < GameManager.MAXMOBPERPOINT)
				{
					slots[r].SetEnemy(EnemySpawner.instance.SpawnRand(slots[r].transform, ref spawnInfo));
					invalid = false;
				}
				if(repCount > GameManager.MAXREPCOUNT)
				{
					break;
				}
			}
		}
		//for (int i = 0; i < self.transform.childCount; i++)
		//{
		//	Transform trm = self.transform.GetChild(i);
		//	if (trm.name.Contains("MobPoint"))
		//	{
		//		EnemySlot slot = trm.GetComponent<EnemySlot>();
		//		if (slot)
		//		{
		//			slot.SetEnemy(EnemySpawner.instance.SpawnRand(trm));
		//			slots.Add(slot);
		//		}
		//	}
		//}
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
		mPP.AdjustSaturation(true);
		if (up)
		{
			upPtExit.SetValid();
		}
		if (down)
		{
			downPtExit.SetValid();
		}
		if (left)
		{
			leftPtExit.SetValid();
		}
		if (right)
		{
			rightPtExit.SetValid();
		}
	}

	public void ResetClearState()
	{
		cleared = false;
        mPP.AdjustSaturation(false);
        if (up && upPtExit)
		{
			upPtExit.ResetValid();
		}
		if (down && downPtExit)
		{
			downPtExit.ResetValid();
		}
		if (left && leftPtExit)
		{
			leftPtExit.ResetValid();
		}
		if (right && rightPtExit)
		{
			rightPtExit.ResetValid();
		}
	}

	public void SetPoints()
	{
		mPP = self.transform.Find("PP").GetComponent<MapPP>();
		mPP.AdjustSaturation(false);

		upPt = self.transform.Find("EnterPoint0").position;
		downPt = self.transform.Find("EnterPoint1").position;
		leftPt = self.transform.Find("EnterPoint2").position;
		rightPt = self.transform.Find("EnterPoint3").position;

		upPtExit = self.transform.Find("ExitPoint0").GetComponent<MoveStation>();	
		downPtExit = self.transform.Find("ExitPoint1").GetComponent<MoveStation>();	
		leftPtExit = self.transform.Find("ExitPoint2").GetComponent<MoveStation>();	
		rightPtExit = self.transform.Find("ExitPoint3").GetComponent<MoveStation>();
		
		upPtExit.onEnterPoint.AddListener(()=>MoveTo(Direction.Up));
		downPtExit.onEnterPoint.AddListener(()=>MoveTo(Direction.Down));
		leftPtExit.onEnterPoint.AddListener(()=>MoveTo(Direction.Left));
		rightPtExit.onEnterPoint.AddListener(()=>MoveTo(Direction.Right));
	}

	public virtual void MoveTo(Direction dir)
	{
		if (!cleared)
			ResetClearState();
		switch (dir)
		{
			case Direction.Up:
				if(up == null)
					break;
				up.OnTransition();
				GameManager.Instance.MovePlayerTo(up.downPt);
				break;
			case Direction.Down:
				if (down == null)
					break;
				down.OnTransition();
				GameManager.Instance.MovePlayerTo(down.upPt);
				break;
			case Direction.Left:
				if (left == null)
					break;
				left.OnTransition();
				GameManager.Instance.MovePlayerTo(left.rightPt);
				break;
			case Direction.Right:
				if (right == null)
					break;
				right.OnTransition();
				GameManager.Instance.MovePlayerTo(right.leftPt);
				break;
		}
	}

	public virtual void OnTransition()
	{
		GameManager.Instance.curRoom = this;
		isQuestion = false;
		if (!cleared)
		{
			
			TriggerEnemy();
			_mapGimik?.RoomInit();
		}
		GameManager.Instance.RefreshMap();
	}
}

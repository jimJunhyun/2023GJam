using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;
using UnityEngine.Playables;

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
	public bool questionRoomStatus;

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

	public MoveStation spRoomEnter;
	public Vector3 spEnterPt;
	public MoveStation spRoomExit;
	public Vector3 spExitPt;
	public GameObject spRoom;

	GameObject actualSpRoom;

	public MoveStation interpoint;

	public Vector3 rootOffSet;

	public bool isRandomizable;

	public bool cleared;
	public bool isAutoClear = true;

	GameObject self;
	List<EnemySlot> slots = new();

	[Header("MapGimik")] public MapGimikSO _mapGimik;

	public bool IsCleared
	{
		get
		{
			bool res = true;
			for (int i = 0; i < slots.Count; i++)
			{
				res &= slots[i].deadMobCnt == slots[i].myEnemies.Count;
			}
			return res && isAutoClear;
		}
	}

	private MapPP mPP;
	public virtual void Init(Vector3 pos, MapGenerator info)
	{
		if (questionRoomStatus)
		{
			isQuestion = true;
			type = RoomType.Question;
		}
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
				//Debug.Log("GONE WELL WITH : " + self.name + " in " + name);
				if (self.transform.Find("MobPoint_01"))
				{
					SetEnemyRandom();
				}
				yield break;
			}
			if(rep > GameManager.MAXREPCOUNT)
			{
				Debug.LogError("SOMETHING WRONG WITH : " + self.name + " in " + name);
				yield break;
			}
		}
	}

	public virtual void SetStructureRandom(MapGenerator info)
	{
		if(type == RoomType.Boss)
		{
			if (isRandomizable)
			{
				obj = GameManager.Instance.mapList.bossMap;
			}
		}
		else if (type == RoomType.Question)
		{
			isQuestion = true;
			bool invalid = true;
			
			int repCount = 0;
			while (invalid)
			{
				++repCount;

				RoomType t = (RoomType)Random.Range(((int)RoomType.Shop), ((int)RoomType.Blessing) + 1);
				type = t;
				spRoom = null;
				isAutoClear = true;
				switch (t)
				{
					case RoomType.Shop:
						if (info.shopCnt > 0)
						{
							obj = GameManager.Instance.mapList.shopMap;
							info.shopCnt -= 1;
							invalid = false;
							Debug.Log("spRoom set");
							spRoom = GameManager.Instance.mapList.shopRoom;
						}
						break;
					case RoomType.Heal:
						if (info.healCnt > 0)
						{
							obj = GameManager.Instance.mapList.healMap;
							info.healCnt -= 1;
							_mapGimik = GameManager.Instance.mapList.healGimmick;
							invalid = false;
						}
						break;
					case RoomType.Curse:
						if (info.curseCnt > 0)
						{
							obj = GameManager.Instance.mapList.curseMap;
							info.curseCnt -= 1;
							invalid = false;
							isAutoClear = false;
						}
						break;
					case RoomType.Blessing:
						if (info.blessCnt > 0)
						{
							obj = GameManager.Instance.mapList.blessMap;
							info.blessCnt -= 1;
							invalid = false;
							Debug.Log("spRoom set");
							spRoom = GameManager.Instance.mapList.blessRoom;
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
			if(isRandomizable)
				obj = GameManager.Instance.mapList.startMap;
		}

		if(type == RoomType.Normal)
		{
			obj = GameManager.Instance.mapList.randomMaps[Random.Range(0, GameManager.Instance.mapList.randomMaps.Count)];
		}
		obj.type = type;


	}
	
	public void InstantiateSelf(Vector3 pos, Transform parent = null)
	{
		self = GameObject.Instantiate(obj.gameObject, pos, obj.gameObject.transform.rotation);
		self.transform.SetParent(parent);
		if(type == RoomType.Curse)
		{
			interpoint = self.transform.Find("Interective").GetComponent<MoveStation>();
			Debug.Log("ARROWSHOWER");
		}
		if (spRoom)
		{
			Vector3 pt = self.transform.position;
			pt.y += 300;
			actualSpRoom = GameObject.Instantiate(spRoom, pt, spRoom.transform.rotation);
			spEnterPt = actualSpRoom.transform.Find("EnterPointSp").position;
			spExitPt = self.transform.Find("ExitPointSp").position;
			spRoomExit = actualSpRoom.transform.Find("ExitPointSp").GetComponent<MoveStation>();
			spRoomEnter = self.transform.Find("EnterPointSp").GetComponent<MoveStation>();
			if (type == RoomType.Blessing)
			{
				interpoint = actualSpRoom.transform.Find("Interective").GetComponent<MoveStation>();
			}
			spRoomEnter.onEnterPoint.AddListener(() => 
			{
				GameManager.Instance.MovePlayerTo(spEnterPt, true); 
				if(type == RoomType.Shop)
				{
					spRoom.GetComponent<Store>().StoreInit();
				}
				if (type == RoomType.Blessing)
				{
					interpoint.SetValid();
				}
			});
			spRoomExit.onEnterPoint.AddListener(() => 
			{
				GameManager.Instance.MovePlayerTo(spExitPt,true);
				if (type == RoomType.Blessing)
				{
					interpoint.ResetValid();
				}
			});

		}
	}


    public void SetEnemyRandom()
	{
		slots = new List<EnemySlot>();
		int mobCnt = Random.Range(4, 11);
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
				//Debug.Log(r);
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
		if (!cleared)
		{
			Debug.Log("CLEARED");
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
			if (spRoomEnter)
			{
				spRoomEnter.SetValid();
			}
			if (interpoint && type == RoomType.Curse)
			{
				interpoint.ResetValid();
			}
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

		if(type == RoomType.Boss && isRandomizable)
		{
			upPtExit.onEnterPoint.AddListener(()=>
			{ 
				GameManager.Instance.boss.ActivateWithDel(12);
				GameManager.Instance.boss.life.SetImmuneFor(12);
				GameManager.Instance.bossHp.ShowUI();
				GameObject.Find("Timeliner").GetComponent<PlayableDirector>().Play();
			});
		}
	}

	public virtual void MoveTo(Direction dir)
	{
		if (!cleared)
			ResetClearState();
		if (up)
		{
			upPtExit.ResetValid();
		}
		if (down)
		{
			downPtExit.ResetValid();
		}
		if (left)
		{
			leftPtExit.ResetValid();
		}
		if (right)
		{
			rightPtExit.ResetValid();
		}
		if (spRoomEnter)
		{
			spRoomEnter.ResetValid();
		}
		if (interpoint && type == RoomType.Curse)
		{
			interpoint.ResetValid();
		}
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
			if(type == RoomType.Heal)
			{
				_mapGimik.RoomInit();
			}
			TriggerEnemy();
			if (up)
			{
				upPtExit.ResetValid();
			}
			if (down)
			{
				downPtExit.ResetValid();
			}
			if (left)
			{
				leftPtExit.ResetValid();
			}
			if (right)
			{
				rightPtExit.ResetValid();
			}
			if (interpoint && type == RoomType.Curse)
			{
				interpoint.SetValid();
			}
		}
		else
		{
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
			if (spRoomEnter)
			{
				spRoomEnter.SetValid();
			}
			if (interpoint && type == RoomType.Curse)
			{
				interpoint.ResetValid();
			}
		}
		GameManager.Instance.RefreshMap();
	}
}

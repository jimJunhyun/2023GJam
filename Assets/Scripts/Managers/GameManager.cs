using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager>
{

	public const float GRAVITY = 9.8f;
	public const int MAXREPCOUNT = 3000;
	public const int MAXMOBPERPOINT = 5;
	private Player _player;
	public Player player
	{
		get
		{
			if (_player == null)
			{
				_player = FindObjectOfType<Player>();
// 				Debug.LogWarning(_player);
			}
			return _player;

		}
	}
	
	public BossAI boss;

	public MapAtom curRoom;
	public int curStage = 0;

	public List<MapGenerator> maps;

	public MapList mapList;

	public DecalBase decal;

	public GoldUI gold;

	NavMeshSurface surface;

	private void Awake()
	{
		surface = GetComponent<NavMeshSurface>();
		gold  = GameObject.Find("GoldArea").GetComponent<GoldUI>();
	}

	private void Start()
	{
		player.RefreshStat();
		for (int i = 0; i < maps.Count; i++)
		{
			maps[i].Create();
		}
		surface.BuildNavMesh();

		curRoom = maps[curStage].startRoom;
		MovePlayerTo(curRoom.downPt);
		maps[curStage].CreateMap();
	}

	private void Update()
	{
		if (curRoom)
		{
			if (curRoom.IsCleared)
			{
				curRoom.SetClearState();
			}

		}
	}

	public void RefreshMap()
	{
		maps[curStage].CreateMap();
	}


	public void MovePlayerTo(Vector3 point, bool yDiff = false)
	{
		//player.transform.position = point;
		player.playerCtrl.SetPosition(point, yDiff);
	}

	public void ChangeRoom(Direction dir)
	{
		curRoom.MoveTo(dir);
	}

	public void SlowDownFor(float to, float dur)
	{
		StartCoroutine(DelSlower(to, dur));
	}

	IEnumerator DelSlower(float to, float dur)
	{
		Time.timeScale = to;
		yield return new WaitForSecondsRealtime(dur);
		Time.timeScale = 1;
	}

	public static int ArraySum(List<int> lst)
	{
		int sum = 0;
		for (int i = 0; i < lst.Count; i++)
		{
			sum += lst[i];
		}
		return sum;
	}
}


[System.Serializable]
public class Stat
{
	public int MaxHP = 3;
	public int HP =3;
	public int ATK = 1;
	public float SPEED = 5f;
	public int Size =1;
	public float AttackRange = 5f;

	public void Reset()
	{
		MaxHP = 0;
		HP = 0;
		ATK = 0;
		SPEED = 0;
		Size = 0;
		AttackRange = 0;
	}
	
	public static Stat operator +(Stat a, Stat b)
	{
		Stat c = new Stat();
		c.MaxHP = a.MaxHP + b.MaxHP;
		c.HP = a.HP + b.HP;
		c.ATK = a.ATK + b.ATK;
		c.SPEED = a.SPEED + b.SPEED;
		c.Size = a.Size + b.Size;
		c.AttackRange = a.AttackRange + b.AttackRange;
		return c;
	}
}
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager>
{

	public const float GRAVITY = 9.8f;

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
	
	
	public MapAtom curRoom;
	public int curStage = 0;

	public List<MapGenerator> maps;

	public MapList mapList;

	NavMeshSurface surface;

	private void Awake()
	{
		surface = GetComponent<NavMeshSurface>();
	}

	private void Start()
	{
		player.RefreshStat();
		for (int i = 0; i < maps.Count; i++)
		{
			maps[i].Create();
		}
		curRoom = maps[curStage].startRoom;
		MovePlayerTo(curRoom.rootOffSet);
		surface.BuildNavMesh();
		maps[curStage].CreateMap();
	}

	public void RefreshMap()
	{
		maps[curStage].CreateMap();
	}


	public void MovePlayerTo(Vector3 point)
	{
		//player.transform.position = point;
		player.playerCtrl.SetPosition(point);
	}

	public void ChangeRoom(Direction dir)
	{
		curRoom.MoveTo(dir);
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
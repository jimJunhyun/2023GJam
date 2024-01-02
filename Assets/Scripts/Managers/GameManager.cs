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
				_player = GameObject.Find("Player").GetComponent<Player>();
 				Debug.LogWarning(_player);
			}
			return _player;

		}
	}
	
	
	public MapAtom curRoom;

	public List<MapGenerator> maps;

	public MapList mapList;

	NavMeshSurface surface;

	private void Awake()
	{
		surface = GetComponent<NavMeshSurface>();
		player.RefreshStat();
	}

	private void Start()
	{
		for (int i = 0; i < maps.Count; i++)
		{
			maps[i].Create();
		}
		curRoom = maps[0].startRoom;
		MovePlayerTo(curRoom.rootOffSet);
		surface.BuildNavMesh();
	}


	public void MovePlayerTo(Vector3 point)
	{
		player.transform.position = point;
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
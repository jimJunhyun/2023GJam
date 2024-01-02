using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public const float GRAVITY = 9.8f;

	private Player _player;
	public Player player
	{
		get
		{
			if (_player == null)
				_player = GameObject.Find("Player").GetComponent<Player>();
			return _player;

		}
	}
	
	
	public MapAtom curRoom;

	public List<MapGenerator> maps;

	NavMeshSurface surface;

	private void Awake()
	{
		instance = this;
		surface = GetComponent<NavMeshSurface>();
	}

	private void Start()
	{
		for (int i = 0; i < maps.Count; i++)
		{
			maps[i].Create();
		}
		curRoom = maps[0].startRoom;
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
	
	public static Stat operator +(Stat a, Stat b)
	{
		a += b;
		return a;
	}
}
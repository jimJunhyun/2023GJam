using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public const float GRAVITY = 9.8f;

	public GameObject player;

	public MapAtom curRoom;

	public List<MapGenerator> maps;

	private void Awake()
	{
		instance = this;

		player = GameObject.Find("Player");
	}

	private void Start()
	{
		for (int i = 0; i < maps.Count; i++)
		{
			maps[i].Create();
		}
		curRoom = maps[0].startRoom;
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

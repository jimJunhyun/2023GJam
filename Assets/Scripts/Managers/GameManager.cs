using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public const float GRAVITY = 9.8f;

	public GameObject player;

	public MapAtom curRoom;

	public List<MapGenerator> maps;

	NavMeshSurface surface;

	private void Awake()
	{
		instance = this;

		player = GameObject.Find("Player");
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

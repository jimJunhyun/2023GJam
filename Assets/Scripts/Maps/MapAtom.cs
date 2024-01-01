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

[CreateAssetMenu()]
public class MapAtom : ScriptableObject
{
    public GameObject obj;
    public MapAtom up;
    public MapAtom down;
    public MapAtom left;
    public MapAtom right;

	public Vector3 upPt;
	public Vector3 downPt;
	public Vector3 leftPt;
	public Vector3 rightPt;

	GameObject self;
	
	public void InstantiateSelf(Vector3 pos, Transform parent = null)
	{
		self = GameObject.Instantiate(obj, pos, obj.transform.rotation);
		self.transform.SetParent(parent);
	}


    public void SetEnemyRandom()
	{
		for (int i = 0; i < self.transform.childCount; i++)
		{
			Transform trm = self.transform.GetChild(i);
			if (trm.name.Contains("MobPoint"))
			{
				EnemySlot slot = trm.GetComponent<EnemySlot>();
				if (slot)
				{
					slot.SetEnemy(EnemySpawner.instance.SpawnRand(trm));

				}
			}
		}
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
	}
}

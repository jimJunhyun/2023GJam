using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapAtom startRoom;

	public Vector2 shopMinMax;
	public Vector2 healMinMax;
	public Vector2 curseMinMax;
	public Vector2 blessMinMax;


	internal int shopCnt=0;
	internal int healCnt	=0;
	internal int curseCnt = 0;
	internal int blessCnt = 0;

	public const float MAPX = 25;
	public const float MAPY = 25;
	public const float MAPXGAP = 10;
	public const float MAPYGAP = 10;

	Queue<KeyValuePair<MapAtom, Vector3>> createCalls = new Queue<KeyValuePair<MapAtom, Vector3>>();
	HashSet<MapAtom> createds = new HashSet<MapAtom>();
	HashSet<MapAtom> mapCreateds = new HashSet<MapAtom>();

	public bool isClearSight = false;

	public void Create()
	{
		createds.Clear();
		Debug.Log("CREATESTART");
		createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(startRoom, startRoom.rootOffSet));
		createds.Add(startRoom);
		while(createCalls.Count > 0)
		{
			KeyValuePair<MapAtom, Vector3> first = createCalls.Dequeue();
			if (first.Key.up)
			{
				if (!createds.Contains(first.Key.up))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.up, first.Value + Vector3.forward * (MAPY + MAPYGAP)));
					createds.Add(first.Key.up);
					
				}
			}
			if (first.Key.down)
			{
				if (!createds.Contains(first.Key.down))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.down, first.Value + Vector3.back * (MAPY + MAPYGAP)));
					createds.Add(first.Key.down);
				}
			}
			if (first.Key.left)
			{
				if (!createds.Contains(first.Key.left))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.left, first.Value + Vector3.left * (MAPX + MAPXGAP)));
					createds.Add(first.Key.left);
				}
			}
			if (first.Key.right)
			{
				if (!createds.Contains(first.Key.right))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.right, first.Value + Vector3.right * (MAPX + MAPXGAP)));
					createds.Add(first.Key.right);
				}
			}
			first.Key.Init(first.Value, this);
		}
	}

	public void CreateMap()
	{
		MapDrawer.instance.ClearSprite();
		mapCreateds.Clear();

		createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(startRoom, Vector3.zero));
		mapCreateds.Add(startRoom);

		while (createCalls.Count > 0)
		{
			KeyValuePair<MapAtom, Vector3> first = createCalls.Dequeue();
			if (first.Key.up)
			{
				if (!mapCreateds.Contains(first.Key.up))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.up, first.Value + Vector3.up * 200 / MapDrawer.MAPUIYCNT));
					mapCreateds.Add(first.Key.up);
				}
			}
			if (first.Key.down)
			{
				if (!mapCreateds.Contains(first.Key.down))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.down, first.Value + Vector3.down * 200 / MapDrawer.MAPUIYCNT));
					mapCreateds.Add(first.Key.down);
				}
			}
			if (first.Key.left)
			{
				if (!mapCreateds.Contains(first.Key.left))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.left, first.Value + Vector3.left * 200 / MapDrawer.MAPUIXCNT));
					mapCreateds.Add(first.Key.left);
				}
			}
			if (first.Key.right)
			{
				if (!mapCreateds.Contains(first.Key.right))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.right, first.Value + Vector3.right * 200 / MapDrawer.MAPUIXCNT));
					mapCreateds.Add(first.Key.right);
				}
			}
			RoomType t = (first.Key.isQuestion && !isClearSight) ? RoomType.Question : first.Key.type;
			MapDrawer.instance.GenerateSprite(t, first.Value, first.Key == GameManager.Instance.curRoom);
		}
	}

	public void ClearSight()
	{
		isClearSight = true;
		GameManager.Instance.RefreshMap();
	}

	public void HindSight()
	{
		isClearSight = false;
		GameManager.Instance.RefreshMap();
	}
}

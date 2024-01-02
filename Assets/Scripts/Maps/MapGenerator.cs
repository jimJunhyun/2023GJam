using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapAtom startRoom;

	public const float MAPX = 25;
	public const float MAPY = 25;

	Queue<KeyValuePair<MapAtom, Vector3>> createCalls = new Queue<KeyValuePair<MapAtom, Vector3>>();
	HashSet<MapAtom> createds = new HashSet<MapAtom>();
	HashSet<MapAtom> mapCreateds = new HashSet<MapAtom>();

	public void Create()
	{

		createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(startRoom, startRoom.rootOffSet));
		createds.Add(startRoom);
		while(createCalls.Count > 0)
		{
			KeyValuePair<MapAtom, Vector3> first = createCalls.Dequeue();
			if (first.Key.up)
			{
				if (!createds.Contains(first.Key.up))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.up, first.Value + Vector3.forward * MAPY));
					createds.Add(first.Key.up);
				}
			}
			if (first.Key.down)
			{
				if (!createds.Contains(first.Key.down))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.down, first.Value + Vector3.back * MAPY));
					createds.Add(first.Key.down);
				}
			}
			if (first.Key.left)
			{
				if (!createds.Contains(first.Key.left))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.left, first.Value + Vector3.left * MAPX));
					createds.Add(first.Key.left);
				}
			}
			if (first.Key.right)
			{
				if (!createds.Contains(first.Key.right))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.right, first.Value + Vector3.right * MAPX));
					createds.Add(first.Key.right);
				}
			}
			first.Key.Init(first.Value);
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
			RoomType t = first.Key.isQuestion ? RoomType.Question : first.Key.type;
			MapDrawer.instance.GenerateSprite(t, first.Value, first.Key == GameManager.Instance.curRoom);
		}
	}
}

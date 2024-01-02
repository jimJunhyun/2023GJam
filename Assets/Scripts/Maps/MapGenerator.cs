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

	public void Create()
	{

		createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(startRoom, startRoom.rootOffSet));

		while(createCalls.Count > 0)
		{
			KeyValuePair<MapAtom, Vector3> first = createCalls.Dequeue();
			if (first.Key.up)
			{
				if (!createds.Contains(first.Key.up))
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.up, first.Value + Vector3.forward * MAPY));
			}
			if (first.Key.down)
			{
				if (!createds.Contains(first.Key.down))
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.down, first.Value + Vector3.back * MAPY));
			}
			if (first.Key.left)
			{
				if (!createds.Contains(first.Key.left))
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.left, first.Value + Vector3.left * MAPX));
			}
			if (first.Key.right)
			{
				if (!createds.Contains(first.Key.right))
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.right, first.Value + Vector3.right * MAPX));
			}
			first.Key.Init(first.Value);
			createds.Add(first.Key);
		}
	}
}

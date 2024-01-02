using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapList : ScriptableObject
{
    public List<MapObjs> maps;

    public List<MapObjs> GetMapOfCondition(List<ConnectStat> p)
	{
		List<MapObjs> res = new List<MapObjs>();
		for (int i = 0; i < maps.Count; i++)
		{
			bool isValid = true;
			for (int j = 0; j < p.Count; j++)
			{
				isValid &= maps[i].ConnectStatus.Contains(p[j]);
			}
			if (isValid)
			{
				res.Add(maps[i]);
			}
		}
		return res;
	}
}
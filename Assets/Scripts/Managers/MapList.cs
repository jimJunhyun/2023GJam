using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapList : ScriptableObject
{
	public MapObjs startMap;
	public MapObjs shopMap;
	public MapObjs curseMap;
	public MapObjs blessMap;
	public MapObjs healMap;
	public MapObjs bossMap;

    public List<MapObjs> randomMaps;

    public List<MapObjs> GetMapOfCondition(List<ConnectStat> p)
	{
		List<MapObjs> res = new List<MapObjs>();
		for (int i = 0; i < randomMaps.Count; i++)
		{
			bool isValid = true;
			for (int j = 0; j < p.Count; j++)
			{
				isValid &= randomMaps[i].ConnectStatus.Contains(p[j]);
			}
			if (isValid)
			{
				res.Add(randomMaps[i]);
			}
		}
		return res;
	}
}
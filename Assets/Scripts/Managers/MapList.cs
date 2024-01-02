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
			string s = "";
			for (int j = 0; j < p.Count; j++)
			{
				isValid &= maps[i].ConnectStatus.Contains(p[j]);
				s += p[j];
			}
			if (isValid)
			{
				Debug.Log(s + " : " + maps[i].name);
				res.Add(maps[i]);
			}
		}
		return res;
	}
}
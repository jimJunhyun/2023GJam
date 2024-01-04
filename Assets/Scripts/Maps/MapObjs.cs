using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectStat
{
    None = 0,
    Up = 1,
    Down = 2,
    Left = 4,
    Right = 8,
}

public class MapObjs : MonoBehaviour
{
	public RoomType type;

    public int connectStatus;

	HashSet<ConnectStat> stat;
	public HashSet<ConnectStat> ConnectStatus
	{
		get
		{
			if (stat == null)
			{
				stat = new HashSet<ConnectStat>();
				if (connectStatus % 2 == 1)
				{
					stat.Add(ConnectStat.Up);
				}
				if (((connectStatus >> 1) % 2) == 1)
				{
					stat.Add(ConnectStat.Down);
				}
				if (((connectStatus >> 2) % 2) == 1)
				{
					stat.Add(ConnectStat.Left);
				}
				if (((connectStatus >> 3) % 2) == 1)
				{
					stat.Add(ConnectStat.Right);
				}
			}
			return stat;
		}
	}
}
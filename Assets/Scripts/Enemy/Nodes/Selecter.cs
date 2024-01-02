using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : INode
{
	public List<INode> childs = new List<INode>();

	public NodeStat Examine()
	{
		for (int i = 0; i < childs.Count; i++)
		{
			switch (childs[i].Examine())
			{
				case NodeStat.Fail:
					break;
				case NodeStat.Sucs:
					return NodeStat.Sucs;
				case NodeStat.Run:
					return NodeStat.Run;
			}
		}
		return NodeStat.Fail;
	}
}

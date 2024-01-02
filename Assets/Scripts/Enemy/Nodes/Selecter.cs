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
			if (childs[i].Examine() == NodeStat.Sucs)
				return NodeStat.Sucs;
			if (childs[i].Examine() == NodeStat.Run)
				return NodeStat.Run;
		}
		return NodeStat.Fail;
	}
}

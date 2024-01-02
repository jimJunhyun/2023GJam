using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : INode
{
	public List<INode> childs = new List<INode>();

	public NodeStat Examine()
	{
		for (int i = 0; i < childs.Count; i++)
		{
			switch (childs[i].Examine())
			{
				case NodeStat.Sucs:
					break;
				case NodeStat.Fail:
					return NodeStat.Fail;
				case NodeStat.Run:
					return NodeStat.Run;
			}
		}
		return NodeStat.Sucs;
	}
}

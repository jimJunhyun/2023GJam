using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : INode
{
	public INode child;

	public NodeStat Examine()
	{
		if(child.Examine() == NodeStat.Sucs)
		{
			return NodeStat.Fail;
		}
		if(child.Examine() == NodeStat.Fail)
		{
			return NodeStat.Sucs;
		}
		return NodeStat.Run;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFlag : INode
{
	bool stat ;

	public SetFlag(bool initStat = false)
	{
		stat = initStat;
	}

	public void Set()
	{
		stat = true;
	}
	public NodeStat Examine()
	{
		if (stat)
		{
			stat = false;
			return NodeStat.Sucs;
		}
		return NodeStat.Fail;
	}
}

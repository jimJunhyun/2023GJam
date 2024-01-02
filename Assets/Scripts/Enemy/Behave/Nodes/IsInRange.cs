using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInRange : INode
{
	public float rad;
	public Transform target;
	public Transform self;

	public IsInRange(float r, Transform targ, Transform s)
	{
		rad = r;
		target = targ;
		self = s;
	}

	public NodeStat Examine()
	{
		if((target.position - self.position).sqrMagnitude > (rad * rad))
		{
			return NodeStat.Fail;
		}
		return NodeStat.Sucs;
	}
}

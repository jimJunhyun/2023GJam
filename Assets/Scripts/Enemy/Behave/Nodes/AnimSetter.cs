using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSetter : INode
{
	Animator self;
	int hash;

	public AnimSetter(Animator anim, int h)
	{
		self = anim;
		hash = h;
	}

	public NodeStat Examine()
	{
		self.SetTrigger(hash);

		return NodeStat.Sucs;
	}
}

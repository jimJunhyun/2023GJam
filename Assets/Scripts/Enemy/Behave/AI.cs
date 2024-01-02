using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AttackType
{
	Shoot,
	Sweep, 
	Body,

}

[RequireComponent(typeof(LifeObject))]
public class AI : MonoBehaviour, IRhythm
{
	public float atkRange;
	public int atkGap;
	public Stat stat;

	public AttackType type;

	LifeObject life;
	
	public Bullet myBullet;
	public Transform shootPos;
	public float shootPow;

	public float angle;

    Selecter head;
	NavMeshAgent agent;

	SetFlag metronome;

	int beatCnt = 0;

	public bool examining;

	public virtual void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.speed = stat.SPEED;
		life = GetComponent<LifeObject>();
		life.maxHp = stat.MaxHP;
		life.ResetCompletely();

		Sequencer doAttack = new Sequencer();

		metronome = new SetFlag();
		doAttack.childs.Add(metronome);

		IsInRange inRange = new IsInRange(atkRange, GameManager.instance.player.transform, transform);
		doAttack.childs.Add(inRange);

		if( type == AttackType.Shoot)
		{
			ShootAttack shoot = new ShootAttack(agent, myBullet, GameManager.instance.player.transform, shootPos, stat.ATK, shootPow);
			doAttack.childs.Add(shoot);
		}
		else
		{
			SweepAttack sweep = new SweepAttack(agent, atkRange, angle, GameManager.instance.player.transform, stat.ATK);
			doAttack.childs.Add(sweep);
		}

		Move doMove = new Move(GameManager.instance.player.transform, agent);



		head = new Selecter();

		head.childs.Add(doAttack);
		head.childs.Add(doMove);
	}

	public virtual void Update()
	{
		if (examining)
		{
			head.Examine();
		}
	}

	public void StopAI()
	{
		examining = false;
		agent.isStopped = true;
		agent.SetDestination(transform.position);
	}

	public void StartAI()
	{
		examining = true;
		agent.isStopped = false;
	}

	public void BeatUpdate()
	{
		++beatCnt;
		if(beatCnt >= atkGap)
		{
			metronome.Set();
			beatCnt= 0;
		}
	}

	public void BeatUpdateDivideFour()
	{
		++beatCnt;
		if (beatCnt >= atkGap)
		{
			metronome.Set();
			beatCnt = 0;
		}
	}
}

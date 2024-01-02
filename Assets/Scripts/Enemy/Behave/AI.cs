using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AttackType
{
	Shoot,
	Sweep, 
	Body,
	Charge,
	Spin,
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

	ChargeAttack charge;
	SpinAttack spin;

	Animator anim;

	float initSpeed;
	float initAcc;
	int beatCnt = 0;

	internal Vector3 chargeDir;

	public bool examining;

	public bool spinning;
	public bool charging;
	public float chargeDamage;
	public float chargeThreshold;
	public float spinDamage;
	public float spinThreshold;
	public int spinDur;
	public float spinSpd;

	readonly int AttackHash = Animator.StringToHash("Attack");
	readonly int IdleHash = Animator.StringToHash("Idle");

	public virtual void Awake()
	{
		

		agent = GetComponent<NavMeshAgent>();
		agent.speed = stat.SPEED;
		initSpeed = stat.SPEED;
		initAcc = agent.acceleration;

		life = GetComponent<LifeObject>();
		life.maxHp = stat.MaxHP;
		life.ResetCompletely();

		Animator[] anims = GetComponentsInChildren<Animator>();
		int idx = Random.Range(0, anims.Length);
		for (int i = 0; i < anims.Length; i++)
		{
			if(i == idx)
			{
				anims[i].gameObject.SetActive(true);
				anim = anims[i];
			}
			else
			{
				anims[i].gameObject.SetActive(false);
			}
		}

		Sequencer doAttack = new Sequencer();

		metronome = new SetFlag();
		doAttack.childs.Add(metronome);

		IsInRange inRange = new IsInRange(atkRange, GameManager.Instance.player.transform, transform);
		doAttack.childs.Add(inRange);

		AnimSetter setter = new AnimSetter(anim, AttackHash);
		doAttack.childs.Add(setter);

		if( type == AttackType.Shoot)
		{
			ShootAttack shoot = new ShootAttack(agent, myBullet, GameManager.Instance.player.transform, shootPos, stat.ATK, shootPow);
			doAttack.childs.Add(shoot);
		}
		else if(type == AttackType.Charge)
		{
			 charge = new ChargeAttack(agent, this, GameManager.Instance.player.transform, 45, 15, 1f);
			doAttack.childs.Add(charge);
		}
		else if(type == AttackType.Spin)
		{
			spin = new SpinAttack(agent, this, spinDur, spinSpd);
			doAttack.childs.Add(spin);
		}
		else
		{
			SweepAttack sweep = new SweepAttack(agent, atkRange, angle, GameManager.Instance.player.transform, stat.ATK);
			doAttack.childs.Add(sweep);
		}

		Move doMove = new Move(GameManager.Instance.player.transform, agent);



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

		if(agent.velocity.sqrMagnitude > 0.1f)
		{
			anim.SetBool(IdleHash, false);
		}
		else
		{
			anim.SetBool(IdleHash, true);
		}

		if (charging)
		{
			anim.SetTrigger(AttackHash);
			bool invalidDest = (agent.velocity.sqrMagnitude > 0.1f && (chargeDir.x > 0 ? transform.position.x - agent.destination.x  >= 0.1f: transform.position.x - agent.destination.x <= -0.1f)
				&& (chargeDir.z > 0 ? transform.position.z - agent.destination.z >= 0.1f : transform.position.z - agent.destination.z <= -0.1f));
			if (invalidDest)
			{
				charge.StopCharge();
				agent.velocity = Vector3.zero;
			}

			Collider[] cols = Physics.OverlapSphere(transform.position, chargeThreshold, (1 << 8) | (1 << 9));
			if(cols.Length > 0)
			{
				for(int i = 0; i < cols.Length; i++)
				{
					if(cols[i].gameObject == gameObject)
					{
						continue;
					}
					LifeObject p = cols[i].GetComponent<LifeObject>();
					p.Damage(chargeDamage);
					AI other;
					if (other = cols[i].GetComponent<AI>())
					{
						if (other.charging)
						{
							p.OnDead();
							life.OnDead();
						}
					}
					Debug.Log(p.name);
					charge.StopCharge();
					agent.velocity = Vector3.zero;
				}

			}
		}

		if (spinning)
		{
			anim.SetTrigger(AttackHash);
			Collider[] cols = Physics.OverlapSphere(transform.position, spinThreshold, (1 << 8) | (1 << 9) | (1 << 10));
			for (int i = 0; i < cols.Length; i++)
			{
				LifeObject p = cols[i].GetComponent<LifeObject>();
				p.Damage(spinDamage);
			}
		}
	}

	public void ResetSpeed()
	{
		agent.speed = initSpeed;
		agent.acceleration =initAcc;
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
		if (spinning)
		{
			spin.OnNextBeat();
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
		if (spinning)
		{
			spin.OnNextBeat();
		}
	}
}

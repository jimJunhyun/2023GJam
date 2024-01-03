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

	Rigidbody rig;

	SetFlag metronome;

	ChargeAttack charge;
	SpinAttack spin;

	Animator anim;

	float initSpeed;
	float initAcc;
	int beatCnt = 0;

	internal Vector3 chargeDest;

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
		rig = GetComponent<Rigidbody>();

		initSpeed = stat.SPEED;

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
				if(type == AttackType.Shoot)
					shootPos = transform.Find("ShootPos");
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
			ShootAttack shoot = new ShootAttack(rig, myBullet, GameManager.Instance.player.transform, shootPos, stat.ATK, shootPow);
			doAttack.childs.Add(shoot);
		}
		else if(type == AttackType.Charge)
		{
			 charge = new ChargeAttack(rig, this, GameManager.Instance.player.transform, 25, 15, 1f);
			doAttack.childs.Add(charge);
		}
		else if(type == AttackType.Spin)
		{
			spin = new SpinAttack(this, spinDur, spinSpd);
			doAttack.childs.Add(spin);
		}
		else
		{
			SweepAttack sweep = new SweepAttack(rig, atkRange, angle, GameManager.Instance.player.transform, stat.ATK);
			doAttack.childs.Add(sweep);
		}

		Move doMove = new Move(GameManager.Instance.player.transform, rig, this);



		head = new Selecter();

		head.childs.Add(doAttack);
		head.childs.Add(doMove);
	}

	public virtual void Update()
	{
		

		if (examining)
		{
			head.Examine();
			Vector3 v = GameManager.Instance.player.transform.position - transform.position;
			v.y = 0;
			transform.rotation = Quaternion.LookRotation(v);
		}

		if(rig.velocity.sqrMagnitude > 0.1f)
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
			bool invalidDest = (rig.velocity.sqrMagnitude > 0.1f && (transform.position - chargeDest).magnitude < 0.5);
			if (invalidDest)
			{
				charge.StopCharge();
				rig.velocity = Vector3.zero;
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
					rig.velocity = Vector3.zero;
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
		rig.velocity = Vector3.zero;
	}

	public void StopAI()
	{
		examining = false;
		rig.velocity = Vector3.zero;
	}

	public void StartAI()
	{
		examining = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//1.박자따라치기 - 6개박자(따라치기박자포함)
//2.블랙박스 - 5개박자
//3.(긴)거리에 맞으면 검기발사 - 7마디
//4.거리에 맞으면 콩알탄발사 - 3마디
//5.플레이어 주변을 둥글게 이동
[RequireComponent(typeof(LifeObject))]
public class BossAI : MonoBehaviour, IRhythm
{
    public float wanderRadius;
    public float bulletRadius;
    public float slashRadius;
    public int bulletGap = 3;
    public int slashGap = 7;
    public int bBoxGapSp = 5;
    public int followGapSp = 6;

    public Bullet bullet;
    public Bullet slash;

    public Transform shootPos;

    public float bulletPow;
    public float slashPow;

    public Stat stat;

    Rigidbody rig;
    LifeObject life;
    Vector3 dest;
    float curAngle = 90;
    float curAngleRad => curAngle * Mathf.Deg2Rad;

    int beatCnt = 0;
    int tempoCnt = 0;

    bool beating = true;
    bool updating = true;


	public void BeatUpdate()
	{
		if (beating)
		{
            float dist = (GameManager.Instance.player.transform.position - transform.position).magnitude;
            tempoCnt += 1;
            beatCnt += 1;
            if (tempoCnt % followGapSp == 0)
            {
                //??
            }
            else if (tempoCnt % bBoxGapSp == 0)
            {
                //??
            }
            else if (beatCnt % slashGap == 0)
            {
                if (dist <= slashRadius)
                {
                    Bullet ssh = GameObject.Instantiate(bullet, shootPos.position, transform.rotation);
                    ssh.Shoot(slashPow);
                }
            }
            else if (beatCnt % bulletGap == 0)
            {
                if (dist <= bulletRadius)
                {
                    Bullet blt = GameObject.Instantiate(bullet, shootPos.position, transform.rotation);
                    blt.Shoot(bulletPow);
                }
            }
        }
		
    }

	public void BeatUpdateDivideFour()
	{
		if (beating)
		{
            float dist = (GameManager.Instance.player.transform.position - transform.position).magnitude;
            beatCnt += 1;
            if (beatCnt % slashGap == 0)
            {
                if(dist <= slashRadius)
				{
                    Bullet ssh = GameObject.Instantiate(slash, shootPos.position, transform.rotation);
                    ssh.Shoot(slashPow);
                }
               
            }
            else if (beatCnt % bulletGap == 0)
            {
                if (dist <= bulletRadius)
				{
                    Bullet blt = GameObject.Instantiate(bullet, shootPos.position, transform.rotation);
                    blt.Shoot(bulletPow);
                }
                    
            }
        }
		
	}



	// Start is called before the first frame update
	void Awake()
    {
        rig = GetComponent<Rigidbody>();

        life = GetComponent<LifeObject>();
        life.maxHp = stat.MaxHP;
        life.hp = stat.HP;

        life.onDead += () => { Debug.Log("zmfflrj"); };
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.R))
		{
            Activate();
		}

		if (updating)
		{
            Vector3 dir = (dest - transform.position);
            dir.y = 0;
            if (dir.magnitude < 1f)
            {
                SampleNewPos();
            }
			else
			{
                //Vector3 v = (GameManager.Instance.player.transform.position - transform.position);
                //v.y = 0;
                transform.rotation = Quaternion.LookRotation(dir);
                rig.velocity = dir.normalized * stat.SPEED;
            }
        }
        
    }
    public void SampleNewPos()
	{
        if (NavMesh.SamplePosition(GameManager.Instance.player.transform.position
                    + (Vector3.forward * Mathf.Cos(curAngleRad) * wanderRadius)
                    + (Vector3.right * Mathf.Sin(curAngleRad) * wanderRadius)
                    , out NavMeshHit hit, 15f, -1))
        {
            dest = hit.position;
            curAngle += 90;
            curAngle %= 360;
        }
    }

    public void Activate()
	{
        beating = true;
        SampleNewPos();
        updating = true;
    }

    public void StopMove()
	{
        rig.velocity = Vector3.zero;
        beating = false;
        updating = false; 
	}
}

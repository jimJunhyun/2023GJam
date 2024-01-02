using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeObject))]
public class Bullet : MonoBehaviour
{
	Rigidbody rig;
	LifeObject life;
	public float dam;
	private void OnTriggerEnter(Collider other)
	{
		LifeObject lf;
		if (lf = other.gameObject.GetComponent<LifeObject>())
		{
			lf.Damage(dam);
			if (other.GetComponent<Bullet>())
			{
				lf.OnDead();
				life.OnDead();
			}
			life.OnDead();
			
		}
	}

	public void Shoot(float pow)
	{
		rig = GetComponent<Rigidbody>();
		rig.AddForce(transform.forward * pow, ForceMode.Impulse);
		life = GetComponent<LifeObject>();
		life.onDead += ()=>Destroy(gameObject);
		StartCoroutine(DelDead());
	}

	IEnumerator DelDead()
	{
		yield return new WaitForSeconds(3);
		Destroy(gameObject);
	}
}

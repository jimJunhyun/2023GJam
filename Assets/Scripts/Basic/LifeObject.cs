using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeObject : MonoBehaviour
{
    public float hp;
	public float maxHp;

	public bool dead = false;

	private void Awake()
	{
		maxHp = hp;

	}


	public void Damage(float amt)
	{
		hp -= amt;
		if(hp < 0)
		{
			dead = true;
			Destroy(gameObject);
		}
	}
}

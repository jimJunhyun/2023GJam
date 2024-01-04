using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshBossUI : MonoBehaviour
{
    Slider hp;
	private void Awake()
	{
		hp = GetComponent<Slider>();
		hp.gameObject.SetActive(false);
	}

	public void ShowUI()
	{
		hp.gameObject.SetActive(true);
	}

	public void Update()
	{
		if (hp)
		{
			hp.value = (float)(GameManager.Instance.boss.life.hp / (float)GameManager.Instance.boss.life.maxHp);
		}
	}
}

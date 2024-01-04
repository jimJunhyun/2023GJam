using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshBossUI : MonoBehaviour
{
    Slider hp;
	private void Awake()
	{
		hp = GameObject.Find("BossHp").GetComponent<Slider>();
		hp.gameObject.SetActive(false);
	}

	public void ShowUI()
	{
		hp.gameObject.SetActive(true);
	}

	public void Update()
	{
		if (GameManager.Instance.boss)
		{
			hp.value = GameManager.Instance.boss.stat.HP / GameManager.Instance.boss.stat.MaxHP;
		}
	}
}

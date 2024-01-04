using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldUI : MonoBehaviour
{
    TextMeshProUGUI goldTxt;

	private void Awake()
	{
		goldTxt = GetComponentInChildren<TextMeshProUGUI>();
	}

	private void Start()
	{
		RefreshGold();
	}

	public void RefreshGold()
	{
		goldTxt.text = GameManager.Instance.player.Gold.ToString();
	}
}

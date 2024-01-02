using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public const float INTERRANGE = 1.5f;

	public int price;
	public ItemSO data;

    public void Purchase()
	{
		if(GameManager.Instance.player.isUseGold(price))
		{
			GameManager.Instance.player.UseingGold(price);
			GameManager.Instance.player.GetItem(data);
		}

	}
}

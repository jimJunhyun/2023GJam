using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class MapDrawer : MonoBehaviour
{
    public static MapDrawer instance;

    public const float MAPUIXCNT = 15;
    public const float MAPUIYCNT = 15;

    public Sprite normalRoom;
    public Sprite qRoom;
    public Sprite shopRoom;
    public Sprite curseRoom;
    public Sprite healRoom;
    public Sprite startRoom;
    public Sprite bossRoom;

    Image mapPanel;

	Canvas mapCanvas;

	Transform contents;


	Vector3 initContentPos;

	

	private void Awake()
	{
		instance = this;

		mapCanvas = GameObject.Find("MinimapUI").GetComponent<Canvas>();
		mapPanel = GameObject.Find("MapPanel").GetComponent<Image>();
		contents = mapPanel.transform.Find("Contents");
		initContentPos = contents.transform.position;
	}

	public void ClearSprite()
	{
		foreach (Transform item in contents.transform)
		{
			Destroy(item.gameObject);
			//Debug.Log(item.name);
		}
	}

	public void GenerateSprite(RoomType type, Vector3 formPos, bool isCur)
	{
        GameObject mapAtomUI = new GameObject("ROOM");
		mapAtomUI.transform.SetParent(contents);
		mapAtomUI.transform.localPosition = formPos;
		mapAtomUI.transform.localScale = Vector3.right * 2 / MAPUIXCNT + Vector3.up * 2/ MAPUIYCNT;
		Image img = mapAtomUI.AddComponent<Image>();
		switch (type)
		{
			case RoomType.Start:
				img.sprite = startRoom;
				break;
			case RoomType.Normal:
				img.sprite = normalRoom;
				break;
			case RoomType.Question:
				img.sprite = qRoom;
				break;
			case RoomType.Shop:
				img.sprite = shopRoom;
				break;
			case RoomType.Heal:
				img.sprite = healRoom;
				break;
			case RoomType.Curse:
				img.sprite = curseRoom;
				break;
			case RoomType.Boss:
				img.sprite = bossRoom;
				break;
			default:
				break;
		}
		if (isCur)
		{
			img.color = Color.white;
			
			contents.transform.localPosition= mapPanel.transform.position - (initContentPos + formPos);
		}
		else
		{
			img.color = Color.gray * 1.3f;
		}
	}
}

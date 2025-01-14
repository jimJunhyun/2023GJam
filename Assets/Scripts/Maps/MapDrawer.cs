using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class MapDrawer : MonoBehaviour
{
    public static MapDrawer instance;

    public const float MAPUIXCNT = 3;
    public const float MAPUIYCNT = 3;

    public Sprite normalRoom;
    public Sprite qRoom;
    public Sprite shopRoom;
    public Sprite curseRoom;
    public Sprite healRoom;
    public Sprite startRoom;
    public Sprite bossRoom;
    public Sprite blessRoom;

	public HallwayUI hallway;

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
		contents.transform.localPosition = Vector3.zero;
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

	public void GenerateSprite(RoomType type, Vector3 formPos, int conStat , bool isCur)
	{
		GameObject mapAtomUI = new GameObject("ROOM");
		mapAtomUI.transform.SetParent(contents);
		mapAtomUI.transform.rotation=  Quaternion.identity;
		mapAtomUI.transform.localPosition = formPos;
		mapAtomUI.transform.localScale = Vector3.right * 2 / MAPUIXCNT + Vector3.up * 2 / MAPUIYCNT;
		Image img = mapAtomUI.AddComponent<Image>();

		HallwayUI hway = Instantiate(hallway);
		hway.transform.SetParent(mapAtomUI.transform);
		hway.transform.localPosition = Vector3.zero;
		hway.transform.localScale = Vector3.one;
		hway.SetConState(conStat);


		
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
			case RoomType.Blessing:
				img.sprite = blessRoom;
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
			img.color = new Color(1,1, 1, 0.5f);
			hway.SetAlpha(0.5f);
		}
	}
}

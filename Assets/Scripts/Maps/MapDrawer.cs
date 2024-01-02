using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapDrawer : MonoBehaviour
{
    public static MapDrawer instance;

    public const float MAPUIX = 25;
    public const float MAPUIY = 25;

    public Image normalRoom;
    public Image qRoom;
    public Image shopRoom;
    public Image curseRoom;
    public Image healRoom;
    public Image startRoom;
    public Image bossRoom;

    public Image mapPanel;

    public void GenerateSprite(RoomType type, Vector3 formPos)
	{
        Image inst;
		switch (type)
		{
			case RoomType.Start:
				inst = startRoom;
				break;
			case RoomType.Normal:
				inst = normalRoom;
				break;
			case RoomType.Question:
				inst = qRoom;
				break;
			case RoomType.Shop:
				inst = shopRoom;
				break;
			case RoomType.Heal:
				inst = healRoom;
				break;
			case RoomType.Curse:
				inst = curseRoom;
				break;
			case RoomType.Boss:
				inst = bossRoom;
				break;
			default:
				break;
		}
	}
}

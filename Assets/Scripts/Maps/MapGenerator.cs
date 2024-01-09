using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapAtom startRoom;

	public Vector2Int shopMinMax;
	public Vector2Int healMinMax;
	public Vector2Int curseMinMax;
	public Vector2Int blessMinMax;

	public int maxQRoomCnt;

	internal int shopCnt = -1;
	internal int healCnt = -1;
	internal int curseCnt = -1;
	internal int blessCnt = -1;

	public const float MAPX = 60;
	public const float MAPY = 60;
	public const float MAPXGAP = 55;
	public const float MAPYGAP = 55;

	public MapAtom bossRoom;

	Queue<KeyValuePair<MapAtom, Vector3>> createCalls = new Queue<KeyValuePair<MapAtom, Vector3>>();
	HashSet<MapAtom> createds = new HashSet<MapAtom>();
	HashSet<MapAtom> mapCreateds = new HashSet<MapAtom>();

	public bool isClearSight = false;

	public void Create()
	{
		shopCnt = (int)shopMinMax.x;
		healCnt = (int)healMinMax.x;
		curseCnt = (int)curseMinMax.x;
		blessCnt = (int)blessMinMax.x;
		maxQRoomCnt -= (shopCnt + healCnt + curseCnt + blessCnt);
		int r;
		int repCount = 0;
		while(maxQRoomCnt > 0)
		{
			++repCount;
			Debug.Log(shopMinMax.y);
			r = Random.Range(0, (((shopMinMax.y - shopCnt) > maxQRoomCnt) ? maxQRoomCnt : (shopMinMax.y - shopCnt)) + 1);
			shopCnt += r;
			maxQRoomCnt -= r;
			r = Random.Range(0, ((((int)healMinMax.y - healCnt) > maxQRoomCnt) ? maxQRoomCnt : ((int)healMinMax.y - healCnt)) + 1);
			healCnt += r;
			maxQRoomCnt -= r;
			r = Random.Range(0, ((((int)curseMinMax.y - curseCnt) > maxQRoomCnt) ? maxQRoomCnt : ((int)curseMinMax.y - curseCnt)) + 1);
			curseCnt += r;
			maxQRoomCnt -= r;
			r = Random.Range(0, ((((int)blessMinMax.y - blessCnt) > maxQRoomCnt) ? maxQRoomCnt : ((int)blessMinMax.y - blessCnt)) + 1);
			blessCnt += r;
			maxQRoomCnt -= r;
			if(repCount > GameManager.MAXREPCOUNT)
			{
				Debug.Log("SOMETHING WRONG");
				curseCnt += maxQRoomCnt;
				break;
			}
		}
		

		Debug.Log("SHOP : " + shopCnt + ", HEAL : " + healCnt + ", CURSE : " + curseCnt + ", BLESS : " + blessCnt + " REMAIN : " + maxQRoomCnt);

		createds.Clear();
		Debug.Log("CREATESTART");
		createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(startRoom, startRoom.rootOffSet));
		createds.Add(startRoom);
		while(createCalls.Count > 0)
		{
			KeyValuePair<MapAtom, Vector3> first = createCalls.Dequeue();
			if (first.Key.up)
			{
				if (!createds.Contains(first.Key.up))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.up, first.Value + Vector3.forward * (MAPY + MAPYGAP)));
					createds.Add(first.Key.up);
					
				}
			}
			if (first.Key.down)
			{
				if (!createds.Contains(first.Key.down))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.down, first.Value + Vector3.back * (MAPY + MAPYGAP)));
					createds.Add(first.Key.down);
				}
			}
			if (first.Key.left)
			{
				if (!createds.Contains(first.Key.left))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.left, first.Value + Vector3.left * (MAPX + MAPXGAP)));
					createds.Add(first.Key.left);
				}
			}
			if (first.Key.right)
			{
				if (!createds.Contains(first.Key.right))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.right, first.Value + Vector3.right * (MAPX + MAPXGAP)));
					createds.Add(first.Key.right);
				}
			}
			first.Key.Init(first.Value, this);
		}
	}

	public void CreateMap()
	{
		MapDrawer.instance.ClearSprite();
		mapCreateds.Clear();

		createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(startRoom, Vector3.zero));
		mapCreateds.Add(startRoom);
		while (createCalls.Count > 0)
		{
			int conStat = 0;
			KeyValuePair<MapAtom, Vector3> first = createCalls.Dequeue();
			if (first.Key.up)
			{
				if (!mapCreateds.Contains(first.Key.up))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.up, first.Value + Vector3.up * 200 / MapDrawer.MAPUIYCNT));
					mapCreateds.Add(first.Key.up);
				}
				conStat += ((int)ConnectStat.Up);
			}
			if (first.Key.down)
			{
				if (!mapCreateds.Contains(first.Key.down))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.down, first.Value + Vector3.down * 200 / MapDrawer.MAPUIYCNT));
					mapCreateds.Add(first.Key.down);
				}
				conStat += ((int)ConnectStat.Down);
			}
			if (first.Key.left)
			{
				if (!mapCreateds.Contains(first.Key.left))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.left, first.Value + Vector3.left * 200 / MapDrawer.MAPUIXCNT));
					mapCreateds.Add(first.Key.left);
				}
				conStat += ((int)ConnectStat.Left);
			}
			if (first.Key.right)
			{
				if (!mapCreateds.Contains(first.Key.right))
				{
					createCalls.Enqueue(new KeyValuePair<MapAtom, Vector3>(first.Key.right, first.Value + Vector3.right * 200 / MapDrawer.MAPUIXCNT));
					mapCreateds.Add(first.Key.right);
				}
				conStat += ((int)ConnectStat.Right);
			}
			RoomType t = (first.Key.isQuestion && !isClearSight) ? RoomType.Question : first.Key.type;
			if(first.Key.type == RoomType.Boss && first.Key.isRandomizable)
			{
				bossRoom = first.Key;
			}
			MapDrawer.instance.GenerateSprite(t, first.Value, conStat, first.Key == GameManager.Instance.curRoom);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Backslash))
		{
			GameManager.Instance.MovePlayerTo(bossRoom.downPt);
		}
	}

	public void ClearSight()
	{
		isClearSight = true;
		GameManager.Instance.RefreshMap();
	}

	public void HindSight()
	{
		isClearSight = false;
		GameManager.Instance.RefreshMap();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurRoomCheck : MonoBehaviour
{
    MapAtom _curRoom;
    [SerializeField] MapCanvas _maps;

    bool _isFirst = true;
    MapCanvas ms;

    private void Update()
    {
        if (_curRoom == null && GameManager.Instance.curRoom)
        {
            _curRoom = GameManager.Instance.curRoom;
            if (ms != null && ms.gameObject.activeSelf)
            {
                PoolManager.Instance.Push(ms);
            }

        }

        if (GameManager.Instance.curRoom)
        {
            if (GameManager.Instance.curRoom != _curRoom)
            {
                    _curRoom = GameManager.Instance.curRoom;
                    ms = PoolManager.Instance.Pop(_maps.name) as MapCanvas;
                    string t = "";

                    switch (_curRoom.type)
                    {
                        case RoomType.Start:
                            t = "시작방 입장";
                            break;
                        case RoomType.Normal:
                        if (GameManager.Instance.curRoom.IsCleared)
                        {
                            t = "방이 이미 정화 되었습니다";
                        }
                        else
                        {

                            t = "필드방 입장 : 모든 적을 처치하세요";
                            _isFirst = true;
                        }
                            break;
                        case RoomType.Shop:
                            t = "상점방 입장 : 포탈을 찾아 입장하세요";
                            break;
                        case RoomType.Heal:
                            t = "병원방 입장 : 체력이 회복되었습니다";
                            break;
                        case RoomType.Curse:
                            t = "석상방 입장 : 석상과 상호작용 하세요";
                            break;
                        case RoomType.Blessing:
                            t = "석상방 입장 : 포탈을 찾아 석상과 상호작용 하세요";
                            break;
                        case RoomType.Boss:
                            t = "보스방 입장 : 승리하세요";
                            break;
                        case RoomType.Question:
                            t = "";
                            break;
                        default:
                            break;
                    }


                    ms.Init($"{t}", 2f);
            }
            else
            {
                if(_curRoom.type == RoomType.Normal && _isFirst && _curRoom.IsCleared) 
                {
                    _isFirst = false;

                    ms = PoolManager.Instance.Pop(_maps.name) as MapCanvas;
                    ms.Init($"방을 정화 하셨습니다.", 2f);
                
                }
            }
        }
    }
}

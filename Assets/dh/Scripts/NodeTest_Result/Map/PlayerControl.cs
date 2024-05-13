using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public enum type { down = 0, left, right, up };
    [SerializeField]
    private type currentType;
    public type CurrentType
    {
        get => currentType;
        set
        {
            if ((int)value < 0 || (int)value > 3)
            {
                Debug.LogError("Player Type이 올바르지 않습니다.");
            }
            else
            {
                currentType = value;
                SetForwardBlockPos(currentPos);
            }
        }
    }

    [SerializeField]
    private (int x, int y) currentPos = (0, 0);
    public (int x, int y) CurrentPos
    {
        get => currentPos;
        set
        {
            if (value.x < 0 || value.x > 4 || value.y < 0 || value.y > 6) Debug.Log("currentPos 업데이트 중 오류 발생 - 올바르지 못한 인덱스");
            else
            {
                currentPos = value;
                SetForwardBlockPos(value);
            }

        }
    }
    public (int x, int y) forwardBlockPos = (1, 0);


    [Header("map")]
    [SerializeField] private MapInfo mapInfo;


    private void Start()
    {
        currentType = type.down;
        MoveToBlock((0, 0));
    }

    private void OnEnable()
    {
        if (mapInfo.transform.childCount == 0)
        {
            return;
        }
        MoveToBlock((0, 0));
    }





    public void MoveToBlock((int x, int y) forwardBlockPos)
    {
        int blockIndex = 7 * forwardBlockPos.x + forwardBlockPos.y;
        MapBlockType nextBlockType = mapInfo.transform.GetChild(blockIndex).GetComponent<MapBlock>().blockType;

        if (nextBlockType == MapBlockType.Block || forwardBlockPos.x < 0 || forwardBlockPos.x > mapInfo.map2DArrayRowCount || forwardBlockPos.y < 0 || forwardBlockPos.y > mapInfo.map2DArrayColumnCount)
        {
            //index - out of range or Blocked
            Debug.Log("충돌");


            //충돌 애니메이션


            //transfrom.positoin 유지
        }
        else
        {
            Debug.Log($"{forwardBlockPos.x},{forwardBlockPos.y}로 이동");
            // Debug.Log(mapInfo.transform.GetChild(blockIndex).transform.position);
            // Debug.Log(mapInfo.transform.GetChild(blockIndex).transform.localPosition);
            // Debug.Log(mapInfo.transform.GetChild(blockIndex).transform.localToWorldMatrix);

            //transform.position = mapInfo.transform.GetChild(blockIndex).transform.position;
            // Debug.Log(transform.position);
            transform.SetParent(mapInfo.transform.GetChild(blockIndex), false);
            transform.localPosition = Vector3.zero;

            //current pos 업데이트
            currentPos = forwardBlockPos;
        }
    }


    public void SetForwardBlockPos((int x, int y) currentPos)
    {
        switch ((int)currentType)
        {
            case 0:
                forwardBlockPos = (currentPos.x + 1, currentPos.y);
                break;
            case 1:
                forwardBlockPos = (currentPos.x, currentPos.y + 1);
                break;
            case 2:
                forwardBlockPos = (currentPos.x - 1, currentPos.y);
                break;
            case 3:
                forwardBlockPos = (currentPos.x, currentPos.y - 1);
                break;

            default:
                Debug.LogError("ForwardBlockPos를 계산하는데 오류가 발생했습니다");
                break;

        }
    }
}

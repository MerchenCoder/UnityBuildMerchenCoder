using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBlock : MonoBehaviour
{
    [Header("Map Block Item")]
    [SerializeField]
    public MapBlockType blockType;

    [SerializeField]
    [Tooltip("순서대로 Block, Road, Start/End 블록 색깔")]
    private Color[] blockColor = new Color[3]{
        new Color(0.6981132f,0.6981132f,0.6981132f),
        new Color(1,0.9559509f,0.6084906f),
        new Color(0.7783019f,0.9305524f,1)

    };

    [SerializeField]
    private GameObject[] blockChildPrefab;


    [Header("Properties")]
    [SerializeField] private bool isBlocked;
    [SerializeField] private bool isEnd;
    [SerializeField] private bool isStart;

    private void Awake()
    {
        SettingBlock();
    }


    private void SettingBlock()
    {
        if (blockType == MapBlockType.Block)
        {
            //색깔 설정
            GetComponent<Image>().color = blockColor[0];

            //properties 설정
            isBlocked = true;
            isEnd = false;
            isStart = false;

            //child prefab instance 생성
            int randomNum = Random.Range(-1, 2); //-1~1까지
            if (randomNum >= 0)
            {
                Instantiate(blockChildPrefab[randomNum], transform, false);
            }

        }
        else if (blockType == MapBlockType.Road)
        {
            GetComponent<Image>().color = blockColor[1];
            isBlocked = false;
            isEnd = false;
            isStart = false;
            //자식 이미지 없음
        }
        else if (blockType == MapBlockType.Start)
        {
            GetComponent<Image>().color = blockColor[2];
            isBlocked = false;
            isEnd = false;
            isStart = true;

            //자식 이미지 없음
        }
        else
        {
            GetComponent<Image>().color = blockColor[2];
            isBlocked = false;
            isEnd = true;
            isStart = false;

            Instantiate(blockChildPrefab[2], transform, false);
        }
    }
}

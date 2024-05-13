using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreNode : MonoBehaviour, INode, IFollowFlow
{
    [Header("Action")]
    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;


    private GameObject map;


    [Header("Data")]
    private NodeData nodeData;
    [SerializeField] private DataOutPort outPort;
    // Start is called before the first frame update
    void Start()
    {
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "ActionNode";
        nodeData = GetComponent<NodeData>();

    }


    public IEnumerator Execute()
    {
        if (NodeManager.Instance.Mode != "run")
        {
            TestManager.Instance.playerOutput.Add(outputStr);
            yield break;
        }
        if (map == null)
        {
            map = GameObject.FindWithTag("map");
        }
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");

        }

        //전방의 block 탐색
        (int x, int y) forwardBlockPos = player.GetComponent<PlayerControl>().forwardBlockPos;
        nodeData.data_int = Explore(forwardBlockPos);
        nodeData.ErrorFlag = false;


    }

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

    public IEnumerator ProcessData()
    {
        if (nodeData.ErrorFlag)
        {
            NodeManager.Instance.SetCompileError(true, "실행되지 않은 액션 노드의\n 결과값을 가져올 수 없습니다.");
            yield break;
        }

        yield return outPort.SendData();
        GetComponent<NodeData>().ErrorFlag = true;

    }

    /// <summary>
    /// map을 벗어나는 경우 -1 반환,
    /// 장애물이 있거나 막힌 길인 경우 0 반환,
    /// 도로(갈 수 있는 길)인 경우 1 반환,
    /// 시작점인 경우 2 반환,
    /// 끝(목적지)인 경우 3 반환
    /// </summary>
    public int Explore((int x, int y) forwardBlockPos)
    {
        //map을 벗어나는 경우
        if (forwardBlockPos.x > map.GetComponent<MapInfo>().map2DArrayRowCount || forwardBlockPos.x < 0 || forwardBlockPos.y < 0 || forwardBlockPos.y > map.GetComponent<MapInfo>().map2DArrayColumnCount)
        {
            return -1;
        }


        int blockIndex = forwardBlockPos.x * 7 + forwardBlockPos.y;
        MapBlockType blockType = map.transform.GetChild(blockIndex).GetComponent<MapBlock>().blockType;

        //장애물
        if (blockType == MapBlockType.Block)
        {
            return 0;
        }
        //도로
        else if (blockType == MapBlockType.Road)
        {
            return 1;
        }
        //시작점
        else if (blockType == MapBlockType.Start)
        {
            return 2;
        }
        //끝(목적지)
        else
        {
            return 3;
        }
    }


}

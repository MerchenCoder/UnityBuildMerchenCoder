using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour, INode, IFollowFlow
{
    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    // private GameObject playerActionBubble;


    private enum RotateMode { left = 0, right = 1 }
    [SerializeField]
    private RotateMode rotateMode;


    // Start is called before the first frame update
    void Start()
    {
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "ActionNode";
    }

    public IEnumerator Execute()
    {
        if (NodeManager.Instance.Mode != "run")
        {
            TestManager.Instance.playerOutput.Add(outputStr);
            yield break;
        }
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        // 현재 플레이어의 회전 각도
        Vector3 startAngle = player.transform.eulerAngles;
        Vector3 endAngle;
        if (rotateMode == RotateMode.left)
        {
            endAngle = startAngle + new Vector3(0, 0, 90);
            endAngle.y = endAngle.y % 360; // Y 축 각도를 0부터 360도 사이로 제한

        }
        else
        {
            endAngle = startAngle - new Vector3(0, 0, 90);
            endAngle.y = (endAngle.y + 360) % 360; // Y 축 각도를 0부터 360도 사이로 제한

        }

        // RotateAtoB 코루틴을 호출하고 start와 end 값을 전달
        yield return StartCoroutine(RotateAtoB(startAngle, endAngle));


        //잠깐 대기
        yield return new WaitForSeconds(0.3f);
    }


    public IEnumerator ProcessData()
    {
        throw new System.NotImplementedException();
    }

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }


    private IEnumerator RotateAtoB(Vector3 start, Vector3 end)
    {
        float percent = 0;
        float rotateTime = 0.5f;

        while (percent < 1)
        {
            percent += Time.deltaTime / rotateTime;
            Vector3 angle = Vector3.Lerp(start, end, percent);
            player.transform.rotation = Quaternion.Euler(angle);
            yield return null;
        }

        //type 업데이트
        int playerType = (int)player.GetComponent<PlayerControl>().CurrentType;
        if (rotateMode == RotateMode.left)
        {
            player.GetComponent<PlayerControl>().CurrentType = (PlayerControl.type)((playerType + 1) % 4);
        }
        else
        {
            player.GetComponent<PlayerControl>().CurrentType = (PlayerControl.type)((playerType - 1 + 4) % 4);
        }


    }
}

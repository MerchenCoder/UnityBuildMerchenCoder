using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetValueNode : MonoBehaviour, INode
{
    private NodeNameManager nodeNameManager;
    [SerializeField] private DataOutPort outPort;

    public IEnumerator Execute()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator ProcessData()
    {
        transform.GetChild(0).GetComponent<AddValueBtn>().OnChangeValue();
        Debug.Log("GetValueNode 내부 ProcessData 실행중");
        if (transform.GetChild(0).GetComponent<AddValueBtn>().dropdown.value == 0)
        {
            Debug.Log("변수가 선택되지 않음");
            NodeManager.Instance.SetCompileError(true, "value");

            yield break;
        }
        if (!transform.GetChild(0).GetComponent<AddValueBtn>().IsInit())
        {
            Debug.Log("추가한 변수가 초기화되지 않은 상태로 사용됨");
            NodeManager.Instance.SetCompileError(true, "초기화되지 않은 변수를 사용하였습니다.\n실행이 중단되었습니다.");

            yield break;
        }
        GetComponent<NodeData>().ErrorFlag = false;
        yield return outPort.SendData();
        GetComponent<NodeData>().ErrorFlag = true;

    }

    void Start()
    {
        GetComponent<NodeData>().ErrorFlag = true;
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "GetValueNode";
    }
}
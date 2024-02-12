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
        if (transform.GetChild(0).GetComponent<AddValueBtn>().dropdown.value == 0)
        {
            Debug.Log("변수가 선택되지 않음");
            NodeManager.Instance.SetCompileError(true);

            yield return null;
        }
        if (!transform.GetChild(0).GetComponent<AddValueBtn>().IsInit())
        {
            Debug.Log("추가한 변수가 초기화되지 않은 상태로 사용됨");
            NodeManager.Instance.SetCompileError(true);

            yield return null;
        }
        yield return outPort.SendData();
    }

    void Start()
    {
        GetComponent<NodeData>().ErrorFlag = true;
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "GetValueNode";
    }
}
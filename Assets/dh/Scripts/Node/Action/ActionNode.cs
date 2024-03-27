// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class ActionNode : MonoBehaviour, INode, IFollowFlow
// {
//     //NodeData 등 데이터와 관련된 로직은 액션 노드마다 개별 스크립트 파서 처리하기 (데이터 포트 안쓰는 액션 노드도 있기 때문에 플로우, output 출력 등 공통적인 것만 처리함)
//     //아래 Execute()나 ProcessData()에서 개별적으로 작성한 IEnumerator 호출하고 대기하면됨.
//     public string outputStr;
//     public string actionComponent;
//     //public Types types
//     //private NodeManager nodeManager;
//     private NodeNameManager nodeNameManager;

//     // Start is called before the first frame update
//     void Start()
//     {

//         nodeNameManager = GetComponent<NodeNameManager>();
//         nodeNameManager.NodeName = "ActionNode";
//     }



//     public IEnumerator Execute()
//     {
//         //실행시 처리할 로직이 있는 코루틴 호출해주면 된다.
//         //yield return GetComponent(actionComponent).Execute();
//         //출력 배열에 반영
//         TestManager.Instance.playerOutput.Add(outputStr);
//         yield return null;
//     }

//     public IEnumerator ProcessData()
//     {
//         throw new System.NotImplementedException();
//     }

//     public FlowoutPort NextFlow()
//     {
//         return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
//     }
// }

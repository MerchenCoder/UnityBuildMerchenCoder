using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PatternNode : MonoBehaviour, IFollowFlow, INode
{

    public string outputStr;
    public string colorCode;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    private GameObject playerChatBubble;
    private Animator patternAnim;




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
            playerChatBubble = GameObject.FindWithTag("ResultPanel_Bubble").transform.GetChild(0).gameObject;
            patternAnim = player.GetComponentInChildren<Animator>(true);
        }

        patternAnim.SetBool("Pattern", true);
        yield return new WaitForSeconds(2f);
        patternAnim.SetBool("Pattern", false);
        if (playerChatBubble != null && !playerChatBubble.activeSelf)
        {
            Debug.Log(playerChatBubble.GetComponentInChildren<TMP_Text>(true).alignment);
            playerChatBubble.GetComponentInChildren<TMP_Text>(true).alignment = TextAlignmentOptions.Left;

            playerChatBubble.SetActive(true);
        }
        playerChatBubble.GetComponentInChildren<TMP_Text>().text += "<color=#" + colorCode + ">" + outputStr + "</color>";

        //출력 배열에 반영
        TestManager.Instance.playerOutput.Add(outputStr);
        yield return new WaitForSeconds(0.5f);
        yield return null;
    }

    public FlowoutPort NextFlow()
    {
        FlowoutPort result = this.transform.Find("outFlow").GetComponent<FlowoutPort>();
        if (NodeManager.Instance.Mode == "run" && result.transform.GetComponentInParent<NodeNameManager>(true).NodeName == "EndNode")
        {
            playerChatBubble.SetActive(false);
        }
        return result;
    }

    public IEnumerator ProcessData()
    {
        throw new System.NotImplementedException();
    }
}

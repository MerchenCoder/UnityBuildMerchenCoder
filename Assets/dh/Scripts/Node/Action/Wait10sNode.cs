using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wait10sNode : MonoBehaviour, INode, IFollowFlow
{
    [Header("Action")]
    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    // private Animator waitAnim;
    private GameObject playerActionBubble;
    private SpriteRenderer timerSprite;

    // Start is called before the first frame update
    void Start()
    {
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "ActionNode";


    }
    public IEnumerator Execute()
    {
        //실행모드일 때
        if (NodeManager.Instance.Mode == "run")
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("ResultPanel").transform.GetChild(0).gameObject;
                playerActionBubble = player.transform.GetChild(0).GetChild(0).gameObject;
                timerSprite = playerActionBubble.transform.GetChild(0).GetComponent<SpriteRenderer>();
                //waitAnim = player.GetComponentInChildren<Animator>(true);
            }

            //waitAnim.SetBool("Wait", true);

            playerActionBubble.SetActive(true);
            yield return StartCoroutine(nameof(Wait10Seconds));
            //waitAnim.SetBool("Wait", false);
            TestManager.Instance.playerOutput.Add(outputStr);
            yield return new WaitForSeconds(0.5f);


        }
        //실행모드가 아닌 제출 모드일때
        else
        {

            TestManager.Instance.playerOutput.Add(outputStr);

            yield break;
        }
    }




    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }

    public IEnumerator ProcessData()
    {
        yield return null;
    }


    IEnumerator Wait10Seconds()
    {
        // if (playerActionBubble == null)
        // {
        //     playerActionBubble = player.transform.GetChild(0).GetChild(0).gameObject;

        // }
        // Debug.Log(timerSprite.material.GetFloat("_Arc1"));

        float percent = 0;
        float waitTime = 0.5f;
        while (percent < 1)
        {
            percent += Time.deltaTime / waitTime;
            timerSprite.material.SetFloat("_Arc1", (int)(percent * 360));
            yield return null;
        }
        timerSprite.material.SetFloat("_Arc1", 0);
        playerActionBubble.SetActive(false);
    }

}
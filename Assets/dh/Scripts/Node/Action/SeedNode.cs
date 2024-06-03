using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedNode : MonoBehaviour, INode, IFollowFlow
{
    public string outputStr;
    private NodeNameManager nodeNameManager;
    private GameObject player;
    private GameObject playerActionBubble;

    Animator feedAnim;
    private void Start()
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
            player = GameObject.FindWithTag("ResultPanel").transform.GetChild(0).gameObject;
            playerActionBubble = player.transform.GetChild(0).GetChild(0).gameObject;
            feedAnim = player.GetComponentInChildren<Animator>(true);
        }

        Vector2 anchoredPosition = player.GetComponent<RectTransform>().anchoredPosition;
        //Vector2 originPosition = anchoredPosition;
        if (anchoredPosition.x < -185 || anchoredPosition.x > 451)
        {
            int randomNumber = Random.Range(-185 / 30, 451 / 30) * 30;
            anchoredPosition.x = randomNumber;
            player.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;

        }

        if (NodeManager.Instance.Mode == "run")
        {
            for (int i = 0; i < 3; i++)
            {
                playerActionBubble.SetActive(true);
                //실행시 처리할 로직이 있는 코루틴 호출해주면 된다.
                feedAnim.SetBool("Seed", true);
                yield return new WaitForSeconds(2f);
                playerActionBubble.SetActive(false);
                feedAnim.SetBool("Seed", false);
                //출력 배열에 반영
                TestManager.Instance.playerOutput.Add(outputStr);
                feedAnim.GetComponent<AnimationAudioControl>().StopAnimationSound();
                yield return new WaitForSeconds(1f);

                //플레이어 위치 변경//
                // -185부터 451까지의 값 중에서 최소 30씩 간격이 나는 난수를 뽑음
                int randomNumber = Random.Range(-185 / 30, 451 / 30) * 30;
                anchoredPosition = player.GetComponent<RectTransform>().anchoredPosition;
                anchoredPosition.x = randomNumber;
                player.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                TestManager.Instance.playerOutput.Add(outputStr);
            }
        }
        yield return null;
    }

    public IEnumerator ProcessData()
    {
        throw new System.NotImplementedException();
    }

    public FlowoutPort NextFlow()
    {
        return this.transform.Find("outFlow").GetComponent<FlowoutPort>();
    }
}

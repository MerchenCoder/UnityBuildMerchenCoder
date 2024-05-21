using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnNode : MonoBehaviour, INode, IFollowFlow
{
    public AudioClip[] audioClip;
    [SerializeField] private AudioSource audioSource;


    public string outputStr;
    public int direction;
    //left = -1 right = 1
    private NodeNameManager nodeNameManager;
    private GameObject player;
    private GameObject playerActionBubble_left;
    private GameObject playerActionBubble_right;
    private Animator turnAnim;


    // Start is called before the first frame update
    void Start()
    {
        nodeNameManager = GetComponent<NodeNameManager>();
        nodeNameManager.NodeName = "ActionNode";
    }

    public IEnumerator Execute()
    {
        if (audioSource == null)
            audioSource = nodeNameManager.AutoAudioSetting.AudioSource;

        if (NodeManager.Instance.Mode != "run")
        {
            TestManager.Instance.playerOutput.Add(outputStr);
            yield break;
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("ResultPanel").transform.GetChild(0).gameObject;
            playerActionBubble_left = player.transform.GetChild(0).GetChild(0).gameObject;
            playerActionBubble_right = player.transform.GetChild(0).GetChild(1).gameObject;
            turnAnim = player.GetComponentInChildren<Animator>(true);
        }
        if (direction > 0)
        {
            audioSource.PlayOneShot(audioClip[0]);
            playerActionBubble_left.SetActive(true);
            playerActionBubble_left.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            audioSource.PlayOneShot(audioClip[1]);
            playerActionBubble_right.SetActive(true);
            playerActionBubble_right.transform.GetChild(0).gameObject.SetActive(true);
        }

        //실행시 처리할 로직이 있는 코루틴 호출해주면 된다.
        turnAnim.SetBool("Unlock", true); //Unlock이랑 똑같은 애니메이션이라서 그대로 사용
        yield return new WaitForSeconds(1f);
        playerActionBubble_left.SetActive(false);
        playerActionBubble_right.SetActive(false);
        audioSource.Stop();
        turnAnim.SetBool("Unlock", false);
        //출력 배열에 반영
        TestManager.Instance.playerOutput.Add(outputStr);
        yield return new WaitForSeconds(0.5f);
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

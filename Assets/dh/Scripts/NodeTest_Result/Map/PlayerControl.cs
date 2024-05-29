using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private AnimationAudioControl animationAudioControl;


    public enum type { down = 0, left, right, up };
    [SerializeField]
    private type currentType;
    public type CurrentType
    {
        get => currentType;
        set
        {
            if ((int)value < 0 || (int)value > 3)
            {
                Debug.LogError("Player Type이 올바르지 않습니다.");
            }
            else
            {
                currentType = value;
                SetForwardBlockPos(currentPos);
            }
        }
    }

    [SerializeField]
    private (int x, int y) currentPos = (0, 0);
    public (int x, int y) CurrentPos
    {
        get => currentPos;
        set
        {
            if (value.x < 0 || value.x > 4 || value.y < 0 || value.y > 6) Debug.Log("currentPos 업데이트 중 오류 발생 - 올바르지 못한 인덱스");
            else
            {
                currentPos = value;
                SetForwardBlockPos(value);
            }

        }
    }
    public (int x, int y) forwardBlockPos = (1, 0);


    [Header("map")]
    [SerializeField] private MapInfo mapInfo;


    private Animator playerAnim;

    private void Awake()
    {
        NodeManager.Instance.OnRunProgramCompleted += EndPosition;
    }
    private void OnDestroy()
    {

        if (NodeManager.Instance != null)
        {
            NodeManager.Instance.OnRunProgramCompleted -= EndPosition;
        }
    }



    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        currentType = type.down;
        StartCoroutine(ResetMove((0, 0)));
        animationAudioControl = GetComponent<AnimationAudioControl>();

    }

    public void PlayerInitiate()
    {
        transform.rotation = Quaternion.identity;
        CurrentPos = (0, 0);
        CurrentType = type.down;

        if (mapInfo.transform.childCount == 0)
        {
            return;
        }
        StartCoroutine(ResetMove(CurrentPos));

    }
    public IEnumerator ResetMove((int x, int y) forwardBlockPos)
    {
        int blockIndex = 7 * forwardBlockPos.x + forwardBlockPos.y;
        MapBlockType nextBlockType = mapInfo.transform.GetChild(blockIndex).GetComponent<MapBlock>().blockType;

        transform.SetParent(mapInfo.transform.GetChild(blockIndex), false);
        transform.localPosition = Vector3.zero;

        //current pos 업데이트
        CurrentPos = forwardBlockPos;
        yield return null;
    }



    public IEnumerator MoveToBlock((int x, int y) forwardBlockPos)
    {
        bool isRunMode = NodeManager.Instance.Mode == "run" ? true : false;

        if (forwardBlockPos.x < 0 || forwardBlockPos.x >= mapInfo.map2DArrayRowCount || forwardBlockPos.y < 0 || forwardBlockPos.y >= mapInfo.map2DArrayColumnCount)
        {
            //index - out of range or Blocked
            Debug.Log("충돌");
            if (!isRunMode)
            {
                yield break;
            }

            //충돌 애니메이션
            animationAudioControl.PlayAnimationSound(1);
            playerAnim.SetBool("Hit", true);
            yield return new WaitForSeconds(0.5f);
            playerAnim.SetBool("Hit", false);
            animationAudioControl.StopAnimationSound();

            //transfrom.positoin 유지
            yield break;
        }
        int blockIndex = 7 * forwardBlockPos.x + forwardBlockPos.y;
        MapBlockType nextBlockType = mapInfo.transform.GetChild(blockIndex).GetComponent<MapBlock>().blockType;

        if (nextBlockType == MapBlockType.Block)
        {
            //index - out of range or Blocked
            Debug.Log("충돌");

            if (!isRunMode)
            {
                yield break;
            }
            else
            {
                //충돌 애니메이션
                animationAudioControl.PlayAnimationSound(1);
                playerAnim.SetBool("Hit", true);
                yield return new WaitForSeconds(0.5f);
                playerAnim.SetBool("Hit", false);
                animationAudioControl.StopAnimationSound();

                //transfrom.positoin 유지
                yield return null;
            }



        }

        else
        {

            if (!isRunMode)
            {
                Debug.Log($"{forwardBlockPos.x},{forwardBlockPos.y}로 이동");
                transform.SetParent(mapInfo.transform.GetChild(blockIndex), false);
                transform.localPosition = Vector3.zero;
                CurrentPos = forwardBlockPos;
                yield return null;


            }
            else
            {
                animationAudioControl.PlayAnimationSound(4);
                Debug.Log($"{forwardBlockPos.x},{forwardBlockPos.y}로 이동");
                transform.SetParent(mapInfo.transform.GetChild(blockIndex), false);
                transform.localPosition = Vector3.zero;

                //current pos 업데이트
                CurrentPos = forwardBlockPos;
                yield return new WaitForSeconds(0.4f);
                animationAudioControl.StopAnimationSound();

                // animationAudioControl.StopAnimationSound();
                yield return null;
            }


        }
    }


    public void SetForwardBlockPos((int x, int y) currentPos)
    {
        Debug.Log("forward pos계산 새로하기");
        switch ((int)currentType)
        {
            case 0:
                forwardBlockPos = (currentPos.x + 1, currentPos.y);
                break;
            case 1:
                forwardBlockPos = (currentPos.x, currentPos.y + 1);
                break;
            case 2:
                forwardBlockPos = (currentPos.x - 1, currentPos.y);
                break;
            case 3:
                forwardBlockPos = (currentPos.x, currentPos.y - 1);
                break;

            default:
                Debug.LogError("ForwardBlockPos를 계산하는데 오류가 발생했습니다");
                break;


        }
        Debug.Log($"forwardpos = {forwardBlockPos.x},{forwardBlockPos.y}");
    }


    public void EndPosition()
    {
        string answer = CurrentPos.x.ToString() + "," + CurrentPos.y.ToString();
        Debug.Log("플레이어 최종 위치 결과배열에 추가 : " + answer);
        TestManager.Instance.playerOutput.Add(answer);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChatBubble : MonoBehaviour
{
    public GameObject spritePosition;
    private float printDuration = 2f;
    // Start is called before the first frame update
    void Awake()
    {
        SetSpritePosition();
        transform.position = Camera.main.WorldToScreenPoint(spritePosition.transform.position);
    }
    // void OnEnable()
    // {
    //     if (spritePosition == null)
    //     {
    //         SetSpritePosition();
    //     }
    // }

    // Update is called once per frame
    void OnEnable()
    {

        if (spritePosition == null)
        {
            SetSpritePosition();
        }
        transform.position = Camera.main.WorldToScreenPoint(spritePosition.transform.position);
    }


    public void SetSpritePosition()
    {
        Debug.Log(GameObject.FindGameObjectWithTag("Player").name);
        string[] name = gameObject.name.Split("_");
        if (name[0] == "player")
        {

            spritePosition = FindChildrenWithTag(GameObject.FindGameObjectWithTag("Player").transform, "bubblePosition");

        }
        else if (name[0] == "Npc" || name[0] == "npc" || name[0] == "NPC")
        {
            spritePosition = FindChildrenWithTag(GameObject.FindGameObjectWithTag("NPC").transform, "bubblePosition");
            print(spritePosition.name);
        }
        else
        {
            print("name이 잘못되어 있거나 태그가 지정되지 않았습니다.");
        }
    }

    GameObject FindChildrenWithTag(Transform parent, string tag)
    {
        // 결과를 저장할 List
        List<GameObject> result = new List<GameObject>();

        // 모든 자식 오브젝트 탐색
        foreach (Transform child in parent)
        {
            // 자식 오브젝트의 태그가 원하는 태그와 일치하는지 확인
            if (child.CompareTag(tag))
            {
                // 일치하는 경우 결과 List에 추가
                return child.gameObject;
            }
        }
        return null;
    }

    public IEnumerator Talk(string str)
    {
        GetComponentInChildren<TMPro.TMP_Text>().text = str;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(printDuration);
        Debug.Log("말풍선 안보이게하기");
        gameObject.SetActive(false);

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlChatBubble : MonoBehaviour
{
    public GameObject spritePosition;
    private float printDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        SetSpritePosition();
        transform.position = Camera.main.WorldToScreenPoint(spritePosition.transform.position + new Vector3(3f, 2f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(spritePosition.transform.position + new Vector3(3f, 2f, 0));
    }


    public void SetSpritePosition()
    {
        string[] name = gameObject.name.Split("_");
        if (name[0] == "player")
        {
            spritePosition = GameObject.FindGameObjectWithTag("Player");
        }
        else if (name[0] == "Npc" || name[0] == "npc" || name[0] == "NPC")
        {
            spritePosition = GameObject.FindGameObjectWithTag("NPC");
        }
        else
        {
            print("name이 잘못되어 있거나 태그가 지정되지 않았습니다.");
        }
    }

    private void OnEnable()
    {
        if (spritePosition == null)
        {
            SetSpritePosition();
        }
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

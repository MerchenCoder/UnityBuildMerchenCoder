using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBtn : MonoBehaviour
{
    Dialogue dialogue;
    bool canTouch;

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<Dialogue>();
        if (this.CompareTag("AfterQuizDia")) gameObject.SetActive(false);
    }

    public void DialogueBtnDown()
    {
        if(canTouch)
        {
            dialogue.DialogueStart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            canTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
        {
            canTouch = false;
        }
    }
}

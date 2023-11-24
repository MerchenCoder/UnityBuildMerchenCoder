using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBtn : MonoBehaviour
{
    Dialogue dialogue;
    bool canTouch;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<Dialogue>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    Dialogue thisDialogue;

    private void Start()
    {
        thisDialogue = GetComponent<Dialogue>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            thisDialogue.DialogueStart();
        }
    }
}

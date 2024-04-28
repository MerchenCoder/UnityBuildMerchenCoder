using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    public int CardID;
    private Animator animator;
    private CardContainer cardContainer;
    private bool isHiding;

    void Start()
    {
        animator = GetComponent<Animator>();
        cardContainer = transform.parent.GetComponent<CardContainer>();
        isHiding = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardContainer.canSelect && cardContainer.firstCard != this && isHiding)
        {
            Show();
            cardContainer.SelectCard(this);
        }
    }

    public void Show()
    {
        animator.SetTrigger("ShowCard");
        isHiding = false;
    }

    public void Hide()
    {
        animator.SetTrigger("HideCard");
        isHiding = true;
    }

    public void ActiveFalse()
    {
        isHiding = false;
        animator.SetTrigger("IdleCard");
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
}

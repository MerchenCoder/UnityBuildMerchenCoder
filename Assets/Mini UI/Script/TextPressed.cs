using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _gameObject;
    private Vector2 vector2;
    public float UnitsToMove;

    void Start()
    {
        _gameObject = transform.GetChild(0).GetComponent<RectTransform>();
        vector2 = _gameObject.GetComponent<RectTransform>().anchoredPosition;
    }

    public void moveDown()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, UnitsToMove);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(vector2.x, vector2.y);
        if (FlagManager.instance != null)
        {
            Debug.Log(gameObject.name);
            if(FlagManager.instance.flagStr == gameObject.name)
            {
                FlagManager.instance.OffFlag();
            }
        }
    }
}

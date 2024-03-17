using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    public ThisItem _thisItem;
    Item thisItem;
    public TMPro.TextMeshProUGUI _itemNameTxt;
    public Text _itemPriceTxt;

    public Button setButton;
    public Button buyButton;

    // Start is called before the first frame update
    void Start()
    {
        thisItem = _thisItem.item;

        buyButton.gameObject.SetActive(false);
        setButton.gameObject.SetActive(false);

        transform.GetChild(0).GetComponent<Image>().sprite = thisItem.item_Image;
        transform.GetChild(0).GetComponent<Image>().SetNativeSize();
        _itemNameTxt.text = thisItem.item_name;
        _itemPriceTxt.text = thisItem.price;
        
        if(_thisItem.isBought) setButton.gameObject.SetActive(true);
        else buyButton.gameObject.SetActive(true);
    }

    public void UpdateThisItemUI()
    {
        if (_thisItem.isBought) { setButton.gameObject.SetActive(true); buyButton.gameObject.SetActive(false); }
        else { setButton.gameObject.SetActive(false); buyButton.gameObject.SetActive(true); }
    }
}

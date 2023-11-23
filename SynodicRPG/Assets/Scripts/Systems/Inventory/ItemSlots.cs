using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlots : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField]public bool selected = false;
    [SerializeField]
    private Item _item;

    public TextMeshProUGUI amountText;
    public int amount = 0;

    public Button button;


    public Item Item {  
        get { return _item; } 
        set {
            _item = value;
        

            if (_item == null)
            {
                image.enabled = false;
                amountText.text = " ";
            }
            else
            {
                image.sprite = _item.Icon;
                image.enabled = true;

                UpdateText();
            }
        
        
        }
    }


    public void UpdateText()
    {
        if(_item != null)
        {
            if (_item.stackable && amount > 1)
            {
                amountText.text = amount.ToString();
            }
            else
            {
                amountText.text = " ";
            }
        }
    }


}

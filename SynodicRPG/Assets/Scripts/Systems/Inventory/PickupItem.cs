using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            bool i = InventoryManager.instance.AddItem(item);
            if (i)
            {
                Destroy(gameObject);
            }
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject breakFX, itemToDrop;
    public bool dropItem;
    public int dropChance = 25;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Sword")
        {
            Instantiate(breakFX, transform.position, transform.rotation);

            if(dropItem)
            {
                DropItem();
            }

            Destroy(gameObject);
        }
    }


    public void SelfDestruct()
    {
        Destroy(gameObject);
    }


    public void DropItem()
    {
        int i = Random.Range(0, 100);

        if(i <= dropChance)
        {
            Instantiate(itemToDrop, transform.position, transform.rotation);
        }
    }
}

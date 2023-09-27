using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject breakFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Sword")
        {
            Instantiate(breakFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}

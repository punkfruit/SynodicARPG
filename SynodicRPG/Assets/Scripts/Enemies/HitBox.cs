using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Enemy thisEnemy;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sword")
        {
            thisEnemy.TakeDamage(PlayerController.instance.weaponDamage);
            thisEnemy.anim.SetTrigger("Flash");
        }
    }
}

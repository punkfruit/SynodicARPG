using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemHealthRecovery : Item
{
   

    public override bool UseItem()
    {
        Debug.Log("used health recovery item");
        return true;
    }
}

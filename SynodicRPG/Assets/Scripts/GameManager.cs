using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }



    public static event Action<string> OnInputDeviceChanged;

   

    public static void NotifyInputDeviceChanged(string deviceType)
    {
        OnInputDeviceChanged?.Invoke(deviceType);
    }



    public void testAddItem(Item item)
    {
        InventoryManager.instance.AddItem(item);
    }
}

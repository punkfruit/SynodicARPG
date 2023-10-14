using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Slider healthSlider;


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
    
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = PlayerController.instance.maxhealth;
        healthSlider.value = PlayerController.instance.health;
    }

    // Update is called once per frame
    

    public void UpdateHealthSlider()
    {
        healthSlider.value = PlayerController.instance.health;
    }
}

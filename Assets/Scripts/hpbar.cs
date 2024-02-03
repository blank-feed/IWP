using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpbar : MonoBehaviour
{
    public Slider healthbar;
    public Image FillImg;

    void Start()
    {
        healthbar.maxValue = PlayerManager.instance.maxHealth;
        healthbar.value = PlayerManager.instance.health;
    }

    void Update()
    {
        healthbar.value = PlayerManager.instance.health;
        if (PlayerManager.instance.health <= PlayerManager.instance.maxHealth / 2 && PlayerManager.instance.health > PlayerManager.instance.maxHealth / 5)
        {
            FillImg.color = new Vector4(1, 0.92f, 0.016f, 1);
        }
        else if (PlayerManager.instance.health <= PlayerManager.instance.maxHealth / 5)
        {
            FillImg.color = new Vector4(1, 0, 0, 1);
        }
        else
        {
            FillImg.color = new Vector4(0, 1, 0, 1);
        }
    }
}

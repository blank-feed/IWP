using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    //public PlayerData PD;
    //public EnemyData ED;

    public int Phealth;
    public Sprite Psprite;
    public int Ehealth;
    public Sprite Esprite;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerData()
    {
        Phealth = PlayerManager.instance.health;
        Psprite = PlayerManager.instance.playerSprite;
    }

    public void SetEnemyData(GameObject other)
    {
        Ehealth = 10;
        Esprite = other.GetComponent<SpriteRenderer>().sprite;
    }

    public int GetPlayerHealth()
    {
        return Phealth;
    }

    public Sprite GetPlayerImg()
    {
        return Psprite;
    }

    public int GetEnemyHealth()
    {
        return Ehealth;
    }

    public Sprite GetEnemyImg()
    {
        return Esprite;
    }

    public void SetHealth(int healthDeducted, string target)
    {
        if (target == "enemy")
        {
            Ehealth -= healthDeducted;
        }
        else
        {
            Phealth -= healthDeducted;
        }
    }    

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class EnemyData
{
    public int Ehealth;
    public Image Esprite;
}

public class PlayerData
{
    public int Phealth;
    public Image Psprite;
}
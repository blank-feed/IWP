using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Melee,
        Ranged,
        Healer
    }

    public EnemyType enemyType;
    public int health = 100;
    public int attack = 15;
    public int baseatk = 15;
    public Vector3Int startingPosition;
    GameObject hpbar;

    public GameObject EnemyHpBarPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Canvas canvas = FindObjectOfType<Canvas>();
        GameObject healthbars = GameObject.Find("healthbars");
        hpbar = Instantiate(EnemyHpBarPrefab, healthbars.transform);
        hpbar.GetComponent<Slider>().maxValue = health;
        hpbar.GetComponent<Slider>().value = health;
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x, Camera.main.WorldToScreenPoint(transform.position).y + 75f);
        hpbar.GetComponent<Slider>().value = health;

        if (health <= 0)
        {
            Destroy(hpbar);
        }
    }

    public void EnemyTurn()
    {
        if (PlayerManager.instance.PlayerClass == PlayerManager.Class.Rogue)
        {
            gameObject.GetComponent<Enemy>().attack = gameObject.GetComponent<Enemy>().baseatk - BattleManager.instance.confiscation;
            if (gameObject.GetComponent<Enemy>().attack <= 0)
            {
                gameObject.GetComponent<Enemy>().attack = 1;
            }
        }

        switch (enemyType)
        {
            case EnemyType.Melee:
                if (playermovetile.instance.IsPlayerOneTileAway(gameObject))
                {
                    PlayerManager.instance.health -= gameObject.GetComponent<Enemy>().attack;
                    BattleManager.instance.deficiency += Mathf.FloorToInt(gameObject.GetComponent<Enemy>().attack / 3);
                }
                else
                {
                    playermovetile.instance.MoveTowardsPlayer(gameObject);
                }
                break;
            case EnemyType.Ranged:

                break;
            case EnemyType.Healer:
                break;
            default:
                break;
        }
    }
}

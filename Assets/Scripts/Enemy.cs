using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public int maxHp;
    public int attack = 15;
    public int baseatk;
    public Vector3Int startingPosition;
    GameObject hpbar;
    TextMeshProUGUI hptxt;
    int c = 0;

    public GameObject EnemyHpBarPrefab;
    public GameObject ProjectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        baseatk = attack;
        maxHp = health;
        GameObject healthbars = GameObject.Find("healthbars");
        hpbar = Instantiate(EnemyHpBarPrefab, healthbars.transform);
        hpbar.GetComponent<Slider>().maxValue = health;
        hpbar.GetComponent<Slider>().value = health;
        hptxt = hpbar.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.transform.position = new Vector3(Camera.main.WorldToScreenPoint(transform.position).x, Camera.main.WorldToScreenPoint(transform.position).y + 75f);
        hpbar.GetComponent<Slider>().value = health;
        hptxt.text = health.ToString();

        if (health <= 0)
        {
            Destroy(hpbar);
        }
    }

    public void EnemyTurn()
    {
        if (BattleManager.instance.dicerolled)
        {
            Debug.Log(BattleManager.instance.PrevDiceRoll);
            BattleManager.instance.Dice.sprite = BattleManager.instance.DiceFaces[BattleManager.instance.PrevDiceRoll];
            BattleManager.instance.dicerolled = false;
        }

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
                if (playermovetile.instance.IsPlayer_X_TileAway(gameObject, 1))
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
                switch (c)
                {
                    case 0: //move away from player
                        if (playermovetile.instance.IsPlayer_X_TileAway(gameObject, 1)) //if enemy already far away, it attack
                        {
                            playermovetile.instance.MoveAwayFromPlayer(gameObject);
                        }
                        else
                        {
                            attack = Mathf.FloorToInt(playermovetile.instance.DistanceFromPlayer(gameObject)) + baseatk;
                            BattleManager.instance.deficiency += Mathf.FloorToInt(gameObject.GetComponent<Enemy>().attack / 3);
                            ThrowProjectile(ProjectilePrefab, attack);
                        }
                        c = 1;
                        break;
                    case 1: //attack player
                        attack = Mathf.FloorToInt(playermovetile.instance.DistanceFromPlayer(gameObject)) + baseatk;
                        BattleManager.instance.deficiency += Mathf.FloorToInt(gameObject.GetComponent<Enemy>().attack / 3);
                        ThrowProjectile(ProjectilePrefab, attack);
                        c = 0;
                        break;
                    default:
                        break;
                }
                break;
            case EnemyType.Healer:
                switch (c)
                {
                    case 0: //move away from player
                        if (playermovetile.instance.IsPlayer_X_TileAway(gameObject, 1)) //if enemy already far away, it heal
                        {
                            playermovetile.instance.MoveAwayFromPlayer(gameObject);
                        }
                        else
                        {
                            foreach (GameObject e in playermovetile.instance.enemies)
                            {
                                e.GetComponent<Enemy>().health += attack;
                                if (e.GetComponent<Enemy>().health > e.GetComponent<Enemy>().maxHp)
                                {
                                    e.GetComponent<Enemy>().health = e.GetComponent<Enemy>().maxHp;
                                }
                            }
                        }
                        c = 1;
                        break;
                    case 1: //healing for everyone
                        foreach (GameObject e in playermovetile.instance.enemies)
                        {
                            e.GetComponent<Enemy>().health += attack;
                            if (e.GetComponent<Enemy>().health > e.GetComponent<Enemy>().maxHp)
                            {
                                e.GetComponent<Enemy>().health = e.GetComponent<Enemy>().maxHp;
                            }
                        }
                        c = 0;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }

        StartCoroutine(BattleManager.instance.DelayBeforePlayerTurn(1.5f));
    }

    void ThrowProjectile(GameObject projectilePrefab, int dmg)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        // Instantiate the ball prefab at the enemy's position
        GameObject projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().atk = dmg;

        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = (PlayerManager.instance.transform.position - gameObject.transform.position).normalized;

        // Set the velocity of the ball to shoot it towards the player
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            projectileRb.velocity = directionToPlayer * 7.5f;
        }
    }
}

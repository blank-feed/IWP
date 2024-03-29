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
        Healer,
        Boss
    }

    public EnemyType enemyType;
    public int health = 100;
    public int maxHp;
    public int attack = 15;
    public int baseatk;
    public Vector3Int startingPosition;
    public GameObject hpbar;
    TextMeshProUGUI hptxt;
    int c = 0;
    public bool GivenBloodlust = false;
    public bool GivenExp = false;

    public GameObject EnemyHpBarPrefab;
    public GameObject ProjectilePrefab;
    public GameObject DmgIndicatorPrefab;

    public bool crippled = false;

    bool boss_moveback = false;

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
    }

    public void EnemyTurn()
    {
        if (gameObject.transform.position == new Vector3(-10, -10))
        {
            return;
        }

        if (BattleManager.instance.dicerolled)
        {
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
                    GameObject go = Instantiate(DmgIndicatorPrefab);
                    go.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    go.transform.position = new Vector3(Camera.main.WorldToScreenPoint(PlayerManager.instance.transform.position).x, Camera.main.WorldToScreenPoint(PlayerManager.instance.transform.position).y + 75);
                    go.GetComponent<DamageNumberIndicator>().baseText.text = "-" + gameObject.GetComponent<Enemy>().attack.ToString();
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
                            ThrowProjectile(ProjectilePrefab, attack, PlayerManager.instance.transform.position);
                        }
                        c = 1;
                        break;
                    case 1: //attack player
                        attack = Mathf.FloorToInt(playermovetile.instance.DistanceFromPlayer(gameObject)) + baseatk;
                        BattleManager.instance.deficiency += Mathf.FloorToInt(gameObject.GetComponent<Enemy>().attack / 3);
                        ThrowProjectile(ProjectilePrefab, attack, PlayerManager.instance.transform.position);
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
            case EnemyType.Boss:
                if (health > 100 && !boss_moveback)
                {
                    if (playermovetile.instance.IsPlayer_X_TileAway(gameObject, 1))
                    {
                        PlayerManager.instance.health -= gameObject.GetComponent<Enemy>().attack;
                        GameObject go = Instantiate(DmgIndicatorPrefab);
                        go.transform.SetParent(GameObject.Find("Canvas").transform, false);
                        go.transform.position = new Vector3(Camera.main.WorldToScreenPoint(PlayerManager.instance.transform.position).x, Camera.main.WorldToScreenPoint(PlayerManager.instance.transform.position).y + 75);
                        go.GetComponent<DamageNumberIndicator>().baseText.text = "-" + gameObject.GetComponent<Enemy>().attack.ToString();
                        BattleManager.instance.deficiency += Mathf.FloorToInt(gameObject.GetComponent<Enemy>().attack / 3);
                    }
                    else
                    {
                        playermovetile.instance.MoveTowardsPlayer(gameObject);
                    }
                }
                if (health <= 100)
                {
                    if (!boss_moveback)
                    {
                        Vector3 targetPos = new Vector3(gameObject.transform.position.x + 3, gameObject.transform.position.y);
                        playermovetile.instance.LeanTweenIt(gameObject, targetPos, 1f);
                        boss_moveback = true;
                    }
                }
                if (boss_moveback)
                {
                    BattleManager.instance.deficiency += Mathf.FloorToInt(gameObject.GetComponent<Enemy>().attack / 3);
                    ThrowProjectile(ProjectilePrefab, attack, PlayerManager.instance.transform.position);
                }
                break;
            default:
                break;
        }

        if (gameObject.activeSelf)
        {
            StartCoroutine(BattleManager.instance.DelayBeforePlayerTurn(1.5f));
        }
    }

    void ThrowProjectile(GameObject projectilePrefab, int dmg, Vector3 pos)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        // Instantiate the ball prefab at the enemy's position
        GameObject projectile = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().atk = dmg;

        // Calculate the direction from the enemy to the player
        Vector3 directionToTarget = (pos - gameObject.transform.position).normalized;

        // Set the velocity of the ball to shoot it towards the player
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if (projectileRb != null)
        {
            projectileRb.velocity = directionToTarget * 7.5f;
        }
    }
}

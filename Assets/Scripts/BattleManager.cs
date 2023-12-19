using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattlingState
{
    start,
    playerturn,
    move,
    Select_attack,
    Aim_attack,
    Use_attack,
    enemyturn,
    win,
    lose,
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public GameObject Menu_AoM;
    public GameObject Menu_Attack;

    public BattlingState bs;
    public bool moveable = false;
    public bool shootable = false;

    public TextMeshProUGUI playerhpinfo;
    public TextMeshProUGUI enemyhpinfo;

    public int enemyhp = 100;

    private bool delayTriggered = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bs = BattlingState.start;
    }

    // Update is called once per frame
    void Update()
    {
        playerhpinfo.text = "player hp : " + PlayerManager.instance.health;
        enemyhpinfo.text = "enemy hp : " + enemyhp;

        switch (bs)
        {
            case BattlingState.start:
                SetMenuActive(0);
                bs = BattlingState.playerturn;
                break;
            case BattlingState.playerturn:
                SetMenuActive(1);
                delayTriggered = false;
                break;
            case BattlingState.move:
                SetMenuActive(0);
                moveable = true;
                break;
            case BattlingState.Select_attack:
                SetMenuActive(2);
                break;
            case BattlingState.enemyturn:
                SetMenuActive(0);
                if (!delayTriggered)
                {
                    StartCoroutine(DelayedAction());
                    delayTriggered = true;
                }
                break;
            case BattlingState.win:

                break;
            case BattlingState.lose:

                break;
            default:
                break;
        }
    }

    public void Move()
    {
        bs = BattlingState.move;
    }

    public void Attack()
    {
        bs = BattlingState.Select_attack;
    }

    public void UseSkill(int skill)
    {
        SetMenuActive(0);
        switch (skill)
        {
            case 1:
                shootable = true;
                break;
            case 2:
                enemyhp -= 10;
                bs = BattlingState.enemyturn;
                break;
            case 3:
                PlayerManager.instance.health += 10;
                if (PlayerManager.instance.health > PlayerManager.instance.maxHealth)
                {
                    PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                }
                bs = BattlingState.enemyturn;
                break;
            default:
                break;
        }
    }

    public int RollDie(int maxDieCount)
    {
        int NumRolled = Random.Range(1, maxDieCount + 1);

        return NumRolled;
    }

    public void SetMenuActive(int ActiveMenu)
    {
        Menu_AoM.SetActive(false);
        Menu_Attack.SetActive(false);

        switch (ActiveMenu)
        {
            case 1:
                Menu_AoM.SetActive(true);
                break;

            case 2:
                Menu_Attack.SetActive(true);
                break;

            default:
                break;
        }
    }

    void EnemyTurn()
    {
        if (playermovetile.instance.enemy == null)
        {
            return;
        }

        if (playermovetile.instance.IsPlayerOneTileAway())
        {
            PlayerManager.instance.health -= 15;
        }
        else
        {
            playermovetile.instance.MoveTowardsPlayer();
        }
        bs = BattlingState.playerturn;
    }

    IEnumerator DelayedAction()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1.0f);
        EnemyTurn();
    }
}

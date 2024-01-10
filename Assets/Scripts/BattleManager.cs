using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    //can move
    public bool moveable = false;

    //can melee
    public bool can_melee = false;

    public TextMeshProUGUI playerhpinfo;
    public TextMeshProUGUI enemyhpinfo;

    public TextMeshProUGUI Skill1_Name;
    public TextMeshProUGUI Skill2_Name;
    public TextMeshProUGUI Skill3_Name;

    public int enemyhp = 100;

    private bool delayTriggered = false;

    public int Damage;

    public int momentum = 0;

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
        Skill1_Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S1);
        Skill2_Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S2);
        Skill3_Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S3);
        momentum = 0;
        bs = BattlingState.start;
    }

    // Update is called once per frame
    void Update()
    {
        playerhpinfo.text = "player hp : " + PlayerManager.instance.health;
        enemyhpinfo.text = "enemy hp : " + enemyhp;

        if (enemyhp <= 0)
        {
            Destroy(playermovetile.instance.enemy);
            StartCoroutine(EndBattle());
        }

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
                //can_melee = true;
                PlayerSkills.instance.UseSkill(PlayerSkills.instance.S1);
                break;
            case 2:
                //enemyhp -= 100; //10
                //bs = BattlingState.enemyturn;
                PlayerSkills.instance.UseSkill(PlayerSkills.instance.S2);
                break;
            case 3:
                //PlayerManager.instance.health += 10;
                //if (PlayerManager.instance.health > PlayerManager.instance.maxHealth)
                //{
                //    PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                //}
                //bs = BattlingState.enemyturn;
                PlayerSkills.instance.UseSkill(PlayerSkills.instance.S3);
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

    IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainLobby");
    }
}

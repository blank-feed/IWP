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

    private Camera MainCamera;

    public GameObject Menu_AoM;
    public GameObject Menu_Attack;
    public GameObject MoveCount_UI;
    public GameObject Momentum_UI;
    public GameObject BackIndicator;
    public GameObject stopmove_UI;

    public TextMeshProUGUI MoveCount_Text;
    public TextMeshProUGUI Momentum_Text;

    public BattlingState bs;

    public Image S1_Img;
    public Image S2_Img;
    public Image S3_Img;

    //can move
    public bool moveable = false;

    //can melee
    public bool can_melee = false;

    //can dash
    public bool can_dash = false;

    //can shoot
    public bool can_shoot = false;

    public TextMeshProUGUI playerhpinfo;
    public TextMeshProUGUI enemyhpinfo;

    public TextMeshProUGUI Skill1_Name;
    public TextMeshProUGUI Skill2_Name;
    public TextMeshProUGUI Skill3_Name;

    public int enemyhp = 100;

    private bool delayTriggered = false;

    public int Damage;

    public int momentum = 0;

    public int MoveCount;

    public Vector3 Ori_Pos;

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
        MainCamera = Camera.main;
        Skill1_Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S1);
        Skill2_Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S2);
        Skill3_Name.text = PlayerSkills.instance.ProcessSkillName(PlayerSkills.instance.S3);
        S1_Img.sprite = PlayerSkills.instance.S1_Img;
        S2_Img.sprite = PlayerSkills.instance.S2_Img;
        S3_Img.sprite = PlayerSkills.instance.S3_Img;
        bs = BattlingState.start;
        momentum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Momentum_UI != null)
        {
            Momentum_Text.text = momentum.ToString();
        }

        //if player at the very bottom
        //if (PlayerManager.instance.transform.position.y == -4.5)
        //{
        //    Menu_AoM.transform.position = MainCamera.WorldToScreenPoint(PlayerManager.instance.transform.position);
        //    MoveCount_UI.transform.position = MainCamera.WorldToScreenPoint(PlayerManager.instance.transform.position);
        //}
        //if player near the top
        /*else */if (PlayerManager.instance.transform.position.y >= 3.5)
        {
            MoveCount_UI.transform.position = MainCamera.WorldToScreenPoint(new Vector3(PlayerManager.instance.transform.position.x, PlayerManager.instance.transform.position.y - 3));
            Menu_AoM.transform.position = MainCamera.WorldToScreenPoint(PlayerManager.instance.transform.position);
        }
        else
        {
            Menu_AoM.transform.position = MainCamera.WorldToScreenPoint(PlayerManager.instance.transform.position);
            MoveCount_UI.transform.position = MainCamera.WorldToScreenPoint(PlayerManager.instance.transform.position);
        }

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
                MoveCount_UI.SetActive(false);
                bs = BattlingState.playerturn;
                break;
            case BattlingState.playerturn:
                SetMenuActive(1);
                delayTriggered = false;
                break;
            case BattlingState.move:
                SetMenuActive(3);
                moveable = true;
                break;
            case BattlingState.Select_attack:
                SetMenuActive(2);
                break;
            case BattlingState.Aim_attack:
                SetMenuActive(4);
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
        Ori_Pos = PlayerManager.instance.transform.position;
        BackIndicator.SetActive(true);
        MoveCount_UI.SetActive(true);
        if (playermovetile.instance.temp_num == -1)
        {
            MoveCount = RollDie(6);
        }
        else
        {
            MoveCount = playermovetile.instance.temp_num;
        }
        MoveCount_Text.text = MoveCount.ToString();
        playermovetile.instance.temp_num = MoveCount;
        playermovetile.instance.movespaces = 1;
        bs = BattlingState.move;
    }

    public void Attack()
    {
        bs = BattlingState.Select_attack;
    }

    public void UseSkill(int skill)
    {
        SetMenuActive(0);
        bs = BattlingState.Aim_attack;
        switch (skill)
        {
            case 1:
                //can_melee = true;
                PlayerSkills.instance.UseSkill(PlayerSkills.instance.S1);
                if (PlayerSkills.instance.range == PlayerSkills.Range.Melee)
                {
                    playermovetile.instance.ShowHittableSpots(playermovetile.instance.movespaces);
                }
                else if (PlayerSkills.instance.range == PlayerSkills.Range.Ranged)
                {
                    playermovetile.instance.ShowShootableSpots(playermovetile.instance.movespaces);
                }
                break;
            case 2:
                //enemyhp -= 100; //10
                //bs = BattlingState.enemyturn;
                PlayerSkills.instance.UseSkill(PlayerSkills.instance.S2);
                if (PlayerSkills.instance.range == PlayerSkills.Range.Melee)
                {
                    playermovetile.instance.ShowHittableSpots(playermovetile.instance.movespaces);
                }
                else if (PlayerSkills.instance.range == PlayerSkills.Range.Ranged)
                {
                    playermovetile.instance.ShowShootableSpots(playermovetile.instance.movespaces);
                }
                break;
            case 3:
                //PlayerManager.instance.health += 10;
                //if (PlayerManager.instance.health > PlayerManager.instance.maxHealth)
                //{
                //    PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                //}
                //bs = BattlingState.enemyturn;
                PlayerSkills.instance.UseSkill(PlayerSkills.instance.S3);
                if (PlayerSkills.instance.range == PlayerSkills.Range.Melee)
                {
                    playermovetile.instance.ShowHittableSpots(playermovetile.instance.movespaces);
                }
                else if (PlayerSkills.instance.range == PlayerSkills.Range.Ranged)
                {
                    playermovetile.instance.ShowShootableSpots(playermovetile.instance.movespaces);
                }
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
        Momentum_UI.SetActive(false);
        stopmove_UI.SetActive(false);
        BackIndicator.SetActive(false);

        switch (ActiveMenu)
        {
            case 1:
                Menu_AoM.SetActive(true);
                break;

            case 2:
                Menu_Attack.SetActive(true);
                BackIndicator.SetActive(true);
                if (PlayerManager.instance.PlayerClass == PlayerManager.Class.Fighter)
                {
                    Momentum_UI.SetActive(true);
                }
                break;

            case 3:
                BackIndicator.SetActive(true);
                stopmove_UI.SetActive(true);
                break;
            case 4:
                BackIndicator.SetActive(true);
                break;

            default:
                break;
        }
    }

    void EnemyTurn()
    {
        PlayerSkills.instance.repeat = -1;
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

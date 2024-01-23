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
    public GameObject Boon_UI;
    public GameObject BackIndicator;
    public GameObject stopmove_UI;

    public TextMeshProUGUI MoveCount_Text;

    public TextMeshProUGUI Boon_Text;

    public BattlingState bs;

    public Image S1_Img;
    public Image S2_Img;
    public Image S3_Img;
    public Image BoonImg;

    public Sprite HighShotReadyImg;

    public Sprite[] DiceFaces;
    public Image Dice;

    //can move
    public bool moveable = false;

    //can melee
    public bool can_melee = false;

    //can dash
    public bool can_dash = false;

    //can shoot
    public bool can_shoot = false;

    public TextMeshProUGUI playerhpinfo;

    private bool delayTriggered = false;
    public bool dicerolled = false;

    public int Damage;
    public int PrevDiceRoll;

    //boons
    //paladin
    public int deficiency = 0;

    //rogue
    public int confiscation = 0;

    //sorcerer
    public int amplification = 0;

    //archer
    public int cripple = 0;

    //fighter
    public int bloodlust = 0;

    //druid
    public int Dragon_Pals = 0;

    public bool LifeSteal = false;
    public bool CripplingAtk = false;
    public bool VoidJumped = false;
    public int HighShotCount = 0;
    public int MoveCount;
    public int ManaToDeduct;
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
        S1_Img.sprite = PlayerSkills.instance.S1_Img;
        S2_Img.sprite = PlayerSkills.instance.S2_Img;
        S3_Img.sprite = PlayerSkills.instance.S3_Img;
        switch (PlayerManager.instance.PlayerClass)
        {
            case PlayerManager.Class.Paladin:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[0];
                break;
            case PlayerManager.Class.Rogue:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[1];
                break;
            case PlayerManager.Class.Sorcerer:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[2];
                break;
            case PlayerManager.Class.Ranger:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[3];
                break;
            case PlayerManager.Class.Fighter:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[4];
                break;
            case PlayerManager.Class.Druid:
                BoonImg.sprite = PlayerSkills.instance.Boon_Img[5];
                break;
            default:
                break;
        }
        bs = BattlingState.start;
    }

    // Update is called once per frame
    void Update()
    {
        if (Boon_UI != null)
        {
            string boonCount = "";
            switch (PlayerManager.instance.PlayerClass)
            {
                case PlayerManager.Class.Paladin:
                    boonCount = deficiency.ToString();
                    break;
                case PlayerManager.Class.Rogue:
                    boonCount = confiscation.ToString();
                    break;
                case PlayerManager.Class.Sorcerer:
                    boonCount = amplification.ToString();
                    break;
                case PlayerManager.Class.Ranger:
                    boonCount = cripple.ToString();
                    break;
                case PlayerManager.Class.Fighter:
                    boonCount = bloodlust.ToString();
                    break;
                case PlayerManager.Class.Druid:
                    boonCount = Dragon_Pals.ToString();
                    break;
                default:
                    break;
            }
            Boon_Text.text = boonCount.ToString();
        }

        if (PlayerManager.instance.PlayerClass == PlayerManager.Class.Ranger && HighShotCount >= 2)
        {
            S2_Img.sprite = HighShotReadyImg;
        }
        else
        {
            S2_Img.sprite = PlayerSkills.instance.S2_Img;
        }

        if (PlayerManager.instance.transform.position.y >= 3.5)
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

        if (PlayerManager.instance.health <= 0)
        {
            StartCoroutine(EndBattle());
        }

        if (AreAllEnemiesInactive())
        {
            StartCoroutine(EndBattle());
        }

        foreach (GameObject e in playermovetile.instance.enemies)
        {
            if (e.GetComponent<Enemy>().health <= 0)
            {
                if (PlayerManager.instance.PlayerClass == PlayerManager.Class.Fighter && !e.GetComponent<Enemy>().GivenBloodlust)
                {
                    bloodlust++;
                    e.GetComponent<Enemy>().GivenBloodlust = true;
                }
                e.transform.position = new Vector3(-10, -10);
                Destroy(e.GetComponent<Enemy>().hpbar);
                e.SetActive(false);
            }
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
        int NumRolled = Random.Range(1, maxDieCount);
        PrevDiceRoll = NumRolled - 1;
        dicerolled = true;

        return NumRolled;
    }

    public void SetMenuActive(int ActiveMenu)
    {
        Menu_AoM.SetActive(false);
        Menu_Attack.SetActive(false);
        Boon_UI.SetActive(false);
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
                Boon_UI.SetActive(true);
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

    bool AreAllEnemiesInactive()
    {
        foreach (GameObject enemy in playermovetile.instance.enemies)
        {
            if (enemy != null && enemy.activeSelf)
            {
                return false;
            }
        }

        return true;
    }
    IEnumerator DelayedAction()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1.0f);
        foreach (GameObject e in playermovetile.instance.enemies)
        {
            e.GetComponent<Enemy>().EnemyTurn();
        }
    }

    public IEnumerator DelayBeforePlayerTurn(float x)
    {
        // Wait for 1 second
        yield return new WaitForSeconds(x);
        bs = BattlingState.playerturn;
    }

    IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainLobby");
    }
}

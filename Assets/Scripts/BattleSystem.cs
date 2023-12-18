using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState
{
    start,
    playerturn,
    enemyturn,
    win,
    lose,
}

public class BattleSystem : MonoBehaviour
{
    public GameObject BattleObj;

    public Transform playerArea;
    public Transform enemyArea;

    public BattleState state;

    int Die_Num;

    [SerializeField]
    List<GameObject> enemy_list = new List<GameObject>();

    [SerializeField]
    List<GameObject> ally_list = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.start;
        SetUpBattle();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BattleState.playerturn:
                PlayerTurn();
                break;
            case BattleState.enemyturn:
                EnemyTurn();
                break;
            case BattleState.win:
                Win();
                break;
            case BattleState.lose:
                Lose();
                break;
            default:
                break;
        }
    }

    void SetUpBattle()
    {
        CreateBattleObj(CombatManager.instance.GetPlayerImg(), playerArea, PlayerManager.instance.maxHealth, 0);
        CreateBattleObj(CombatManager.instance.GetEnemyImg(), enemyArea, CombatManager.instance.GetEnemyHealth(), 1);
        state = BattleState.playerturn;
    }

    void CreateBattleObj(Sprite BattleObj_Img, Transform area, int maxHp, int Type)
    {
        if (Type == 0)
        {
            BattleObj.transform.Find("Sprite").GetComponent<Image>().sprite = BattleObj_Img;
            BattleObj.transform.Find("Slider").GetComponent<Slider>().maxValue = maxHp;
            BattleObj.transform.Find("Slider").GetComponent<Slider>().value = PlayerManager.instance.health;
            Instantiate(BattleObj, area);
            ally_list.Add(GameObject.Find("playerArea").transform.GetChild(0).gameObject);
        }
        else
        {
            BattleObj.transform.Find("Sprite").GetComponent<Image>().sprite = BattleObj_Img;
            BattleObj.transform.Find("Slider").GetComponent<Slider>().maxValue = maxHp;
            BattleObj.transform.Find("Slider").GetComponent<Slider>().value = maxHp;
            Instantiate(BattleObj, area);
            enemy_list.Add(GameObject.Find("enemyArea").transform.GetChild(0).gameObject);
        }
    }

    void PlayerTurn()
    {
        bool atk = false;
        switch (Input.inputString.ToUpper())
        {
            case "Q":
                atk = true;
                break;
            case "W":
                atk = true;
                break;
            case "E":
                atk = true;
                break;
            default:
                break;
        }

        if (atk)
        {
            RollDie();
            CombatManager.instance.SetHealth(Die_Num, "enemy");
            enemy_list[0].GetComponentInChildren<Slider>().value = CombatManager.instance.GetEnemyHealth();
            if (CombatManager.instance.GetEnemyHealth() <= 0)
            {
                atk = false;
                state = BattleState.win;
            }
            else
            {
                atk = false;
                state = BattleState.enemyturn;
            }
        }
    }

    void EnemyTurn()
    {
        RollDie();
        CombatManager.instance.SetHealth(Die_Num, "player");
        ally_list[0].GetComponentInChildren<Slider>().value = CombatManager.instance.GetPlayerHealth();
        if (CombatManager.instance.GetPlayerHealth() <= 0)
        {
            state = BattleState.lose;
        }
        else
        {
            state = BattleState.playerturn;
        }
    }

    void RollDie()
    {
        Die_Num = Random.Range(1, 7);
    }

    void Win()
    {
        SceneManager.LoadScene("MainLobby");
    }

    void Lose()
    {
        SceneManager.LoadScene("MainLobby");
    }
}

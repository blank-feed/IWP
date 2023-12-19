using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BattlingState
{
    start,
    playerturn,
    move,
    attack,
    enemyturn,
    win,
    lose,
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public GameObject Menu_AoM;

    public BattlingState bs;
    public bool moveable = false;

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
        switch (bs)
        {
            case BattlingState.start:
                Menu_AoM.SetActive(false);
                bs = BattlingState.playerturn;
                break;
            case BattlingState.playerturn:
                Menu_AoM.SetActive(true);
                break;
            case BattlingState.move:
                Menu_AoM.SetActive(false);
                moveable = true;
                break;
            case BattlingState.attack:
                Menu_AoM.SetActive(false);
                break;
            case BattlingState.enemyturn:

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
        bs = BattlingState.attack;
    }

}

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
                bs = BattlingState.playerturn;
                break;
            case BattlingState.playerturn:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    bs = BattlingState.move;
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    bs = BattlingState.attack;
                }
                break;
            case BattlingState.move:
                moveable = true;
                break;
            case BattlingState.attack:

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
}

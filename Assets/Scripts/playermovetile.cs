using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playermovetile : MonoBehaviour
{
    public static playermovetile instance;

    GameObject player;
    public GameObject enemy;
    Tilemap tmap;
    private Vector3 targetPosition;
    public int temp_num = -1;
    public GameObject MoveableArrowsPrefab;
    bool shown = false;

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
        player = GameObject.FindGameObjectWithTag("Player");
        tmap = GameObject.Find("Tilemap_Base").GetComponent<Tilemap>();
        player.transform.localScale = new Vector3(.5f, .5f, 1);
        player.transform.position = tmap.GetCellCenterWorld(new Vector3Int(-4, 0));
        enemy.transform.position = tmap.GetCellCenterWorld(new Vector3Int(4, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.instance.moveable)
        {
            if (!shown)
            {
                ShowMoveableSpots();
            }
        }
        else
        {
            DestroyObjectsWithName("BouncingArrow");
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (BattleManager.instance.moveable)
            {
                if (BattleManager.instance.MoveCount != 0)
                {
                    targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    TileBase destinationTile = GetDestination(targetPosition);
                    if (destinationTile != null && tmap.WorldToCell(targetPosition) != tmap.WorldToCell(enemy.transform.position))
                    {
                        Vector3Int destinationCell = tmap.WorldToCell(targetPosition);
                        player.transform.position = tmap.GetCellCenterWorld(destinationCell);
                        DestroyObjectsWithName("BouncingArrow");
                        BattleManager.instance.momentum--;
                        BattleManager.instance.MoveCount--;
                        BattleManager.instance.MoveCount_Text.text = BattleManager.instance.MoveCount.ToString();
                    }
                    if (BattleManager.instance.MoveCount == 0)
                    {
                        temp_num = -1;
                        BattleManager.instance.MoveCount_UI.SetActive(false);
                        BattleManager.instance.moveable = false;
                        BattleManager.instance.bs = BattlingState.enemyturn;
                    }
                }
            }

            if (BattleManager.instance.can_melee)
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                TileBase destinationTile = GetDestination(targetPosition);
                if (destinationTile != null)
                {
                    Vector3Int destinationCell = tmap.WorldToCell(targetPosition);
                    if (tmap.GetCellCenterWorld(destinationCell) == tmap.GetCellCenterWorld(tmap.WorldToCell(enemy.transform.position)))
                    {
                        BattleManager.instance.enemyhp -= BattleManager.instance.Damage;
                        BattleManager.instance.momentum++;
                    }
                    BattleManager.instance.can_melee = false;
                    BattleManager.instance.bs = BattlingState.enemyturn;
                }
            }
        }

        //right click to cancel
        if (Input.GetMouseButtonDown(1))
        {
            if (BattleManager.instance.moveable)
            {
                BattleManager.instance.moveable = false;
                BattleManager.instance.MoveCount_UI.SetActive(false);
                PlayerManager.instance.transform.position = BattleManager.instance.Ori_Pos;
                BattleManager.instance.bs = BattlingState.playerturn;
            }

            if (BattleManager.instance.can_melee)
            {
                BattleManager.instance.can_melee = false;
                BattleManager.instance.bs = BattlingState.Select_attack;
            }

            if (BattleManager.instance.bs == BattlingState.Select_attack)
            {
                BattleManager.instance.bs = BattlingState.playerturn;
            }
        }

        //end movement early
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (BattleManager.instance.moveable)
            {
                BattleManager.instance.moveable = false;
                BattleManager.instance.MoveCount_UI.SetActive(false);
                temp_num = -1;
                BattleManager.instance.bs = BattlingState.enemyturn;
            }
        }
    }

    public TileBase GetDestination(Vector3 worldPos)
    {
        Vector3Int Temp_playerGridPos = tmap.WorldToCell(player.transform.position);
        Vector2Int playerGridPos = new Vector2Int(Temp_playerGridPos.x, Temp_playerGridPos.y);
        Vector3Int mouseGridPos = tmap.WorldToCell(worldPos);

        Vector3Int Top = new Vector3Int(playerGridPos.x, playerGridPos.y + 1);
        Vector3Int Down = new Vector3Int(playerGridPos.x, playerGridPos.y - 1);
        Vector3Int Left = new Vector3Int(playerGridPos.x - 1, playerGridPos.y);
        Vector3Int Right = new Vector3Int(playerGridPos.x + 1, playerGridPos.y);

        if (Top == mouseGridPos)
        {
            return tmap.GetTile(Top);
        }
        if (Down == mouseGridPos)
        {
            return tmap.GetTile(Down);
        }
        if (Left == mouseGridPos)
        {
            return tmap.GetTile(Left);
        }
        if (Right == mouseGridPos)
        {
            return tmap.GetTile(Right);
        }

        return null;
    }

    void ShowMoveableSpots()
    {
        Vector3Int Temp_playerGridPos = tmap.WorldToCell(player.transform.position);
        Vector2Int playerGridPos = new Vector2Int(Temp_playerGridPos.x, Temp_playerGridPos.y);

        Vector3Int T = new Vector3Int(playerGridPos.x, playerGridPos.y + 1);
        Vector3Int D = new Vector3Int(playerGridPos.x, playerGridPos.y - 1);
        Vector3Int L = new Vector3Int(playerGridPos.x - 1, playerGridPos.y);
        Vector3Int R = new Vector3Int(playerGridPos.x + 1, playerGridPos.y);

        Vector3 Top = tmap.GetCellCenterWorld(T);
        Vector3 Down = tmap.GetCellCenterWorld(D);
        Vector3 Left = tmap.GetCellCenterWorld(L);
        Vector3 Right = tmap.GetCellCenterWorld(R);

        Instantiate(MoveableArrowsPrefab, new Vector3(Top.x, Top.y, Top.z), Quaternion.identity);
        Instantiate(MoveableArrowsPrefab, new Vector3(Down.x, Down.y, Down.z), Quaternion.identity);
        Instantiate(MoveableArrowsPrefab, new Vector3(Left.x, Left.y, Left.z), Quaternion.identity);
        Instantiate(MoveableArrowsPrefab, new Vector3(Right.x, Right.y, Right.z), Quaternion.identity);

        shown = true;
    }

    void DestroyObjectsWithName(string tagToFind)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tagToFind);

        if (gameObjects == null || gameObjects.Length == 0)
        {
            return;
        }

        foreach (GameObject obj in gameObjects)
        {
            Destroy(obj);
        }

        shown = false;
    }

    public bool IsPlayerOneTileAway()
    {
        Vector3Int playerCell = tmap.WorldToCell(player.transform.position);
        Vector3Int enemyCell = tmap.WorldToCell(enemy.transform.position);

        // Check if the player is exactly one tile above, below, to the left, or to the right of the enemy
        return Mathf.Abs(playerCell.x - enemyCell.x) == 1 && playerCell.y == enemyCell.y
            || Mathf.Abs(playerCell.y - enemyCell.y) == 1 && playerCell.x == enemyCell.x;
    }

    public void MoveTowardsPlayer()
    {
        Vector3Int playerCell = tmap.WorldToCell(player.transform.position);
        Vector3Int enemyCell = tmap.WorldToCell(enemy.transform.position);

        Vector3 destinationCell;

        if (playerCell.x < enemyCell.x)
        {
            // Player is to the left of the enemy
            destinationCell = tmap.GetCellCenterWorld(new Vector3Int(enemyCell.x - 1, enemyCell.y, enemyCell.z));
        }
        else if (playerCell.x > enemyCell.x)
        {
            // Player is to the right of the enemy
            destinationCell = tmap.GetCellCenterWorld(new Vector3Int(enemyCell.x + 1, enemyCell.y, enemyCell.z));
        }
        else if (playerCell.y < enemyCell.y)
        {
            // Player is below the enemy
            destinationCell = tmap.GetCellCenterWorld(new Vector3Int(enemyCell.x, enemyCell.y - 1, enemyCell.z));
        }
        else
        {
            // Player is above the enemy
            destinationCell = tmap.GetCellCenterWorld(new Vector3Int(enemyCell.x, enemyCell.y + 1, enemyCell.z));
        }

        enemy.transform.position = destinationCell;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playermovetile : MonoBehaviour
{
    public static playermovetile instance;

    GameObject player;
    //public GameObject enemy;
    public GameObject[] enemies;
    Tilemap tmap;
    private Vector3 targetPosition;
    public int temp_num = -1;
    public GameObject MoveableArrowsPrefab;
    public GameObject HittablePrefab;
    public GameObject CrosshairPrefab;
    public bool shown = false;
    public int movespaces = 0;
    bool back = false;
    public bool isMoving = false;

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
        foreach (GameObject e in enemies)
        {
            e.transform.position = tmap.GetCellCenterWorld(e.GetComponent<Enemy>().startingPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.instance.moveable)
        {
            if (!shown)
            {
                ShowMoveableSpots(movespaces);
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
                    //movespaces = 1;
                    TileBase destinationTile = GetDestination(targetPosition, movespaces);
                    if (destinationTile != null)
                    {
                        foreach (GameObject e in enemies)
                        {
                            if (tmap.WorldToCell(targetPosition) == tmap.WorldToCell(e.transform.position))
                            {
                                return;
                            }
                        }
                        Vector3Int destinationCell = tmap.WorldToCell(targetPosition);
                        LeanTweenIt(player, tmap.GetCellCenterWorld(destinationCell), 1f);
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
                TileBase destinationTile = GetDestination(targetPosition, movespaces);
                if (destinationTile != null)
                {
                    Vector3Int destinationCell = tmap.WorldToCell(targetPosition);
                    foreach (GameObject e in enemies)
                    {
                        if (tmap.GetCellCenterWorld(destinationCell) == tmap.GetCellCenterWorld(tmap.WorldToCell(e.transform.position)))
                        {
                            e.GetComponent<Enemy>().health -= BattleManager.instance.Damage;
                            BattleManager.instance.confiscation++;
                        }
                    }
                    PlayerManager.instance.mana -= BattleManager.instance.ManaToDeduct;
                    BattleManager.instance.ManaToDeduct = 0;
                    BattleManager.instance.can_melee = false;
                    DestroyObjectsWithName("swordcross");
                    BattleManager.instance.bs = BattlingState.enemyturn;
                }
            }

            if (BattleManager.instance.can_dash)
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                TileBase destinationTile = GetDestination(targetPosition, movespaces);
                if (destinationTile != null)
                {
                    Vector3Int destinationCell = tmap.WorldToCell(targetPosition);
                    foreach (GameObject e in enemies)
                    {
                        if (HitByDash(tmap.GetCellCenterWorld(tmap.WorldToCell(player.transform.position)), tmap.GetCellCenterWorld(destinationCell), tmap.GetCellCenterWorld(tmap.WorldToCell(e.transform.position))))
                        {
                            e.GetComponent<Enemy>().health -= BattleManager.instance.Damage;
                        }
                    }
                    LeanTweenIt(player, tmap.GetCellCenterWorld(destinationCell), 1);
                    PlayerManager.instance.mana -= BattleManager.instance.ManaToDeduct;
                    BattleManager.instance.ManaToDeduct = 0;
                    BattleManager.instance.can_dash = false;
                    DestroyObjectsWithName("swordcross");
                    DestroyObjectsWithName("crosshair");
                    BattleManager.instance.bs = BattlingState.enemyturn;
                }
            }

            if (BattleManager.instance.can_shoot)
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                TileBase destinationTile = GetDestination(targetPosition, movespaces);
                if (destinationTile != null)
                {
                    Vector3Int destinationCell = tmap.WorldToCell(targetPosition);
                    foreach (GameObject e in enemies)
                    {
                        if (HitByDash(tmap.GetCellCenterWorld(tmap.WorldToCell(player.transform.position)), tmap.GetCellCenterWorld(destinationCell), tmap.GetCellCenterWorld(tmap.WorldToCell(e.transform.position))))
                        {
                            e.GetComponent<Enemy>().health -= BattleManager.instance.Damage;
                            if (BattleManager.instance.LifeSteal)
                            {
                                PlayerManager.instance.health += Mathf.FloorToInt(BattleManager.instance.Damage / 2);
                                if (PlayerManager.instance.health > PlayerManager.instance.maxHealth)
                                {
                                    PlayerManager.instance.health = PlayerManager.instance.maxHealth;
                                }
                                BattleManager.instance.LifeSteal = false;
                            }
                        }
                    }
                    PlayerManager.instance.mana -= BattleManager.instance.ManaToDeduct;
                    BattleManager.instance.ManaToDeduct = 0;
                    BattleManager.instance.can_shoot = false;
                    DestroyObjectsWithName("crosshair");
                    BattleManager.instance.bs = BattlingState.enemyturn;
                }
            }
        }

        //right click to cancel
        if (Input.GetMouseButtonDown(1) || back)
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
                DestroyObjectsWithName("swordcross");
                BattleManager.instance.bs = BattlingState.Select_attack;
            }

            if (BattleManager.instance.can_dash)
            {
                BattleManager.instance.can_dash = false;
                DestroyObjectsWithName("swordcross");
                BattleManager.instance.bs = BattlingState.Select_attack;
            }

            if (BattleManager.instance.can_shoot)
            {
                BattleManager.instance.can_shoot = false;
                DestroyObjectsWithName("crosshair");
                BattleManager.instance.bs = BattlingState.Select_attack;
            }

            if (BattleManager.instance.bs == BattlingState.Select_attack)
            {
                BattleManager.instance.bs = BattlingState.playerturn;
            }

            back = false;
        }
    }

    public void PressBack()
    {
        if (isMoving)
        {
            return;
        }
        back = true;
    }

    //end movement early
    public void StopEarly()
    {
        if (BattleManager.instance.moveable)
        {
            BattleManager.instance.moveable = false;
            BattleManager.instance.MoveCount_UI.SetActive(false);
            temp_num = -1;
            BattleManager.instance.bs = BattlingState.enemyturn;
        }
    }

    public TileBase GetDestination(Vector3 worldPos, int spaces)
    {
        Vector3Int Temp_playerGridPos = tmap.WorldToCell(player.transform.position);
        Vector2Int playerGridPos = new Vector2Int(Temp_playerGridPos.x, Temp_playerGridPos.y);
        Vector3Int mouseGridPos = tmap.WorldToCell(worldPos);

        Vector3Int Top = new Vector3Int(playerGridPos.x, playerGridPos.y + spaces);
        Vector3Int Down = new Vector3Int(playerGridPos.x, playerGridPos.y - spaces);
        Vector3Int Left = new Vector3Int(playerGridPos.x - spaces, playerGridPos.y);
        Vector3Int Right = new Vector3Int(playerGridPos.x + spaces, playerGridPos.y);

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

    public void ShowMoveableSpots(int spaces)
    {
        bool noneOnTop = true;
        bool noneOnDown = true;
        bool noneOnLeft = true;
        bool noneOnRight = true;

        Vector3Int Temp_playerGridPos = tmap.WorldToCell(player.transform.position);
        Vector2Int playerGridPos = new Vector2Int(Temp_playerGridPos.x, Temp_playerGridPos.y);

        Vector3Int T = new Vector3Int(playerGridPos.x, playerGridPos.y + spaces);
        Vector3Int D = new Vector3Int(playerGridPos.x, playerGridPos.y - spaces);
        Vector3Int L = new Vector3Int(playerGridPos.x - spaces, playerGridPos.y);
        Vector3Int R = new Vector3Int(playerGridPos.x + spaces, playerGridPos.y);

        foreach (GameObject e in enemies)
        {
            Vector3Int enemyCell = tmap.WorldToCell(e.transform.position);
            if (enemyCell == T)
            {
                noneOnTop = false;
            }
            if (enemyCell == D)
            {
                noneOnDown = false;
            }
            if (enemyCell == L)
            {
                noneOnLeft = false;
            }
            if (enemyCell == R)
            {
                noneOnRight = false;
            }
        }

        Vector3 Top = tmap.GetCellCenterWorld(T);
        Vector3 Down = tmap.GetCellCenterWorld(D);
        Vector3 Left = tmap.GetCellCenterWorld(L);
        Vector3 Right = tmap.GetCellCenterWorld(R);

        if (noneOnTop)
        {
            Instantiate(MoveableArrowsPrefab, new Vector3(Top.x, Top.y, Top.z), Quaternion.identity);
        }
        if (noneOnDown)
        {
            Instantiate(MoveableArrowsPrefab, new Vector3(Down.x, Down.y, Down.z), Quaternion.identity);
        }
        if (noneOnLeft)
        {
            Instantiate(MoveableArrowsPrefab, new Vector3(Left.x, Left.y, Left.z), Quaternion.identity);
        }
        if (noneOnRight)
        {
            Instantiate(MoveableArrowsPrefab, new Vector3(Right.x, Right.y, Right.z), Quaternion.identity);
        }
        shown = true;
    }

    public void ShowHittableSpots(int range)
    {
        Vector3Int Temp_playerGridPos = tmap.WorldToCell(player.transform.position);
        Vector2Int playerGridPos = new Vector2Int(Temp_playerGridPos.x, Temp_playerGridPos.y);

        Vector3Int T = new Vector3Int(playerGridPos.x, playerGridPos.y + range);
        Vector3Int D = new Vector3Int(playerGridPos.x, playerGridPos.y - range);
        Vector3Int L = new Vector3Int(playerGridPos.x - range, playerGridPos.y);
        Vector3Int R = new Vector3Int(playerGridPos.x + range, playerGridPos.y);

        Vector3 Top = tmap.GetCellCenterWorld(T);
        Vector3 Down = tmap.GetCellCenterWorld(D);
        Vector3 Left = tmap.GetCellCenterWorld(L);
        Vector3 Right = tmap.GetCellCenterWorld(R);

        Instantiate(HittablePrefab, new Vector3(Top.x, Top.y, Top.z), Quaternion.identity);
        Instantiate(HittablePrefab, new Vector3(Down.x, Down.y, Down.z), Quaternion.identity);
        Instantiate(HittablePrefab, new Vector3(Left.x, Left.y, Left.z), Quaternion.identity);
        Instantiate(HittablePrefab, new Vector3(Right.x, Right.y, Right.z), Quaternion.identity);
    }

    public void ShowShootableSpots(int range)
    {
        Vector3Int Temp_playerGridPos = tmap.WorldToCell(player.transform.position);
        Vector2Int playerGridPos = new Vector2Int(Temp_playerGridPos.x, Temp_playerGridPos.y);

        Vector3Int T = new Vector3Int(playerGridPos.x, playerGridPos.y + range);
        Vector3Int D = new Vector3Int(playerGridPos.x, playerGridPos.y - range);
        Vector3Int L = new Vector3Int(playerGridPos.x - range, playerGridPos.y);
        Vector3Int R = new Vector3Int(playerGridPos.x + range, playerGridPos.y);

        Vector3 Top = tmap.GetCellCenterWorld(T);
        Vector3 Down = tmap.GetCellCenterWorld(D);
        Vector3 Left = tmap.GetCellCenterWorld(L);
        Vector3 Right = tmap.GetCellCenterWorld(R);

        Instantiate(CrosshairPrefab, new Vector3(Top.x, Top.y, Top.z), Quaternion.identity);
        Instantiate(CrosshairPrefab, new Vector3(Down.x, Down.y, Down.z), Quaternion.identity);
        Instantiate(CrosshairPrefab, new Vector3(Left.x, Left.y, Left.z), Quaternion.identity);
        Instantiate(CrosshairPrefab, new Vector3(Right.x, Right.y, Right.z), Quaternion.identity);
    }

    public void DestroyObjectsWithName(string tagToFind)
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

    public bool IsPlayer_X_TileAway(GameObject enemy, int x)
    {
        Vector3Int playerCell = tmap.WorldToCell(player.transform.position);
        Vector3Int enemyCell = tmap.WorldToCell(enemy.transform.position);

        if (playerCell == enemyCell)
        {
            MoveTowardsPlayer(enemy);
            return true;
        }

        // Check if the player is exactly one tile above, below, to the left, or to the right of the enemy
        return Mathf.Abs(playerCell.x - enemyCell.x) == x && playerCell.y == enemyCell.y
            || Mathf.Abs(playerCell.y - enemyCell.y) == x && playerCell.x == enemyCell.x;
    }

    public void MoveTowardsPlayer(GameObject enemy)
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

        //enemy.transform.position = destinationCell;
        LeanTweenIt(enemy, destinationCell, 1f);
    }

    public void MoveAwayFromPlayer(GameObject enemy)
    {
        Vector3Int playerCell = tmap.WorldToCell(player.transform.position);
        Vector3Int enemyCell = tmap.WorldToCell(enemy.transform.position);

        Vector3 destinationCell;

        if (playerCell.x < enemyCell.x && (tmap.GetTile(new Vector3Int(enemyCell.x + 1, enemyCell.y, enemyCell.z)) != null))
        {
            // Player is to the left of the enemy
            destinationCell = tmap.GetCellCenterWorld(new Vector3Int(enemyCell.x + 1, enemyCell.y, enemyCell.z));
        }
        else if (playerCell.x > enemyCell.x && (tmap.GetTile(new Vector3Int(enemyCell.x - 1, enemyCell.y, enemyCell.z)) != null))
        {
            // Player is to the right of the enemy
            destinationCell = tmap.GetCellCenterWorld(new Vector3Int(enemyCell.x - 1, enemyCell.y, enemyCell.z));
        }
        else if (playerCell.y < enemyCell.y && (tmap.GetTile(new Vector3Int(enemyCell.x, enemyCell.y + 1, enemyCell.z)) != null))
        {
            // Player is below the enemy
            destinationCell = tmap.GetCellCenterWorld(new Vector3Int(enemyCell.x, enemyCell.y + 1, enemyCell.z));
        }
        else
        {
            // Player is above the enemy
            destinationCell = tmap.GetCellCenterWorld(new Vector3Int(enemyCell.x, enemyCell.y - 1, enemyCell.z));
        }

        //enemy.transform.position = destinationCell;
        LeanTweenIt(enemy, destinationCell, 1f);
    }

    public float DistanceFromPlayer(GameObject enemy)
    {
        Vector3 playerCell = tmap.GetCellCenterWorld(tmap.WorldToCell(player.transform.position));
        Vector3 enemyCell = tmap.GetCellCenterWorld(tmap.WorldToCell(enemy.transform.position));

        if (Mathf.Abs(playerCell.x - enemyCell.x) < Mathf.Abs(playerCell.y - enemyCell.y))
        {
            return Mathf.Abs(playerCell.y - enemyCell.y);
        }
        else
        {
            return Mathf.Abs(playerCell.x - enemyCell.x);
        }
    }

    bool HitByDash(Vector3 OriPos, Vector3 EndPos, Vector3 TargetPos)
    {
        // Check if TargetPos is between OriPos and EndPos along the X-axis
        bool hitOnX = Mathf.Min(OriPos.x, EndPos.x) <= TargetPos.x && TargetPos.x <= Mathf.Max(OriPos.x, EndPos.x);

        // Check if TargetPos is between OriPos and EndPos along the Y-axis
        bool hitOnY = Mathf.Min(OriPos.y, EndPos.y) <= TargetPos.y && TargetPos.y <= Mathf.Max(OriPos.y, EndPos.y);

        // Return true only if both conditions are satisfied
        return hitOnX && hitOnY;
    }

    void LeanTweenIt(GameObject obj, Vector3 targetPos, float duration)
    {
        isMoving = true;
        Vector3 originalPos = obj.transform.position;
        bool isX;

        // Use LeanTween to move the object without Hashtable
        LeanTweenType easeType = LeanTweenType.easeInOutQuad; // You can change the ease type as needed

        if (obj.transform.position.y == targetPos.y)
        {
            isX = true;
        }
        else
        {
            isX = false;
        }

        // Choose the appropriate tween method based on the specified axis
        if (isX)
        {
            LeanTween.moveX(obj, targetPos.x, duration).setEase(easeType).setOnComplete(() => { DestroyObjectsWithName("BouncingArrow"); isMoving = false; });
        }
        else
        {
            LeanTween.moveY(obj, targetPos.y, duration).setEase(easeType).setOnComplete(() => { DestroyObjectsWithName("BouncingArrow"); isMoving = false; });
        }
    }
}

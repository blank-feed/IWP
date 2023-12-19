using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playermovetile : MonoBehaviour
{
    GameObject player;
    Tilemap tmap;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tmap = GameObject.Find("Tilemap_Base").GetComponent<Tilemap>();
        player.transform.localScale = new Vector3(.5f, .5f, 1);
        player.transform.position = tmap.GetCellCenterWorld(new Vector3Int(-4, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && BattleManager.instance.moveable)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(GetDestination(targetPosition));
            TileBase destinationTile = GetDestination(targetPosition);
            if (destinationTile != null)
            {
                Vector3Int destinationCell = tmap.WorldToCell(targetPosition);
                player.transform.position = tmap.GetCellCenterWorld(destinationCell);
                BattleManager.instance.moveable = false;
                BattleManager.instance.bs = BattlingState.playerturn;
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
}

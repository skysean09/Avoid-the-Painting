using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{    
    public List<Tile> slots;
    public Tilemap map;

    public static SlotManager instance;
    public Dictionary<Vector2Int, SlotInfo> slotInfo =  new();

    private void Awake()
    {
        instance = this;
        slotInfo.Clear();
        for (int x = 1; x < Setting.gridSize + 1; x++)
        {
            for (int y = 1; y < Setting.gridSize + 1; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                SlotInfo slotinfo = new SlotInfo()
                {
                    level = 0,
                    state = Item.None
                };
                slotInfo[pos] = slotinfo;

                Vector3Int tilePos = new Vector3Int(pos.x - (Setting.gridSize / 2 + 2), -pos.y + (Setting.gridSize / 2), 0);
                map.SetTile(tilePos, slots[0]);
            }
        }
    }

    public void SlotLevelUp(Vector2Int pos)
    {
        SlotInfo slot = slotInfo[pos];
        Vector3Int tilePos = new Vector3Int(pos.x - (Setting.gridSize / 2 + 2), -pos.y + (Setting.gridSize / 2), 0);
        map.SetTile(tilePos, slots[slot.level]);
    }
}

public struct SlotInfo
{
    public int level;
    public Item state;
}
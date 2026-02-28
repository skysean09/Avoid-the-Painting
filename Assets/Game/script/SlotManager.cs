using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public List<string> hex = new List<string>()
    {
        "#E7E6F2",
        "#8DE074",
        "#7BD35B",
        "#51A52A",
        "#197B14"
    };


    public static SlotManager instance;
    public Dictionary<Vector2Int, SlotInfo> slotInfo =  new();

    private void Awake()
    {
        instance = this;
        slotInfo.Clear();
        for (int x = 1; x < Setting.gridSize +1; x++)
        {
            for (int y = 1; y < Setting.gridSize +1; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                SlotInfo slotinfo = new SlotInfo()
                {
                    level = 0,
                    state = Item.None
                };

                SlotManager.instance.slotInfo[pos] = slotinfo;
            }
        }
    }

    public List<Tile> slots = new();
    public Tilemap map;
    public void UpdateSlot(Vector2Int pos)
    {
        SlotInfo slot = slotInfo[pos];
        
    }
}

public struct SlotInfo
{
    public int level;
    public Item state;
}
using UnityEngine;

public class ItemSpanwer : MonoBehaviour
{
    public static ItemSpanwer instance;
    private void Awake()
    {
        instance = this;
    }


    public GameObject paintPrefab;

    public int paintNum = 0;

    private void Update()
    {
        if(Setting.maxLevelCount < Setting.clearCount && paintNum < Setting.paintNum)
        {
            int x = Random.Range(1, Setting.gridSize+1);
            int y = Random.Range(1, Setting.gridSize+1);

            Vector2Int pos = new Vector2Int(x, y);

            Debug.Log("check Level");
            SlotInfo slot = SlotManager.instance.slotInfo[pos];

            if(slot.level != Setting.maxLevel)
            {
                Debug.Log("Drop Painting");
                Paint(pos);
            }
        }

        if(Setting.maxLevelCount >= Setting.clearCount)
        {
            Debug.Log("Clear");
        }
    }

    void Paint(Vector2Int pos)
    {
        Vector3 worldPos =  new();
        worldPos.x = +(Setting.cellSize * (pos.x - (Setting.gridSize/2 + 1))) + Setting.gridPos.x;
        worldPos.y = -(Setting.cellSize * (pos.y - (Setting.gridSize / 2 + 1))) + Setting.gridPos.y;

        GameObject paint = Instantiate(paintPrefab, worldPos, Quaternion.identity);

        paint.GetComponent<Painting>().pos = pos;

        SlotInfo slot = SlotManager.instance.slotInfo[pos];
        slot.state = Item.Paint;
        SlotManager.instance.slotInfo[pos] = slot;

        paintNum++;
    }
}
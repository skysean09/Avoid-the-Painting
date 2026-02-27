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
        if(paintNum < Setting.paintNum)
        {
            int x = Random.Range(1, Setting.gridSize+1);
            int y = Random.Range(1, Setting.gridSize+1);

            Paint(new Vector2Int(x,y));
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
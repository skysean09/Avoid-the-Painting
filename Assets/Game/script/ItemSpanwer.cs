using System.Collections.Generic;
using UnityEngine;

public class ItemSpanwer : MonoBehaviour
{
    public Dictionary<Vector2Int, Item> status;

    private void Awake()
    {
        status.Clear();
        for (int i = 0; i < Setting.gridSize; i++)
        {
            for (int j = 0; j < Setting.gridSize; j++)
            {
                status.Add(new Vector2Int(i, j), Item.None);
                //(i - (gridSize + 1)/2) * CellSize
            }
        }
    }
    public GameObject linePrefab;
    public GameObject cellPrefab;
    public GameObject paintPrefab;
    public GameObject hpPrefab;

    public int lineAttackNum = 0;
    public int cellAttackNum = 0;
    public int paintNum = 0;
    public int hpNum = 0;

    private float lineTime = 0;
    private float cellTime = 0;

    private void Update()
    {
        lineTime -= Time.deltaTime;
        cellTime -= Time.deltaTime;


        if (lineTime <= 0 && lineAttackNum < Setting.lineAttackNum)
        {
            int x = Random.Range(0, Setting.gridSize);
            int y = Random.Range(0, Setting.gridSize);
            lineTime = Setting.lineTime;

        }
        else if(cellTime <= 0 && cellAttackNum < Setting.cellAttackNum)
        {
            int x = Random.Range(0, Setting.gridSize);
            int y = Random.Range(0, Setting.gridSize);
            CellAttack(new Vector2Int(x,y));
        }
        else if(paintNum < Setting.paintNum)
        {
            int x = Random.Range(0, Setting.gridSize);
            int y = Random.Range(0, Setting.gridSize);

            Paint(new Vector2Int(x,y));
        }
        else if(Setting.hp < Setting.maxHp && hpNum < Setting.hpNum)
        {
            int x = Random.Range(0, Setting.gridSize);
            int y = Random.Range(0, Setting.gridSize);

            Hp(new Vector2Int(x,y));
        }
    }

    void LineAttack(Vector2Int pos)
    {
        int line = Random.Range(0, 2); // 0이면 가로, 1이면 세로
    }

    void CellAttack(Vector2Int pos)
    {
        Instantiate(
            cellPrefab, 
            new Vector3((pos.x - (Setting.gridSize + 1) / 2) * Setting.cellSize, (pos.y - (Setting.gridSize + 1) / 2) * Setting.cellSize, 0), 
            Quaternion.identity);

        status[pos] = Item.CellAttack;

        cellAttackNum++;
        cellTime = Setting.cellTime;
    }

    void Paint(Vector2Int pos)
    {
       Instantiate(
           paintPrefab, 
           new Vector3((pos.x - (Setting.gridSize + 1) / 2) * Setting.cellSize, (pos.y - (Setting.gridSize + 1) / 2) * Setting.cellSize, 0), 
           Quaternion.identity);

        status[pos] = Item.Paint;

        paintNum++;
    }

    void Hp(Vector2Int pos)
    {
        Instantiate(
            hpPrefab, 
            new Vector3((pos.x - (Setting.gridSize + 1) / 2) * Setting.cellSize, (pos.y - (Setting.gridSize + 1) / 2) * Setting.cellSize, 0), 
            Quaternion.identity);

        status[pos] = Item.Hp;

        hpNum++;
    }
}
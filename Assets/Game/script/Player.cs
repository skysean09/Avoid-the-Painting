using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{
    Vector3 mouseOld;
    Vector3 mouseNew;
    public Vector2Int pos;
    public static Player instance;

    private void Awake()
    {
        instance = this;
        pos = new Vector2Int(Setting.gridSize/2 + 1, Setting.gridSize / 2 + 1);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseOld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0) && mouseOld != null)
        {
            mouseNew = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float gap_x = mouseNew.x - mouseOld.x;
            float gap_y = mouseNew.y - mouseOld.y;
            if (Mathf.Abs(gap_x) > Mathf.Abs(gap_y)) // 가로
            {
                if (gap_x > 0) // 오른쪽
                {
                    if (pos.x != Setting.gridSize)
                    {
                        this.transform.position += new Vector3(Setting.cellSize, 0, 0);
                        pos.x += 1;
                    }
                }
                else // 왼쪽
                {
                    if (pos.x != 1)
                    {
                        this.transform.position += new Vector3(-Setting.cellSize, 0, 0);
                        pos.x -= 1;
                    }
                }
            }
            else // 세로
            {
                if (gap_y > 0) // 위
                {
                    if (pos.y != 1)
                    {
                        this.transform.position += new Vector3(0, Setting.cellSize, 0);
                        pos.y -= 1;
                    }
                }
                else // 아래
                {
                    if (pos.y != Setting.gridSize)
                    {
                        this.transform.position += new Vector3(0, -Setting.cellSize, 0);
                        pos.y += 1;
                    }
                }
            }
        }
    }
}

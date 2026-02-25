using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{
    Vector3 old_;
    Vector3 new_;
    public static Player instance;
    Vector2Int pos;

    private void Awake()
    {
        instance = this;
        pos = new Vector2Int(3, 3);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            old_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0) && old_ != null)
        {
            new_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float gap_x = new_.x - old_.x;
            float gap_y = new_.y - old_.y;
            if (Mathf.Abs(gap_x) > Mathf.Abs(gap_y)) // 가로
            {
                if (gap_x > 0) // 오른쪽
                {
                    if (pos.x != Setting.gridSize)
                    {
                        this.transform.position += new Vector3(Setting.cellSize, 0, 0);
                        pos.x++;
                    }
                }
                else // 왼쪽
                {
                    if (pos.x != 1)
                    {
                        this.transform.position += new Vector3(-Setting.cellSize, 0, 0);
                        pos.x--;
                    }
                }
            }
            else // 세로
            {
                if (gap_y > 0) // 위
                {
                    if (pos.y != Setting.gridSize)
                    {
                        this.transform.position += new Vector3(0, Setting.cellSize, 0);
                        pos.y++;
                    }
                }
                else // 아래
                {
                    if (pos.y != 1)
                    {
                        this.transform.position += new Vector3(0, -Setting.cellSize, 0);
                        pos.y--;
                    }
                }
            }
        }
    }
}
